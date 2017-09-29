using Acme.Models;

namespace Acme.ViewModels
{
    public class CustomerDetailViewModel : BaseViewModel
    {
        public CustomerDetailViewModel()
        {
            
        }

        public CustomerDetailViewModel(Customer item = null)
        {
            Title = item?.FirstName;
            SelectedCustomer = item;
        }

        public Customer SelectedCustomer { get; set; }
    }
}
