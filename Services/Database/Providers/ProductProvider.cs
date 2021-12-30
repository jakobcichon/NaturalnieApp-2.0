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
using NaturalnieApp2.Attributes;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class ProductProvider: DatabaseBase, IProductProvider, IGetModelProvider
    {
        public ProductProvider(string connectionStrng): base(connectionStrng)
        {

        }

        //====================================================================================================
        //Method used to check if given element exist in database
        //Product name and barcode or supplier code are obligatory
        //Method will return existing entity. If nothing from perspective of given keys exist in DB, it will return null.
        //====================================================================================================
        public ProductModel CheckIfProductExist(string productName, string barCode = "", string supplierCode = "")
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

            //return GetProductFromProductDTO(entity);
            return null;
        }

        //====================================================================================================
        //Method used to retrieve from DB all product entries
        //====================================================================================================
        public List<ProductModel> GetAllProductEntities()
        {
            List<ProductModel> localProduct = new List<ProductModel>();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from p in contextDB.Products
                            select p;

                List<ProductDTO> productDTOs = query.ToList();

                localProduct = GetProductFromProductDTO(productDTOs);
            }
            return localProduct;
        }

        public List<ProductModel> GetProductFromProductDTO(List<ProductDTO> productsDTO)
        {
            if (productsDTO == null) return null;
            List<ProductModel> localProduct = new List<ProductModel>();
            List<ManufacturerDTO> manufacturerDTOs = new ManufacturerProvider(ConnectionString).GetAllManufacturersEnts();
            List<SupplierDTO> supplierDTOs = new SupplierProvider(ConnectionString).GetAllSupplierEnts();
            List<TaxDTO> taxDTOs = new TaxProvider(ConnectionString).GetAllTaxEnts();

            foreach (ProductDTO productDTO in productsDTO)
            {
                ProductModel? _product = GetProductFromProductDTO(productDTO, manufacturerDTOs, supplierDTOs, taxDTOs);
                if(_product != null) localProduct.Add(_product);
            }
              
            return localProduct;
        }

        public ProductModel? GetProductFromProductDTO(ProductDTO productDTO, 
            List<ManufacturerDTO> manufacturerDTOs,
            List<SupplierDTO> supplierDTOs,
            List<TaxDTO> taxDTOs)
        {
            if (productDTO == null) return null;
            return new ProductModel()
            {
                ProductName = productDTO.ProductName,
                BarCode = productDTO.BarCode,
                SupplierCode = productDTO.SupplierCode,
                BarCodeShort = productDTO.BarCodeShort,
                Discount = productDTO.Discount,
                ElzabProductId = productDTO.ElzabProductId,
                ElzabProductName = productDTO.ElzabProductName,
                FinalPrice = productDTO.FinalPrice,
                ManufacturerName = manufacturerDTOs.Find(e => e.Id == productDTO.ManufacturerId)?.Name,
                Marigin = productDTO.Marigin,
                PriceNet = productDTO.PriceNet,
                PriceNetWithDiscount = productDTO.PriceNetWithDiscount,
                ProductInfo = productDTO.ProductInfo,
                SupplierName = supplierDTOs.Find(e => e.Id == productDTO.SupplierId)?.Name,
                TaxValue = taxDTOs.Find(e => e.Id == productDTO.TaxId).TaxValue
            };
        }

        public Type GetModelType()
        {
            return typeof(ProductModel);
        }

        public List<object> GetAllModelData()
        {
            return GetAllProductEntities().ConvertAll(e => e as object);
        }
    }
}
