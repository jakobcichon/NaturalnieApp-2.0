using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NaturalnieApp2.Attributes.DisplayModelAttributes;

namespace NaturalnieApp2.Models.MenuScreens.Inventory
{
    internal class InventoryModel: ModelBase
    {
        [DisplayName("Nazwa inwentaryzacji")]
        [VisibilityProperties(true, true)]
        public string InventoryName { get; set; }

        [DisplayName("Data ostatniej modyfikacji")]
        [VisibilityProperties(true, true)]
        public DateTime LastModificationDate { get; set; }

        [DisplayName("Nazwa dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierName { get; set; }

        [DisplayName("Numer w kasie Elzab")]
        [VisibilityProperties(true, false)]
        public int ElzabProductId { get; set; }

        [DisplayName("Nazwa producenta")]
        [VisibilityProperties(true, false)]
        public string ManufacturerName { get; set; }

        [DisplayName("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        public string ProductName { get; set; }

        [DisplayName("Aktualna ilość produktów")]
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

        [DisplayName("Nazwa produktu w kasie Elzab")]
        [VisibilityProperties(true, true)]
        public string ElzabProductName { get; set; }

        [DisplayName("Cena netto")]
        [VisibilityProperties(true, false)]
        public float PriceNet { get; set; }

        [DisplayName("Zniżka")]
        [VisibilityProperties(true, true)]
        public int Discount { get; set; }

        [DisplayName("Cena netto ze zniżką")]
        [VisibilityProperties(true, false)]
        public float PriceNetWithDiscount { get; set; }

        [DisplayName("Wartość podatku")]
        [VisibilityProperties(true, false)]
        public int TaxValue { get; set; }

        [DisplayName("Marża")]
        [VisibilityProperties(true, true)]
        public int Marigin { get; set; }

        [DisplayName("Cena klienta")]
        [VisibilityProperties(true, true)]
        public float FinalPrice { get; set; }

        [DisplayName("Kod kreskowy")]
        [VisibilityProperties(true, false)]
        public string BarCode { get; set; }

        [DisplayName("kod kreskowy własny")]
        [VisibilityProperties(true, false)]
        public string BarCodeShort { get; set; }

        [DisplayName("Kod dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierCode { get; set; }

        [DisplayName("Informacje o produkcie")]
        [VisibilityProperties(true, true)]
        public string ProductInfo { get; set; }
    }
}
