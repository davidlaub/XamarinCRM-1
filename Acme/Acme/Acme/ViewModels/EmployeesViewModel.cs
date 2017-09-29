using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Acme.Helpers;
using Acme.Models;
using Acme.Views;
using Xamarin.Forms;

namespace Acme.ViewModels
{
    public class EmployeesViewModel : BaseViewModel
    {
        private Command loadEmployeesCommand;
        
        public EmployeesViewModel()
        {
            Title = "Employees";

            MessagingCenter.Subscribe<EmployeeEditPage, Employee>(this, "AddEmployee", async (obj, employee) =>
            {
                var _emp = (Employee) employee;
                Employees.Add(_emp);
                await EmployeesDataStore.AddItemAsync(_emp);
            });
        }

        public ObservableRangeCollection<Employee> Employees { get; set; } = new ObservableRangeCollection<Employee>();

        public Command LoadEmployeesCommand => loadEmployeesCommand ?? (loadEmployeesCommand = new Command(async () => await ExecuteLoadEmployeesCommand()));

        private async Task ExecuteLoadEmployeesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Employees.Clear();
                var items = await EmployeesDataStore.GetItemsAsync(true);
                Employees.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load employees.",
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
