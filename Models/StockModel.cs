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
    internal record StockModel : ModelBase
    {
        [NameToBeDisplayed("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        public string ProductName { get; set; }

        [NameToBeDisplayed("Obecny stan")]
        [VisibilityProperties(true, false)]
        public int ActualQuantity { get; set; }

        [NameToBeDisplayed("Poprzedni stan")]
        [VisibilityProperties(true, false)]
        public int LastQuantity { get; set; }

        [NameToBeDisplayed("Data ostatniej modyfikacji")]
        [VisibilityProperties(true, false)]
        public DateTime ModificationDate { get; set; }

        [NameToBeDisplayed("Data ważności")]
        [VisibilityProperties(true, true)]
        public DateTime ExpirationDate { get; set; }

        [NameToBeDisplayed("Kod kreskowy z datą")]
        [VisibilityProperties(true, true)]
        public string BarcodeWithDate { get; set; }


        public StockModel()
        {

        }

        public StockModel(string productName, 
            int actualQuantity, 
            int lastQuantity, 
            DateTime modificationDate, 
            DateTime expirationDate, string barcodeWithDate)
        {
            ProductName = productName;
            ActualQuantity = actualQuantity;
            LastQuantity = lastQuantity;
            ModificationDate = modificationDate;
            ExpirationDate = expirationDate;
            BarcodeWithDate = barcodeWithDate;
        }
    }
}
