
using Acme.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomerDetailPage : ContentPage
    {
        public CustomerDetailPage()
        {
            InitializeComponent();
        }

        public CustomerDetailPage(CustomerDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}