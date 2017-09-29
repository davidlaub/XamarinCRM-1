using Acme.Models;

namespace Acme.ViewModels
{
    public class EmployeeDetailViewModel : BaseViewModel
    {
        public EmployeeDetailViewModel()
        {
            
        }

        public EmployeeDetailViewModel(Employee item = null)
        {
            Title = item?.FirstName;
            SelectedEmployee = item;
        }

        public Employee SelectedEmployee { get; set; }
    }
}
