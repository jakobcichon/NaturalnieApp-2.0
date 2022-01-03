using NaturalnieApp2.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs
{
    [Table("inventory_history")]
    public class InventoryHistoryDTO
    {
        [Key]
        public int Id { get; set; }
        public DateTime OperationDateTime { get; set; }
        public string InventoryName { get; set; }
        public DateTime LastModificationDate { get; set; }
        public int ProductQuantity { get; set; }
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
        public string OperationType { get; set; }
    }
}
