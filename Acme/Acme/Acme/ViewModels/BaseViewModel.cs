using Acme.Helpers;
using Acme.Models;
using Acme.Services;

using Xamarin.Forms;

namespace Acme.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        public IDataStore<Employee> EmployeesDataStore => DependencyService.Get<IDataStore<Employee>>();
        public IDataStore<Customer> CustomersDataStore => DependencyService.Get<IDataStore<Customer>>();
        public IDataStore<Product> ProductsDataStore => DependencyService.Get<IDataStore<Product>>();
        public IDataStore<Order> OrdersDataStore => DependencyService.Get<IDataStore<Order>>();
        //public IDataStore<Item> ItemsDataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set => Set(ref isBusy, value);
        }

        string title = string.Empty;
        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }
    }
}

