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
using NaturalnieApp2.Services.Attributes;
using System.ComponentModel;
using System.Diagnostics;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class ProductProvider: DatabaseBase<ProductProvider>, IProductProvider, IModelProvider
    {
        public Type ModelType { get; init; }

        public ProductProvider(ShopContext shopContext) : base(shopContext)
        {
            //ModelType = modelType;
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


            //Check if product name already exist. If not, continue checking
            if (productName != "")
            {
                //Create query to database
                var queryProductName = from p in ShopContext.Products
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
                var queryBarCode = from p in ShopContext.Products
                                    where p.BarCode == barCode
                                    select p;

                entity = queryBarCode.FirstOrDefault();
                if ((entity == null) && (barCode != "")) step = 2;

            }
            //Next check supplier code
            if (supplierCode != "" && step == 2)
            {
                //Create query to database
                var querySupplierCode = from p in ShopContext.Products
                                        where p.SupplierCode == supplierCode
                                        select p;

                entity = querySupplierCode.FirstOrDefault();
            }
            

            //return GetProductFromProductDTO(entity);
            return null;
        }

        //====================================================================================================
        //Method used to retrieve from DB Product entity
        //====================================================================================================
        public ProductModel GetProductEntityByProductId(int productId)
        {
            ProductModel localProduct = new ProductModel();

            var query = from p in ShopContext.Products.Include(p => p.Tax).
                        Include(p => p.Supplier).
                        Include(p => p.Manufacturer)
                        where p.Id == productId
                        select p;

            ProductDTO productDTOs = query.SingleOrDefault();

            localProduct = GetProductFromProductDTO(productDTOs);

            return localProduct;
        }

        //====================================================================================================
        //Method used to retrieve from DB Product id by product Name
        //====================================================================================================
        public int? GetProductIdByProductName(string productName)
        {

            var query = from p in ShopContext.Products.Include(p => p.Tax).
                        Include(p => p.Supplier).
                        Include(p => p.Manufacturer)
                        where p.ProductName == productName
                        select p;

            ProductDTO? productDTOs = query.SingleOrDefault();
            if (productDTOs == null) return null;

            return productDTOs?.Id;

        }

        //====================================================================================================
        //Method used to retrieve from DB Product Name by product id
        //====================================================================================================
        public string GetProductNameByProductId(int productId)
        {

            var query = from p in ShopContext.Products.Include(p => p.Tax).
                        Include(p => p.Supplier).
                        Include(p => p.Manufacturer)
                        where p.Id == productId
                        select p;

            ProductDTO productDTOs = query.SingleOrDefault();

            return productDTOs.ProductName;

        }

        //====================================================================================================
        //Method used to retrieve from DB Product entity
        //====================================================================================================
        public ProductModel GetProductEntityByProductName(string productName)
        {
            ProductModel localProduct = new ProductModel();

            var query = from p in ShopContext.Products.Include(p => p.Tax).
                        Include(p => p.Supplier).
                        Include(p => p.Manufacturer)
                        where p.ProductName == productName
                        select p;

            ProductDTO productDTO = query.SingleOrDefault();

            localProduct = GetProductFromProductDTO(productDTO);

            return localProduct;
        }

        //====================================================================================================
        //Method used to retrieve from DB all product entries
        //====================================================================================================
        public List<ProductModel> GetAllProductEntities()
        {
            List<ProductModel> localProduct = new List<ProductModel>();

            var query = from p in ShopContext.Products.Include(p => p.Tax).
                        Include(p => p.Supplier).
                        Include(p => p.Manufacturer)
                        select p;

            List<ProductDTO> productDTOs = query.ToList();

            localProduct = GetProductFromProductDTO(productDTOs);

            return localProduct;
        }

        //====================================================================================================
        //Method used to retrieve from DB all product entries
        //====================================================================================================
        public async Task<List<ProductModel>> GetAllProductEntitiesAsync()
        {
            List<ProductModel> localProduct = new List<ProductModel>();

            var query = from p in ShopContext.Products.Include(p => p.Tax).
                        Include(p => p.Supplier).
                        Include(p => p.Manufacturer)
                        select p;

            var test = query;
            List<ProductDTO> productDTOs = await query.ToListAsync();

            localProduct = GetProductFromProductDTO(productDTOs);

            return localProduct;
        }


        //====================================================================================================
        //Method used to retrieve from DB Product entity by Barcode value
        //====================================================================================================
        public ProductDTO? GetProductEntityByBarcode(string barcode)
        {
            ProductDTO? localProduct = new ProductDTO();

            var query = from p in ShopContext.Products.Include(p => p.Tax).
                        Include(p => p.Supplier).
                        Include(p => p.Manufacturer)
                        where p.BarCode == barcode
                        select p;

            localProduct = query?.SingleOrDefault();

            return localProduct;
        }

        //====================================================================================================
        //Method used to add new product
        //====================================================================================================
        public void AddNewProduct(ProductModel product)
        {
            //Local variables
            ProductDTO? localProductDTO = GetProductDTOFromProductModel(product);

            if (localProductDTO == null) throw new ArgumentNullException();

            ShopContext.Products.Add(localProductDTO);
            ShopContext.SaveChanges();

            localProductDTO = GetProductEntityByBarcode(product.BarCode);

            /// UNDONE: Dorobić zapis changelog:

            /*if (localProductDTO == null) AddProductToChangelog(product, ProductOperationType.AddNew);
            else AddProductToChangelog(localProductDTO, ProductOperationType.AddNew);*/

            OnModelChange(this, this.GetType(), this.ToString());
        }


        #region Interface elements

        #endregion

        private (List<ManufacturerDTO>, List<SupplierDTO>, List<TaxDTO>) GetAuxiliaryProviders()
        {
            List<ManufacturerDTO> manufacturerDTOs = new ManufacturerProvider(ShopContext).GetAllManufacturersEnts();
            List<SupplierDTO> supplierDTOs = new SupplierProvider(ShopContext).GetAllSupplierEnts();
            List<TaxDTO> taxDTOs = new TaxProvider(ShopContext).GetAllTaxEnts();

            return (manufacturerDTOs, supplierDTOs, taxDTOs);
        }

        #region Conversion from Model to DTO
        public List<ProductModel> GetProductFromProductDTO(List<ProductDTO> productsDTO)
        {
            if (productsDTO == null) return null;
            List<ProductModel> localProduct = new List<ProductModel>();
           
            foreach (ProductDTO productDTO in productsDTO)
            {
                ProductModel? _product = GetProductFromProductDTO(productDTO);
                if(_product != null) localProduct.Add(_product);
            }
              
            return localProduct;
        }

        public ProductModel? GetProductFromProductDTO(ProductDTO productDTO)
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
                ManufacturerName = productDTO.Manufacturer.Name,
                Marigin = productDTO.Marigin,
                PriceNet = productDTO.PriceNet,
                PriceNetWithDiscount = productDTO.PriceNetWithDiscount,
                ProductInfo = productDTO.ProductInfo,
                SupplierName = productDTO.Supplier.Name,
                TaxValue = productDTO.Tax.TaxValue
            };
        }
        #endregion

        #region Conversion from DTO to Model
        public List<ProductDTO> GetProductDTOFromProductModel(List<ProductModel> productModels)
        {

            if (productModels == null) return null;
            List<ProductDTO> localProduct = new();

            foreach (ProductModel productModel in productModels)
            {
                ProductDTO? _product = GetProductDTOFromProductModel(productModel);
                if (_product != null) localProduct.Add(_product);
            }

            return localProduct;
        }

        public ProductDTO GetProductDTOFromProductModel(ProductModel productModel)
        {
            if (productModel == null) return null;
            ProductDTO localProduct = new ProductDTO();
            (List<ManufacturerDTO> manufacturerDTOs, List<SupplierDTO> supplierDTOs, List<TaxDTO> taxDTOs) = GetAuxiliaryProviders();

            ProductDTO? _product = GetProductDTOFromProductModel(productModel, manufacturerDTOs, supplierDTOs, taxDTOs);

            return localProduct;
        }

        public ProductDTO? GetProductDTOFromProductModel(ProductModel productModel,
            List<ManufacturerDTO> manufacturerDTOs,
            List<SupplierDTO> supplierDTOs,
            List<TaxDTO> taxDTOs)
        {
            if (productModel == null) return null;
            return new ProductDTO()
            {
                ProductName = productModel.ProductName,
                BarCode = productModel.BarCode,
                SupplierCode = productModel.SupplierCode,
                BarCodeShort = productModel.BarCodeShort,
                Discount = productModel.Discount,
                ElzabProductId = productModel.ElzabProductId,
                ElzabProductName = productModel.ElzabProductName,
                FinalPrice = productModel.FinalPrice,
                ManufacturerId = manufacturerDTOs.Find(e => e.Name == productModel.ManufacturerName)?.Id ?? 0,
                Marigin = productModel.Marigin,
                PriceNet = productModel.PriceNet,
                PriceNetWithDiscount = productModel.PriceNetWithDiscount,
                ProductInfo = productModel.ProductInfo,
                SupplierId = supplierDTOs.Find(e => e.Name == productModel.SupplierName)?.Id ?? 0,
                TaxId = taxDTOs.Find(e => e.TaxValue == productModel.TaxValue)?.Id ?? 0
            };
        }

        public TmodelDTO GetModelFromModelDTO<TmodelDTO, Tmodel>()
            where TmodelDTO : class
            where Tmodel : class
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
