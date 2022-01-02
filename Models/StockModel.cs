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
    internal class StockModel: ModelBase
    {
        [DisplayName("Nazwa produktu")]
        [VisibilityProperties(true, false)]
        public string ProductName { get; set; }

        [DisplayName("Obecny stan")]
        [VisibilityProperties(true, false)]
        public int ActualQuantity { get; set; }

        [DisplayName("Poprzedni stan")]
        [VisibilityProperties(true, false)]
        public int LastQuantity { get; set; }

        [DisplayName("Data ostatniej modyfikacji")]
        [VisibilityProperties(true, false)]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Data ważności")]
        [VisibilityProperties(true, true)]
        public DateTime ExpirationDate { get; set; }

        [DisplayName("Kod kreskowy z datą")]
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
