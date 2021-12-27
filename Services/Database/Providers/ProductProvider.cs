using NaturalnieApp.Database;
using NaturalnieApp2.Interfaces.Database;
using NaturalnieApp2.Models;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class ProductProvider: DatabaseBase, IProductProvider
    {
        public ProductProvider(string connectionStrng): base(connectionStrng)
        {

        }

        //====================================================================================================
        //Method used to check if given element exist in database
        //Product name and barcode or supplier code are obligatory
        //Method will return existing entity. If nothing from perspective of given keys exist in DB, it will return null.
        //====================================================================================================
        public Product CheckIfProductExist(string productName, string barCode = "", string supplierCode = "")
        {
            ProductDTO entity = new ProductDTO();
            int step = 0;

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                //Check if product name already exist. If not, continue checking
                if (productName != "")
                {
                    //Create query to database
                    var queryProductName = from p in contextDB.Products
                                           where p.ProductName == productName
                                           select p;

                    entity = queryProductName.FirstOrDefault();
                    if ((entity == null) && (barCode != "")) step = 1;
                    else if ((entity == null) && (supplierCode != "")) step = 2;
                }
                //Next check bar code
                if (barCode != "" && step == 1)
                {
                    //Create query to database
                    var queryBarCode = from p in contextDB.Products
                                       where p.BarCode == barCode
                                       select p;

                    entity = queryBarCode.FirstOrDefault();
                    if ((entity == null) && (barCode != "")) step = 2;

                }
                //Next check supplier code
                if (supplierCode != "" && step == 2)
                {
                    //Create query to database
                    var querySupplierCode = from p in contextDB.Products
                                            where p.SupplierCode == supplierCode
                                            select p;

                    entity = querySupplierCode.FirstOrDefault();
                }
            }

            return GetProductFromProductDTO(entity);
        }

        public Product? GetProductFromProductDTO(ProductDTO productDTO)
        {
            if (productDTO == null) return null;
            return new Product()
            {
                ProductName = productDTO.ProductName,
                BarCode = productDTO.BarCode,
                SupplierCode = productDTO.SupplierCode,
                BarCodeShort = productDTO.BarCodeShort,
                Discount = productDTO.Discount,
                ElzabProductId = productDTO.ElzabProductId,
                ElzabProductName = productDTO.ElzabProductName,
                FinalPrice = productDTO.FinalPrice,
                ManufacturerName = new ManufacturerProvider(ConnectionString).GetManufacturerNameById(productDTO.ManufacturerId),
                Marigin = productDTO.Marigin,
                PriceNet = productDTO.PriceNet,
                PriceNetWithDiscount = productDTO.PriceNetWithDiscount,
                ProductInfo = productDTO.ProductInfo,
                SupplierName = new SupplierProvider(ConnectionString).GetSupplierNameById(productDTO.Id),
                TaxValue = new TaxProvider(ConnectionString).GetTaxValueById(productDTO.Id)
            };
        }
    }
}
