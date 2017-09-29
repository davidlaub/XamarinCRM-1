using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acme.Helpers;
using Acme.Models;
using Acme.Views;
using Xamarin.Forms;

namespace Acme.ViewModels
{
    public class CustomersViewModel : BaseViewModel
    {
        private Command loadCustomersCommand;

        public CustomersViewModel()
        {
            Title = "Customers";

            MessagingCenter.Subscribe<CustomerEditPage, Customer>(this, "AddCustomer", async (obj, customer) =>
            {
                var _customer = (Customer) customer;
                Customers.Add(_customer);
                await CustomersDataStore.AddItemAsync(_customer);
            });
        }

        public ObservableRangeCollection<Customer> Customers { get; set; } = new ObservableRangeCollection<Customer>();
        
        public Command LoadCustomersCommand => loadCustomersCommand ?? (loadCustomersCommand = new Command(async () => await ExecuteLoadCustomersCommand()));

        private async Task ExecuteLoadCustomersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Customers.Clear();
                var items = await CustomersDataStore.GetItemsAsync(true);
                Customers.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load customers.",
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
