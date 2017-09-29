using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure.Mobile.Server;

namespace Acme.MobileAppService.DataObjects
{
    public class Customer : EntityData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string StateOrRegion { get; set; }
        public string County { get; set; }
        public string ZipCode { get; set; }
        public string Notes { get; set; }
    }
}