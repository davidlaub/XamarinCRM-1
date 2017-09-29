using Acme.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Acme.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        public OrderDetailPage()
        {
            InitializeComponent();
        }

        public OrderDetailPage(OrderDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}