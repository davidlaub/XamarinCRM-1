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
    public class AzureProductDataStore : IDataStore<Product>
    {
        public bool UseAuthentication => false;
        public MobileServiceAuthenticationProvider AuthProvider => MobileServiceAuthenticationProvider.Facebook;

        bool isInitialized;
        IMobileServiceSyncTable<Product> productsTable;

        public MobileServiceClient MobileService { get; set; }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();
            if (forceRefresh)
                await PullLatestAsync();

            return await productsTable.ToEnumerableAsync();
        }

        public async Task<Product> GetItemAsync(string id)
        {
            await InitializeAsync();
            await PullLatestAsync();
            var products = await productsTable.Where(s => s.Id == id).ToListAsync();

            if (products == null || products.Count == 0)
                return null;

            return products[0];
        }

        public async Task<bool> AddItemAsync(Product product)
        {
            await InitializeAsync();
            await PullLatestAsync();
            await productsTable.InsertAsync(product);
            await SyncAsync();

            return true;
        }

        public async Task<bool> UpdateItemAsync(Product product)
        {
            await InitializeAsync();
            await productsTable.UpdateAsync(product);
            await SyncAsync();

            return true;
        }

        public async Task<bool> DeleteItemAsync(Product product)
        {
            await InitializeAsync();
            await PullLatestAsync();
            await productsTable.DeleteAsync(product);
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
            store.DefineTable<Product>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            productsTable = MobileService.GetSyncTable<Product>();

            isInitialized = true;
        }

        public async Task<bool> PullLatestAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine("Unable to pull products, we are offline");
                return false;
            }
            try
            {
                await productsTable.PullAsync($"all{typeof(Product).Name}", productsTable.CreateQuery());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to pull products, that is alright as we have offline capabilities: " + ex);
                return false;
            }
            return true;
        }


        public async Task<bool> SyncAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                Debug.WriteLine("Unable to sync products, we are offline");
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

                    Debug.WriteLine(@"Error executing sync operation. Item: {0} ({1}). Operation discarded.", error.TableName, error.Item["id"]);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to sync products, that is alright as we have offline capabilities: " + ex);
                return false;
            }

            return true;
        }
    }
}
