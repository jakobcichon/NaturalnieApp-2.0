using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Models.MenuScreens.Inventory
{
    internal class InventoryModel
    {
        [DataGridAttributes.DisplayName("Nazwa dostawcy")]
        [DataGridAttributes.ColumnProperties(true, true)]
        public string SupplierName { get; set; }

        [DataGridAttributes.DisplayName("Numer w kasie Elzab")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public int ElzabProductId { get; set; }

        [DataGridAttributes.DisplayName("Nazwa producenta")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public string ManufacturerName { get; set; }

        [DataGridAttributes.DisplayName("Nazwa produktu")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public string ProductName { get; set; }

        [DataGridAttributes.DisplayName("Nazwa produktu w kasie Elzab")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public string ElzabProductName { get; set; }

        [DataGridAttributes.DisplayName("Cena netto")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public float PriceNet { get; set; }

        [DataGridAttributes.DisplayName("Zniżka")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public int Discount { get; set; }

        [DataGridAttributes.DisplayName("Cena netto ze zniżką")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public float PriceNetWithDiscount { get; set; }

        [DataGridAttributes.DisplayName("Wartość podatku")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public int TaxValue { get; set; }

        [DataGridAttributes.DisplayName("Marża")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public int Marigin { get; set; }

        [DataGridAttributes.DisplayName("Cena klienta")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public float FinalPrice { get; set; }

        [DataGridAttributes.DisplayName("Kod kreskowy")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public string BarCode { get; set; }

        [DataGridAttributes.DisplayName("kod kreskowy własny")]
        [DataGridAttributes.ColumnProperties(true, false)]
        public string BarCodeShort { get; set; }

        [DataGridAttributes.DisplayName("Kod dostawcy")]
        [DataGridAttributes.ColumnProperties(true, true)]
        public string SupplierCode { get; set; }

        [DataGridAttributes.DisplayName("Informacje o produkcie")]
        [DataGridAttributes.ColumnProperties(true, true)]
        public string ProductInfo { get; set; }
    }
}
