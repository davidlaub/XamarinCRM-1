using System;
using Telerik.XamarinForms.Common.DataAnnotations;

namespace Acme.Models
{
    public class Employee : BaseDataObject
    {
        private string firstName;
        private string lastName;
        private string photoUri;
        private string officeLocation;
        private DateTime hireDate;
        private double salary;
        private int vacationBalance;
        private int vacationUsed;
        private string notes;

        [DisplayOptions(PlaceholderText = "First name", Header = "First Name", Position = 0)]
        [NonEmptyValidator("Please fill the first name")]
        public string FirstName
        {
            get => firstName;
            set => Set(ref firstName, value);
        }

        [DisplayOptions(PlaceholderText = "Last Name", Header = "Last Name", Position = 1)]
        [NonEmptyValidator("Please fill the last name")]
        public string LastName
        {
            get => lastName;
            set => Set(ref lastName, value);
        }

        [DisplayOptions(PlaceholderText = "Enter URL to photo", Header = "Photo")]
        public string PhotoUri
        {
            get => photoUri;
            set => Set(ref photoUri, value);
        }

        [DisplayOptions(Header = "Office Location")]
        [NonEmptyValidator("Enter Office Location")]
        public string OfficeLocation
        {
            get => officeLocation;
            set => Set(ref officeLocation, value);
        }

        [DisplayOptions(Header = "Last Name")]
        [NonEmptyValidator("Enter Hire Date")]
        public DateTime HireDate
        {
            get => hireDate;
            set => Set(ref hireDate, value);
        }

        [DisplayOptions(Header = "Salary")]
        public double Salary
        {
            get => salary;
            set => Set(ref salary, value);
        }

        [DisplayOptions(Header = "Vacation Remaining")]
        public int VacationBalance
        {
            get => vacationBalance;
            set => Set(ref vacationBalance, value);
        }

        [DisplayOptions(Header = "Vacation Used")]
        public int VacationUsed
        {
            get => vacationUsed;
            set => Set(ref vacationUsed, value);
        }

        [DisplayOptions(Header = "Notes")]
        public string Notes
        {
            get => notes;
            set => Set(ref notes, value);
        }
    }
}