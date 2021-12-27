using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs
{
    [Table("stock_history")]
    public class StockHistoryDTO
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAndTime { get; set; }
        public string OperationType { get; set; }
        public string SalesIdForAutomaticUpdate { get; set; }
    }
}
