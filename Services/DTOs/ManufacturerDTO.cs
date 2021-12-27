using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs
{
    [Table("manufacturer")]
    public class ManufacturerDTO
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string BarcodeEanPrefix { get; set; }
        public string Info { get; set; }

        public ManufacturerDTO DeepCopy()
        {
            ManufacturerDTO manufacturer = (ManufacturerDTO)this.MemberwiseClone();
            manufacturer.Id = this.Id;
            manufacturer.Name = this.Name;
            manufacturer.BarcodeEanPrefix = this.BarcodeEanPrefix;
            manufacturer.Info = this.Info;

            return manufacturer;
        }
    }
}
