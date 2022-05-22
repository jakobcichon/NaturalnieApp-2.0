using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NaturalnieApp2.Services.DataModel;
using static NaturalnieApp2.Attributes.DisplayModelAttributes;
using System.ComponentModel;
using NaturalnieApp2.Validators.StringValidations;
using System.Windows.Controls;
using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Services.Database.Providers;

namespace NaturalnieApp2.Models
{
    internal record ProductModel: ModelBase, IHintListProvider
    {
        [NameToBeDisplayed("Nazwa dostawcy")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string SupplierName { get; set; }

        [NameToBeDisplayed("Numer w kasie elzab")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public int ElzabProductId { get; set; }

        [NameToBeDisplayed("Nazwa producenta")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string ManufacturerName { get; set; }

        [NameToBeDisplayed("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        [PropertyValidationRule(typeof(ProductNameValidator))]
        public string ProductName {get; set; }

        [NameToBeDisplayed("Nazwa produktu w kasie Elzab")]
        [VisualRepresenation(VisualRepresenationType.Field)]
        [VisibilityProperties(true, false)]
        public string ElzabProductName { get; set; }

        [NameToBeDisplayed("Cena netto")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public float PriceNet { get; set; }

        [NameToBeDisplayed("Zniżka")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public int Discount { get; set; }

        [NameToBeDisplayed("Cena netto ze zniżką")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public float PriceNetWithDiscount { get; set; }

        [NameToBeDisplayed("Wartość podatku")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.List)]
        public int TaxValue { get; set; }

        [NameToBeDisplayed("Marża")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public int Marigin { get; set; }

        [NameToBeDisplayed("Cena klienta")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public float FinalPrice { get; set; }

        [NameToBeDisplayed("Kode kreskowy")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string BarCode { get; set; }

        [NameToBeDisplayed("Kod kreskowy własny")]
        [VisibilityProperties(true, false)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string BarCodeShort { get; set; }

        [NameToBeDisplayed("Kod dostawcy")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string SupplierCode { get; set; }

        [NameToBeDisplayed("Informacje o produkcie")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public string ProductInfo { get; set; }

        [NameToBeDisplayed("Produkt może zostać usunięty z kasy")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public bool CanBeRemovedFromCashRegister { get; set; }

        public ProductModel()
        {
        }

        public ProductModel(TaxProvider taxProvider)
        {
            this.taxProvider = taxProvider ?? throw new ArgumentNullException(nameof(taxProvider));
        }

        private readonly TaxProvider? taxProvider = null;

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

        public List<object> GetHintList(string propertyName)
        {
            List<object> hintList = new List<object>();

            switch(propertyName)
            {
                case "TaxValue":
                    if (taxProvider != null)
                    {
                        try
                        {
                            hintList = new List<object>() { 0,5,8,23 };
                        }
                        catch (Exception ex)
                        {
                            break;
                        }
                        
                    }

                    break;

                default:
                    break;
            }

            return hintList;
        }

    }

}
