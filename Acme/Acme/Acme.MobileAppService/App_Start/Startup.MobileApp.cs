using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;
using Acme.MobileAppService.DataObjects;
using Acme.MobileAppService.Models;

namespace Acme.MobileAppService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new DatabaseInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<templateitemsContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);

        }
    }

    public class DatabaseInitializer : CreateDatabaseIfNotExists<MasterDetailContext>
    {
        protected override void Seed(MasterDetailContext context)
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Seed1 FirstName",
                    LastName = "LastName",
                    HireDate = DateTime.Now,
                    Notes = "These are notes",
                    OfficeLocation = "Boston",
                    PhotoUri = "https://www.dropbox.com/s/fcln4i8qnsgfyl5/Boston%20City%20Flow.jpg?dl=0",
                    Salary = 56000,
                    VacationBalance = 108,
                    VacationUsed = 16
                },
                new Employee
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Seed2 FirstName",
                    LastName = "LastName",
                    HireDate = DateTime.Now,
                    Notes = "These are notes",
                    OfficeLocation = "Boston",
                    PhotoUri = "https://www.dropbox.com/s/fcln4i8qnsgfyl5/Boston%20City%20Flow.jpg?dl=0",
                    Salary = 56000,
                    VacationBalance = 108,
                    VacationUsed = 16
                },
            };

            // Seed the employees table
            foreach (Employee item in employees)
                context.Set<Employee>().Add(item);

            List<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Seed1 FirstName",
                    LastName = "LastName",
                    Street = "101 Tremont St",
                    City = "Boston",
                    StateOrRegion = "MA",
                    County = "USA",
                    ZipCode = "02118",
                    Notes = "These are notes"
                },
                new Customer
                {
                    Id = Guid.NewGuid().ToString(),
                    FirstName = "Seed2 FirstName",
                    LastName = "LastName",
                    Street = "101 Tremont St",
                    City = "Boston",
                    StateOrRegion = "MA",
                    County = "USA",
                    ZipCode = "02118",
                    Notes = "These are notes"
                },
            };

            // seed the Customers table
            foreach (Customer item in customers)
                context.Set<Customer>().Add(item);

            // seed the Products table
            context.Set<Product>().Add(new Product
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Seed Title", PhotoUri = "https://www.dropbox.com/s/fcln4i8qnsgfyl5/Boston%20City%20Flow.jpg?dl=0",
                InventoryCount = 100,
                IsDiscontinued = false,
                IsOnBackorder = true,
                Price = 25.99,
                SaleModifier = 0.25
            });
            
            // seed the Orders table (using the Employee and Customer seeds so there is referential integrity for seed data)
            context.Set<Order>().Add(new Order
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeId = employees[0].Id,
                CustomerId = customers[0].Id,
                Addressee = "Seed Addressee",
                DeliveryDate = DateTime.Now.AddDays(-10),
                OrderDate = DateTime.Now,
                City = "Boston",
                County = "MA",
                Notes = "These are notes",
                StateOrRegion = "MA",
                ZipCode = "02118",
                Street = "101 Tremont St",
                DeliveryService = "FedEx",
                Quantity = 2,
                SignatureRequired = true,
                TotalPrice = 299.95,
                WasDelivered = false
            });

            base.Seed(context);
        }
    }
}