using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs
{
    [Table("tax")]
    public class TaxDTO
    {
        [Key]
        public int Id { get; set; }
        public int TaxValue { get; set; }

        public ICollection<ProductDTO> Products { get; set; }

        public TaxDTO DeepCopy()
        {
            TaxDTO tax = (TaxDTO)this.MemberwiseClone();
            tax.Id = this.Id;
            tax.TaxValue = this.TaxValue;

            return tax;
        }
    }
}
