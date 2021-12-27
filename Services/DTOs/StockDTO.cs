using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs
{
    [Table("stock")]
    public class StockDTO
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ActualQuantity { get; set; }
        public int LastQuantity { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string BarcodeWithDate { get; set; }
    }
}
