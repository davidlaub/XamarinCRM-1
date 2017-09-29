using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace Acme.MobileAppService.DataObjects
{
    public class Order : EntityData
    {
        public string CustomerId { get; set; }
        public string EmployeeId { get; set; }
        public double TotalPrice { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public bool WasDelivered { get; set; }
        public bool SignatureRequired { get; set; }
        public string DeliveryService { get; set; }
        public string Addressee { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string StateOrRegion { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }
        public string Notes { get; set; }
    }
}