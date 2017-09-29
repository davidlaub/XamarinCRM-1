using System;
using Microsoft.Azure.Mobile.Server;

namespace Acme.MobileAppService.DataObjects
{
    public class Employee : EntityData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhotoUri { get; set; }
        public string OfficeLocation { get; set; }
        public DateTime HireDate { get; set; }
        public double Salary { get; set; }
        public int VacationBalance { get; set; }
        public int VacationUsed { get; set; }
        public string Notes { get; set; }
    }
}