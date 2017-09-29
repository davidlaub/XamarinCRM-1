using System;
using System.Collections.Generic;

using Acme.Helpers;
using Acme.Services;
using Acme.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Acme
{
    public partial class App : Application
    {
        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(ServiceConstants.AzureMobileAppUrl))
            {
                throw new NotImplementedException("You need to update Acme/Helpers/ServiceConstants.cs with your Azure Mobile Service URL.");
            }

            DependencyService.Register<AzureEmployeeDataStore>();
            DependencyService.Register<AzureCustomerDataStore>();
            DependencyService.Register<AzureProductDataStore>();
            DependencyService.Register<AzureOrderDataStore>();
            
            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (!Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
        }

        public static void GoToMainPage()
        {
            // TODO Replace with MasterDetailPage
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new CustomersPage())
                    {
                        Title = "Customers",
                        Icon = GetPageIcon("Customers")
                    },
                    new NavigationPage(new ProductsPage())
                    {
                        Title = "Products",
                        Icon = GetPageIcon("Products")
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = GetPageIcon("About")
                    },
                }
            };
        }

        private static FileImageSource GetPageIcon(string pageName)
        {
            if (pageName == "Products")
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return "tab_feed.png";
                    case Device.Windows:
                    case Device.iOS:
                        return null; //TODO determine better option
                    default:
                        return null;
                }
            }
            else if (pageName == "About")
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return "tab_about.png";
                    case Device.Windows:
                    case Device.iOS:
                        return null; //TODO determine better option
                    default:
                        return null;
                }
            }

            return null;
        }
    }
}
