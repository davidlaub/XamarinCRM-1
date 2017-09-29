using System;

namespace Acme.Models
{
    public class Order : BaseDataObject
    {
        private string customerId;
        private string employeeId;
        private double totalPrice;
        private int quantity;
        private DateTime orderDate;
        private DateTime deliveryDate;
        private bool wasDelivered;
        private bool signatureRequired;
        private string deliveryService;
        private string addressee;
        private string street;
        private string city;
        private string stateOrRegion;
        private string county;
        private string zipCode;
        private string notes;

        public string CustomerId
        {
            get => customerId;
            set => Set(ref customerId, value);
        }

        public string EmployeeId
        {
            get => employeeId;
            set => Set(ref employeeId, value);
        }

        public double TotalPrice
        {
            get => totalPrice;
            set => Set(ref totalPrice, value);
        }

        public int Quantity
        {
            get => quantity;
            set => Set(ref quantity, value);
        }

        public DateTime OrderDate
        {
            get => orderDate;
            set => Set(ref orderDate, value);
        }

        public DateTime DeliveryDate
        {
            get => deliveryDate;
            set => Set(ref deliveryDate, value);
        }

        public bool WasDelivered
        {
            get => wasDelivered;
            set => Set(ref wasDelivered, value);
        }

        public bool SignatureRequired
        {
            get => signatureRequired;
            set => Set(ref signatureRequired, value);
        }

        public string DeliveryService
        {
            get => deliveryService;
            set => Set(ref deliveryService, value);
        }

        public string Addressee
        {
            get => addressee;
            set => Set(ref addressee, value);
        }

        public string Street
        {
            get => street;
            set => Set(ref street, value);
        }

        public string City
        {
            get => city;
            set => Set(ref city, value);
        }

        public string StateOrRegion
        {
            get => stateOrRegion;
            set => Set(ref stateOrRegion, value);
        }

        public string County
        {
            get => county;
            set => Set(ref county, value);
        }

        public string ZipCode
        {
            get => zipCode;
            set => Set(ref zipCode, value);
        }

        public string Notes
        {
            get => notes;
            set => Set(ref notes, value);
        }
    }
}