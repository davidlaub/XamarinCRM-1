using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeeEditPage : ContentPage
    {
        public EmployeeEditPage()
        {
            InitializeComponent();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddEmployee", DataForm.Source);
            await Navigation.PopToRootAsync();
        }
    }
}