using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Acme.Helpers;
using Acme.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Plugin.Connectivity;

namespace Acme.Services
{
    public class AzureOrderDataStore : IDataStore<Order>
    {
        public bool UseAuthentication => false;
        public MobileServiceAuthenticationProvider AuthProvider => MobileServiceAuthenticationProvider.Facebook;

        bool isInitialized;
        IMobileServiceSyncTable<Order> ordersTable;

        public MobileServiceClient MobileService { get; set; }

        public async Task<IEnumerable<Order>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();
            if (forceRefresh)
                await PullLatestAsync();

            return await ordersTable.ToEnumerableAsync();
        }

        public async Task<Order> GetItemAsync(string id)
        {
            await InitializeAsync();
            await PullLatestAsync();
            var orders = await ordersTable.Where(s => s.Id == id).ToListAsync();

            if (orders == null || orders.Count == 0)
                return null;

            return orders[0];
        }

        public async Task<bool> AddItemAsync(Order order)
        {
            await InitializeAsync();
            await PullLatestAsync();
            await ordersTable.InsertAsync(order);
            await SyncAsync();

            return true;
        }

        public async Task<bool> UpdateItemAsync(Order order)
        {
            await InitializeAsync();
            await ordersTable.UpdateAsync(order);
            await SyncAsync();

            return true;
        }

        public async Task<bool> DeleteItemAsync(Order order)
        {
            await InitializeAsync();
            await PullLatestAsync();
            await ordersTable.DeleteAsync(order);
            await SyncAsync();

            return true;
        }

        public async Task InitializeAsync()
        {
            if (isInitialized)
                return;

            AuthenticationHandler handler = null;

            if (UseAuthentication)
                handler = new AuthenticationHandler();

            MobileService = new MobileServiceClient(ServiceConstants.AzureMobileAppUrl, handler)
            {
                SerializerSettings = new MobileServiceJsonSerializerSettings
                {
                    CamelCasePropertyNames = true
                }
            };

            if (UseAuthentication && !string.IsNullOrWhiteSpace(Settings.AuthToken) && !string.IsNullOrWhiteSpace(Settings.UserId))
            {
                MobileService.CurrentUser = new MobileServiceUser(Settings.UserId);
                MobileService.CurrentUser.MobileServiceAuthenticationToken = Settings.AuthToken;
            }

            var store = new MobileServiceSQLiteStore("app.db");
            store.DefineTable<Order>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            ordersTable = MobileService.GetSyncTable<Order>();

            isInitialized = true;
        }

        public async Task<bool> PullLatestAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine("Unable to pull orders, we are offline");
                return false;
            }
            try
            {
                await ordersTable.PullAsync($"all{typeof(Order).Name}", ordersTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to pull orders, that is alright as we have offline capabilities: " + ex);
                return false;
            }
            return true;
        }


        public async Task<bool> SyncAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine("Unable to sync orders, we are offline");
                return false;
            }
            try
            {
                await MobileService.SyncContext.PushAsync();
                if (!(await PullLatestAsync().ConfigureAwait(false)))
                    return false;
            }
            catch (MobileServicePushFailedException exc)
            {
                if (exc.PushResult == null)
                {
                    Debug.WriteLine("Unable to sync, that is alright as we have offline capabilities: " + exc);
                    return false;
                }
                foreach (var error in exc.PushResult.Errors)
                {
                    if (error.OperationKind == MobileServiceTableOperationKind.Update && error.Result != null)
                    {
                        //Update failed, reverting to server's copy.
                        await error.CancelAndUpdateItemAsync(error.Result);
                    }
                    else
                    {
                        // Discard local change.
                        await error.CancelAndDiscardItemAsync();
                    }

                    Debug.WriteLine(@"Error executing sync operation. order: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync orders, that is alright as we have offline capabilities: " + ex);
                return false;
            }

            return true;
        }
    }
}
