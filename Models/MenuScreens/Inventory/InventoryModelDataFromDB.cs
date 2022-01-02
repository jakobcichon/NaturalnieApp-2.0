using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Models.MenuScreens.Inventory
{
    internal class InventoryModelDataFromDB: ModelBase
    {
        [DisplayName("Nazwa dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierName { get; set; }

        [DisplayName("Numer w kasie Elzab")]
        [VisibilityProperties(true, true)]
        public int ElzabProductId { get; set; }

        [DisplayName("Nazwa producenta")]
        [VisibilityProperties(true, true)]
        public string ManufacturerName { get; set; }

        [DisplayName("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        public string ProductName { get; set; }

        [DisplayName("Aktualna ilość produktów")]
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

        [DisplayName("Nazwa produktu w kasie Elzab")]
        [VisibilityProperties(true, true)]
        public string ElzabProductName { get; set; }

        [DisplayName("Cena netto")]
        [VisibilityProperties(true, true)]
        public float PriceNet { get; set; }

        [DisplayName("Zniżka")]
        [VisibilityProperties(true, true)]
        public int Discount { get; set; }

        [DisplayName("Cena netto ze zniżką")]
        [VisibilityProperties(true, true)]
        public float PriceNetWithDiscount { get; set; }

        [DisplayName("Wartość podatku")]
        [VisibilityProperties(true, true)]
        public int TaxValue { get; set; }

        [DisplayName("Marża")]
        [VisibilityProperties(true, true)]
        public int Marigin { get; set; }

        [DisplayName("Cena klienta")]
        [VisibilityProperties(true, true)]
        public float FinalPrice { get; set; }

        [DisplayName("Kod kreskowy")]
        [VisibilityProperties(true, true)]
        public string BarCode { get; set; }

        [DisplayName("kod kreskowy własny")]
        [VisibilityProperties(true, true)]
        public string BarCodeShort { get; set; }

        [DisplayName("Kod dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierCode { get; set; }

        [DisplayName("Informacje o produkcie")]
        [VisibilityProperties(true, true)]
        public string ProductInfo { get; set; }
    }
}
