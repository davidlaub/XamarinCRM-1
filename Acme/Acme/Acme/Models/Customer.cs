using Telerik.XamarinForms.Common.DataAnnotations;

namespace Acme.Models
{
    public class Customer : BaseDataObject
    {
        private string firstName;
        private string lastName;
        private string street;
        private string city;
        private string stateOrRegion;
        private string county;
        private string zipCode;
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


        [DisplayOptions(Header = "Street")]
        public string Street
        {
            get => street;
            set => Set(ref street, value);
        }

        [DisplayOptions(Header = "City")]
        public string City
        {
            get => city;
            set => Set(ref city, value);
        }

        [DisplayOptions(Header = "State or Region")]
        public string StateOrRegion
        {
            get => stateOrRegion;
            set => Set(ref stateOrRegion, value);
        }

        [DisplayOptions(Header = "Country")]
        public string County
        {
            get => county;
            set => Set(ref county, value);
        }

        [DisplayOptions(Header = "ZIP Code")]
        public string ZipCode
        {
            get => zipCode;
            set => Set(ref zipCode, value);
        }

        [DisplayOptions(Header = "Notes")]
        public string Notes
        {
            get => notes;
            set => Set(ref notes, value);
        }
    }
}
