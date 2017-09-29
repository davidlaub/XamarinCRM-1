using System;
using Acme.Models;
using Acme.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeesPage : ContentPage
    {
        public EmployeesPage()
        {
            InitializeComponent();
        }

        private async void OnEmployeeSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is Employee employee)
            {
                await Navigation.PushAsync(new EmployeeDetailPage(new EmployeeDetailViewModel(employee)));

                // Manually deselect item
                EmployeesListView.SelectedItem = null;
            }
        }

        async void AddEmployee_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EmployeeEditPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is EmployeesViewModel vm)
            {
                if (vm.Employees.Count == 0)
                    vm.LoadEmployeesCommand.Execute(null);
            }
        }
    }
}