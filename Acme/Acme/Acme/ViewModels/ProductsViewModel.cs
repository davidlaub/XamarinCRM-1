using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acme.Helpers;
using Acme.Models;
using Acme.Views;
using Xamarin.Forms;

namespace Acme.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private Command loadProductsCommand;

        public ProductsViewModel()
        {
            Title = "Products";

            MessagingCenter.Subscribe<ProductEditPage, Product>(this, "AddProduct", async (obj, product) =>
            {
                var _prod = (Product) product;
                Products.Add(_prod);
                await ProductsDataStore.AddItemAsync(_prod);
            });
        }

        public ObservableRangeCollection<Product> Products { get; set; } = new ObservableRangeCollection<Product>();

        public Command LoadProductsCommand => loadProductsCommand ?? (loadProductsCommand = new Command(async () => await ExecuteLoadProductsCommand()));
        

        private async Task ExecuteLoadProductsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Products.Clear();
                var items = await ProductsDataStore.GetItemsAsync(true);
                Products.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load Products.",
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
