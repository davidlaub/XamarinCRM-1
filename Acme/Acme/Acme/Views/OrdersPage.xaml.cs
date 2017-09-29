using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.Models;
using Acme.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public OrdersPage()
        {
            InitializeComponent();
        }

        private async void OnOrderSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Order order)
            {
                await Navigation.PushAsync(new OrderDetailPage(new OrderDetailViewModel(order)));

                // Manually deselect item
                OrdersListView.SelectedItem = null;
            }
        }

        async void AddOrder_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OrderEditPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is OrdersViewModel vm)
            {
                if (vm.Orders.Count == 0)
                    vm.LoadOrdersCommand.Execute(null);
            }
        }
    }
}