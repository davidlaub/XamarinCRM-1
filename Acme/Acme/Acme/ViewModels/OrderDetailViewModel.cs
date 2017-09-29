using System.Threading.Tasks;
using Acme.Models;

namespace Acme.ViewModels
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public OrderDetailViewModel()
        {
            
        }

        public OrderDetailViewModel(Order item = null)
        {
            Title = "Order Details";
            SelectedOrder = item;
        }

        public Order SelectedOrder { get; set; }

        public Customer RelatedCustomer { get; set; }

        public Employee RelatedEmployee { get; set; }

        public async Task LoadRelatedFieldsAsync()
        {
            RelatedCustomer = await CustomersDataStore.GetItemAsync(SelectedOrder.CustomerId);
            RelatedEmployee = await EmployeesDataStore.GetItemAsync(SelectedOrder.EmployeeId);
        }
    }
}
