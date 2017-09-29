namespace Acme.Models
{
    public class Product : BaseDataObject
    {
        private string title;
        private double price;
        private string photoUri;
        private int inventoryCount;
        private bool isOnBackorder;
        private bool isDiscontinued;
        private double saleModifier;

        public string Title
        {
            get => title;
            set => Set(ref title, value);
        }

        public double Price
        {
            get => price;
            set => Set(ref price, value);
        }

        public string PhotoUri
        {
            get => photoUri;
            set => Set(ref photoUri, value);
        }

        public int InventoryCount
        {
            get => inventoryCount;
            set => Set(ref inventoryCount, value);
        }

        public bool IsOnBackorder
        {
            get => isOnBackorder;
            set => Set(ref isOnBackorder, value);
        }

        public bool IsDiscontinued
        {
            get => isDiscontinued;
            set => Set(ref isDiscontinued, value);
        }

        public double SaleModifier
        {
            get => saleModifier;
            set => Set(ref saleModifier, value);
        }
    }
}