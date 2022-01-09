using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs
{
    [Table("product")]
    public class ProductDTO
    {
        [Key]
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int ElzabProductId { get; set; }
        public int ManufacturerId { get; set; }
        public string ProductName { get; set; }
        public string ElzabProductName { get; set; }
        public float PriceNet { get; set; }
        public int Discount { get; set; }
        public float PriceNetWithDiscount { get; set; }
        public int TaxId { get; set; }
        public int Marigin { get; set; }
        public float FinalPrice { get; set; }
        public string BarCode { get; set; }
        public string BarCodeShort { get; set; }
        public string SupplierCode { get; set; }
        public string ProductInfo { get; set; }

        public ProductDTO DeepCopy()
        {
            ProductDTO product = (ProductDTO)this.MemberwiseClone();
            product.Id = this.Id;
            product.SupplierId = this.SupplierId;
            product.ElzabProductId = this.ElzabProductId;
            product.ManufacturerId = this.ManufacturerId;
            product.ProductName = this.ProductName;
            product.ElzabProductName = this.ElzabProductName;
            product.PriceNet = this.PriceNet;
            product.Discount = this.Discount;
            product.PriceNetWithDiscount = this.PriceNetWithDiscount;
            product.TaxId = this.TaxId;
            product.Marigin = this.Marigin;
            product.FinalPrice = this.FinalPrice;
            product.BarCode = this.BarCode;
            product.BarCodeShort = this.BarCodeShort;
            product.SupplierCode = this.SupplierCode;
            product.ProductInfo = this.ProductInfo;
            return product;
        }
    }

    public enum ProductOperationType
    {
        AddNew,
        Update,
        Delete
    }
    public enum StockOperationType
    {
        AddNew,
        Update,
        Delete,
        AutomaticUpdate,
        AutomaticAddedNew,
        InventoryData
    }

}