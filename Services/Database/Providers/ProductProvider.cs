﻿using NaturalnieApp.Database;
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

        //====================================================================================================
        //Method used to retrieve from DB all product entries
        //====================================================================================================
        public async Task<List<ProductModel>> GetAllProductEntitiesAsync()
        {
            List<ProductModel> localProduct = new List<ProductModel>();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from p in contextDB.Products
                            select p;

                var test = query;
                List<ProductDTO> productDTOs = await query.ToListAsync();

                localProduct = GetProductFromProductDTO(productDTOs);
            }
            return localProduct;
        }


        #region Interface elements
        public List<string> GetElementsNames()
        {
            List<PropertyDescriptor> propertiesList = DisplayModelAttributesServices.GetPropertiesToBeDisplayed(typeof(ProductModel));
            List<string> returnList = propertiesList.Select(p => DisplayModelAttributesServices.GetPropertyDisplayName(p)).ToList();

            return returnList;
        }

        public Dictionary<string, object> GetElementsValues()
        {
            List<ProductModel> productModels = GetAllProductEntities();
            Dictionary<string, object> _returnDict = new Dictionary<string, object>();

            foreach (string propertyName in GetElementsNames())
            {
                List<object> allPropertyValues = new List<object>();
                foreach (ProductModel model in productModels)
                {
                    allPropertyValues.Add(DisplayModelAttributesServices.GetPropertyValueByDisplayName(propertyName, model));
                }
                allPropertyValues = allPropertyValues.Distinct().ToList();
                _returnDict.Add(propertyName, allPropertyValues);
            }

            return _returnDict;

        }

        public async Task<Dictionary<string, object>> GetElementsValuesAsync()
        {
            List<ProductModel> productModels = await GetAllProductEntitiesAsync();

            Dictionary<string, object> _returnDict = new Dictionary<string, object>();

            foreach (string propertyName in GetElementsNames())
            {
                List<object> allPropertyValues = new List<object>();
                foreach (ProductModel model in productModels)
                {
                    allPropertyValues.Add(DisplayModelAttributesServices.GetPropertyValueByDisplayName(propertyName, model));
                }
                allPropertyValues = allPropertyValues.Distinct().ToList();
                _returnDict.Add(propertyName, allPropertyValues);
            }

            return _returnDict;

        }
        #endregion


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

    }
}
