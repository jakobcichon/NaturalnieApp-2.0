using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Models
{
    internal class Product: BindableModelBase
    {
        public string SupplierName { get; set; }
        public int ElzabProductId { get; set; }
        public string ManufacturerName { get; set; }
        //public string ProductName { get; set; }
        private string _productName;

        [DataGridAttributes.DisplayedPropertyName("Nazwa produktu")]
        public string ProductName
        {
            get { return _productName; }
            set 
            { 
                _productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

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

        public Product()
        {

        }

        public Product(string supplierName, 
            int elzabProductId, 
            string manufacturerName, 
            string productName, string 
            elzabProductName, 
            float priceNet, 
            int discount, 
            float priceNetWithDiscount, 
            int taxValue, 
            int marigin, 
            float finalPrice, 
            string barCode, 
            string barCodeShort, 
            string supplierCode, 
            string productInfo)
        {
            SupplierName = supplierName;
            ElzabProductId = elzabProductId;
            ManufacturerName = manufacturerName;
            ProductName = productName;
            ElzabProductName = elzabProductName;
            PriceNet = priceNet;
            Discount = discount;
            PriceNetWithDiscount = priceNetWithDiscount;
            TaxValue = taxValue;
            Marigin = marigin;
            FinalPrice = finalPrice;
            BarCode = barCode;
            BarCodeShort = barCodeShort;
            SupplierCode = supplierCode;
            ProductInfo = productInfo;
        }
    }
}
