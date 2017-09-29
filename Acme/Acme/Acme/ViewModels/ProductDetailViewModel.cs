using Acme.Models;

namespace Acme.ViewModels
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public ProductDetailViewModel()
        {
        } 

        public ProductDetailViewModel(Product item = null)
        {
            Title = item?.Title;
            SelectedProduct = item;
        }

        public Product SelectedProduct { get; set; }
    }
}
