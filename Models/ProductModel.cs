using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NaturalnieApp2.Services.DataModel;
using static NaturalnieApp2.Attributes.DisplayModelAttributes;

namespace NaturalnieApp2.Models
{
    internal class ProductModel: ModelBase
    {
        [DisplayName("Nazwa dostawcy")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string SupplierName { get; set; }

        [DisplayName("Numer w kasie elzab")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public int ElzabProductId { get; set; }

        [DisplayName("Nazwa producenta")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string ManufacturerName { get; set; }

        [DisplayName("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string ProductName { get; set; }

        [DisplayName("Nazwa produktu w kasie Elzab")]
        [VisualRepresenation(VisualRepresenationType.Field)]
        [VisibilityProperties(true, false)]
        public string ElzabProductName { get; set; }

        [DisplayName("Cena netto")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public float PriceNet { get; set; }

        [DisplayName("Zniżka")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public int Discount { get; set; }

        [DisplayName("Cena netto ze zniżką")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public float PriceNetWithDiscount { get; set; }

        [DisplayName("Wartość podatku")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public int TaxValue { get; set; }

        [DisplayName("Marża")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public int Marigin { get; set; }

        [DisplayName("Cena klienta")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public float FinalPrice { get; set; }

        [DisplayName("Kode kreskowy")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string BarCode { get; set; }

        [DisplayName("Kod kreskowy własny")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string BarCodeShort { get; set; }

        [DisplayName("Kod dostawcy")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string SupplierCode { get; set; }

        [DisplayName("Informacje o produkcie")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string ProductInfo { get; set; }

        [DisplayName("Produkt może zostać usunięty z kasy")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public bool CanBeRemovedFromCashRegister { get; set; }

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
            string productInfo,
            bool canBeRemovedFromCashRegister)
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
            CanBeRemovedFromCashRegister = canBeRemovedFromCashRegister;
        }

    }
}
