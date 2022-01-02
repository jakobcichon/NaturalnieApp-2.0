using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NaturalnieApp2.Services.DataModel;

namespace NaturalnieApp2.Models
{
    internal class ProductModel: ModelBase
    {
        [DisplayName("Nazwa dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierName { get; set; }

        [DisplayName("Numer w kasie elzab")]
        [VisibilityProperties(true, false)]
        public int ElzabProductId { get; set; }

        [DisplayName("Nazwa producenta")]
        [VisibilityProperties(true, false)]
        public string ManufacturerName { get; set; }

        [DisplayName("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        public string ProductName { get; set; }

        [DisplayName("Nazwa produktu w kasie Elzab")]
        [VisibilityProperties(true, false)]
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

        [DisplayName("Kode kreskowy")]
        [VisibilityProperties(true, false)]
        public string BarCode { get; set; }

        [DisplayName("Kod kreskowy własny")]
        [VisibilityProperties(true, false)]
        public string BarCodeShort { get; set; }

        [DisplayName("Kod dostawcy")]
        [VisibilityProperties(true, true)]
        public string SupplierCode { get; set; }

        [DisplayName("Informacje o produkcie")]
        [VisibilityProperties(true, true)]
        public string ProductInfo { get; set; }

        public ProductModel()
        {

        }

        public ProductModel(string supplierName, 
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
