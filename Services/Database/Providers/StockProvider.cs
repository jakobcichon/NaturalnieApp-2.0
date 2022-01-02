using NaturalnieApp.Database;
using NaturalnieApp2.Models;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class StockProvider: DatabaseBase
    {
        public StockProvider(string connectionStrng) : base(connectionStrng)
        {
        }

        //====================================================================================================
        //Method used to retrieve from DB quantity of given product in stock
        //====================================================================================================
        public int GetProductQuantityInStock(string productName)
        {
            int localQuantity = 0;

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from s in contextDB.Stock
                            join p in contextDB.Products
                            on s.ProductId equals p.Id
                            where p.ProductName == productName
                            select new
                            {
                                s
                            };
                foreach (var element in query)
                {
                    localQuantity += element.s.ActualQuantity;
                }
            }
            return localQuantity;
        }

        //====================================================================================================
        //Method used to get stock entity from user prepared stock entity
        //====================================================================================================
        public StockModel GetStockEntityByUserStock(StockModel stockProduct)
        {
            StockModel localStock = new StockModel();

            StockDTO stockDTO = GetStockDTOFromStockModel(stockProduct);

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from s in contextDB.Stock
                            where s.ProductId == stockDTO.ProductId &&
                            s.ExpirationDate == stockDTO.ExpirationDate &&
                            s.BarcodeWithDate == stockDTO.BarcodeWithDate
                            select s;

                stockDTO = query.FirstOrDefault();

                localStock = GetStockModelFromStockDTO(stockDTO);
            }
            return localStock;
        }

        //====================================================================================================
        //Method used to get stock entity from product ID
        //====================================================================================================
        public int GetStockQuantity(int productId)
        {
            int quantity = 0;

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from s in contextDB.Stock
                            where s.ProductId == productId
                            select s;

                foreach (StockDTO element in query)
                {
                    quantity += element.ActualQuantity;
                }

            }
            return quantity;
        }


        //====================================================================================================
        //Method used to get stock entity from with given manufacturer ID
        //====================================================================================================
        public List<StockModel> GetStockEntsWithManufacturerId(int manufacturerId)
        {

            List<StockModel> localStock = new List<StockModel>();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from s in contextDB.Stock
                            join p in contextDB.Products on s.ProductId equals p.Id
                            join m in contextDB.Manufacturers on p.ManufacturerId equals m.Id
                            where m.Id == manufacturerId
                            select new
                            {
                                s
                            };

                foreach (var element in query)
                {
                    localStock.Add(GetStockModelFromStockDTO(element.s));
                }

            }
            return localStock;
        }

        //====================================================================================================
        //Method used to get all stock entity
        //====================================================================================================
        public List<StockModel> GetAllStockEnts()
        {

            List<StockModel> localStock = new List<StockModel>();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from s in contextDB.Stock
                            select new
                            {
                                s
                            };

                foreach (var element in query)
                {
                    localStock.Add(GetStockModelFromStockDTO(element.s));
                }

            }
            return localStock;
        }


        //====================================================================================================
        //Method used to check if specified product already exist in stock
        //====================================================================================================
        public bool CheckIfProductExistInStock(StockModel stockProduct)
        {
            bool result = false;

            StockDTO stockDTO = GetStockDTOFromStockModel(stockProduct);

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from s in contextDB.Stock
                            where s.ProductId == stockDTO.ProductId &&
                            s.ExpirationDate == stockDTO.ExpirationDate &&
                            s.BarcodeWithDate == stockDTO.BarcodeWithDate
                            select s;

                if (query.FirstOrDefault() != null) result = true;
                else result = false;

            }
            return result;
        }

        //====================================================================================================
        //Method used to add to stock
        //====================================================================================================
        public void AddToStock(StockModel stockPiece)
        {
            StockDTO stockDTO = GetStockDTOFromStockModel(stockPiece);

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                contextDB.Stock.Add(stockDTO);
                int retVal = contextDB.SaveChanges();
            }
        }

        //====================================================================================================
        //Method used to edit product product in stock
        //====================================================================================================
        public void EditInStock(StockModel stockProduct)
        {
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                contextDB.Stock.Add(GetStockDTOFromStockModel(stockProduct));
                contextDB.Entry(stockProduct).State = EntityState.Modified;
                int retVal = contextDB.SaveChanges();
            }
        }


        public StockModel? GetStockModelFromStockDTO(StockDTO stockDTO, List<ProductDTO> productDTOs)
        {
            if (stockDTO == null) return null;

            return new StockModel()
            {
                ActualQuantity = stockDTO.ActualQuantity,
                BarcodeWithDate = stockDTO.BarcodeWithDate,
                ExpirationDate = stockDTO.ExpirationDate,
                LastQuantity = stockDTO.LastQuantity,
                ModificationDate = stockDTO.ModificationDate,
                ProductName = productDTOs.Find(e => e.Id == stockDTO.ProductId)?.ProductName
            };
        }

        public StockModel? GetStockModelFromStockDTO(StockDTO stockDTO)
        {
            if (stockDTO == null) return null;

            return new StockModel()
            {
                ActualQuantity = stockDTO.ActualQuantity,
                BarcodeWithDate = stockDTO.BarcodeWithDate,
                ExpirationDate = stockDTO.ExpirationDate,
                LastQuantity = stockDTO.LastQuantity,
                ModificationDate = stockDTO.ModificationDate,
                ProductName = new ProductProvider(ConnectionString).GetProductNameByProductId(stockDTO.Id)
            };
        }

        public StockDTO? GetStockDTOFromStockModel(StockModel stockModel, List<ProductDTO> productDTOs)
        {
            if (stockModel == null) return null;

            return new StockDTO()
            {
                ActualQuantity = stockModel.ActualQuantity,
                BarcodeWithDate = stockModel.BarcodeWithDate,
                ExpirationDate = stockModel.ExpirationDate,
                LastQuantity = stockModel.LastQuantity,
                ModificationDate = stockModel.ModificationDate,
                ProductId = productDTOs.Find(e => e.ProductName == stockModel.ProductName).Id
            };
        }

        public StockDTO? GetStockDTOFromStockModel(StockModel stockModel)
        {
            if (stockModel == null) return null;

            return new StockDTO()
            {
                ActualQuantity = stockModel.ActualQuantity,
                BarcodeWithDate = stockModel.BarcodeWithDate,
                ExpirationDate = stockModel.ExpirationDate,
                LastQuantity = stockModel.LastQuantity,
                ModificationDate = stockModel.ModificationDate,
                ProductId = new ProductProvider(ConnectionString).GetProductIdByProductName(stockModel.ProductName)
                };
        }
    }
}
