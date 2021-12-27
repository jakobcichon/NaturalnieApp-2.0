using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs
{
    [Table("supplier")]
    public class SupplierDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }

        public SupplierDTO DeepCopy()
        {
            SupplierDTO supplier = (SupplierDTO)this.MemberwiseClone();
            supplier.Id = this.Id;
            supplier.Name = this.Name;
            supplier.Info = this.Info;

            return supplier;
        }
    }
}
