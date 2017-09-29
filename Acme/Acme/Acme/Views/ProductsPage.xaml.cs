using System;
using Acme.Models;
using Acme.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public ProductsPage()
        {
            InitializeComponent();
        }

        private async void OnProductSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Product employee)
            {
                await Navigation.PushAsync(new ProductDetailPage(new ProductDetailViewModel(employee)));

                // Manually deselect item
                ProductsListView.SelectedItem = null;
            }
        }

        async void AddProduct_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductEditPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ProductsViewModel vm)
            {
                if (vm.Products.Count == 0)
                    vm.LoadProductsCommand.Execute(null);
            }
        }
    }
}