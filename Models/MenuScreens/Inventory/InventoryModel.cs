using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Models.MenuScreens.Inventory
{
    internal class InventoryModel
    {
        public string SupplierName { get; set; }
        public int ElzabProductId { get; set; }
        public string ManufacturerName { get; set; }
        public string ProductName { get; set; }
        public string ElzabProductName { get; set; }
        public float PriceNet { get; set; }
        public int Discount { get; set; }
        public float PriceNetWithDiscount { get; set; }
        public int TaxValue { get; set; }
        public int Marigin { get; set; }
        public float FinalPrice { get; set; }
        public string BarCode { get; set; }
        public string BarCodeShort { get; set; }
        public string SupplierCode { get; set; }
        public string ProductInfo { get; set; }
    }
}
