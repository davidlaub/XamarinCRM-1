using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acme.Helpers;
using Acme.Models;
using Acme.Views;
using Xamarin.Forms;

namespace Acme.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private Command loadOrdersCommand;

        public OrdersViewModel()
        {
            Title = "Orders";

            MessagingCenter.Subscribe<OrderEditPage, Order>(this, "AddOrder", async (obj, order) =>
            {
                var _order = (Order) order;
                Orders.Add(_order);
                await OrdersDataStore.AddItemAsync(_order);
            });
        }

        public ObservableRangeCollection<Order> Orders { get; set; } = new ObservableRangeCollection<Order>();

        public Command LoadOrdersCommand => loadOrdersCommand ?? (loadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand()));

        private async Task ExecuteLoadOrdersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Orders.Clear();
                var items = await OrdersDataStore.GetItemsAsync(true);
                Orders.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load Orders.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
