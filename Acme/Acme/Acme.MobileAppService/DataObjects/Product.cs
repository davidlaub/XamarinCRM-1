using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace Acme.MobileAppService.DataObjects
{
    public class Product : EntityData
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public string PhotoUri { get; set; }
        public int InventoryCount { get; set; }
        public bool IsOnBackorder { get; set; }
        public bool IsDiscontinued { get; set; }
        public double SaleModifier { get; set; }
    }
}