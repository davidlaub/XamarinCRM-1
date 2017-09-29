using System;
using Acme.Models;
using Acme.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersPage : ContentPage
    {
        public CustomersPage()
        {
            InitializeComponent();
        }

        private async void OnCustomerSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Customer customer)
            {
                await Navigation.PushAsync(new CustomerDetailPage(new CustomerDetailViewModel(customer)));

                // Manually deselect item
                CustomersListView.SelectedItem = null;
            }
        }

        async void AddCustomer_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CustomerEditPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is CustomersViewModel vm)
            {
                if (vm.Customers.Count == 0)
                    vm.LoadCustomersCommand.Execute(null);
            }
        }
    }
}