using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NaturalnieApp2.Attributes.DisplayModelAttributes;

namespace NaturalnieApp2.Models.MenuScreens.Inventory
{
    internal record InventoryModel : ModelBase
    {
        [NameToBeDisplayed("Nazwa inwentaryzacji")]
        [VisibilityProperties(true, true)]
        public string InventoryName { get; set; }

        [NameToBeDisplayed("Data ostatniej modyfikacji")]
        [VisibilityProperties(true, true)]
        public DateTime LastModificationDate { get; set; }

        [NameToBeDisplayed("Nazwa dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierName { get; set; }

        [NameToBeDisplayed("Numer w kasie Elzab")]
        [VisibilityProperties(true, false)]
        public int ElzabProductId { get; set; }

        [NameToBeDisplayed("Nazwa producenta")]
        [VisibilityProperties(true, false)]
        public string ManufacturerName { get; set; }

        [NameToBeDisplayed("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        public string ProductName { get; set; }

        [NameToBeDisplayed("Aktualna ilość produktów")]
        [VisibilityProperties(true, false)]
        [ModificationProperties(true)]
        public int ProductQuantity
        {
            get { return productQuantity; }
            set 
            { 
                productQuantity = value; 
                OnPropertyChanged(nameof(ProductQuantity));
            }
        }
        private int productQuantity;

        [NameToBeDisplayed("Nazwa produktu w kasie Elzab")]
        [VisibilityProperties(true, true)]
        public string ElzabProductName { get; set; }

        [NameToBeDisplayed("Cena netto")]
        [VisibilityProperties(true, false)]
        public float PriceNet { get; set; }

        [NameToBeDisplayed("Zniżka")]
        [VisibilityProperties(true, true)]
        public int Discount { get; set; }

        [NameToBeDisplayed("Cena netto ze zniżką")]
        [VisibilityProperties(true, false)]
        public float PriceNetWithDiscount { get; set; }

        [NameToBeDisplayed("Wartość podatku")]
        [VisibilityProperties(true, false)]
        public int TaxValue { get; set; }

        [NameToBeDisplayed("Marża")]
        [VisibilityProperties(true, true)]
        public int Marigin { get; set; }

        [NameToBeDisplayed("Cena klienta")]
        [VisibilityProperties(true, true)]
        public float FinalPrice { get; set; }

        [NameToBeDisplayed("Kod kreskowy")]
        [VisibilityProperties(true, false)]
        public string BarCode { get; set; }

        [NameToBeDisplayed("kod kreskowy własny")]
        [VisibilityProperties(true, false)]
        public string BarCodeShort { get; set; }

        [NameToBeDisplayed("Kod dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierCode { get; set; }

        [NameToBeDisplayed("Informacje o produkcie")]
        [VisibilityProperties(true, true)]
        public string ProductInfo { get; set; }
    }
}
