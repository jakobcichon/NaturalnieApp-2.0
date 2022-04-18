using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NaturalnieApp2.Attributes.DisplayModelAttributes;

namespace NaturalnieApp2.Models.MenuScreens.Inventory
{
    internal class InventoryModelDataFromDB: ModelBase
    {
        [NameToBeDisplayed("Nazwa dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierName { get; set; }

        [NameToBeDisplayed("Numer w kasie Elzab")]
        [VisibilityProperties(true, true)]
        public int ElzabProductId { get; set; }

        [NameToBeDisplayed("Nazwa producenta")]
        [VisibilityProperties(true, true)]
        public string ManufacturerName { get; set; }

        [NameToBeDisplayed("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        public string ProductName { get; set; }

        [NameToBeDisplayed("Aktualna ilość produktów")]
        [VisibilityProperties(true, false)]
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
        [VisibilityProperties(true, true)]
        public float PriceNet { get; set; }

        [NameToBeDisplayed("Zniżka")]
        [VisibilityProperties(true, true)]
        public int Discount { get; set; }

        [NameToBeDisplayed("Cena netto ze zniżką")]
        [VisibilityProperties(true, true)]
        public float PriceNetWithDiscount { get; set; }

        [NameToBeDisplayed("Wartość podatku")]
        [VisibilityProperties(true, true)]
        public int TaxValue { get; set; }

        [NameToBeDisplayed("Marża")]
        [VisibilityProperties(true, true)]
        public int Marigin { get; set; }

        [NameToBeDisplayed("Cena klienta")]
        [VisibilityProperties(true, true)]
        public float FinalPrice { get; set; }

        [NameToBeDisplayed("Kod kreskowy")]
        [VisibilityProperties(true, true)]
        public string BarCode { get; set; }

        [NameToBeDisplayed("kod kreskowy własny")]
        [VisibilityProperties(true, true)]
        public string BarCodeShort { get; set; }

        [NameToBeDisplayed("Kod dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierCode { get; set; }

        [NameToBeDisplayed("Informacje o produkcie")]
        [VisibilityProperties(true, true)]
        public string ProductInfo { get; set; }
    }
}
