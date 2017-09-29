using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderEditPage : ContentPage
    {
        public OrderEditPage()
        {
            InitializeComponent();
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddOrder", DataForm.Source);
            await Navigation.PopToRootAsync();
        }
    }
}