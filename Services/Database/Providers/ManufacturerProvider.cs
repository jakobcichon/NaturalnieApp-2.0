using NaturalnieApp.Database;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class ManufacturerProvider: DatabaseBase<ManufacturerProvider>
    {
        public ManufacturerProvider(ShopContext shopContext) : base(shopContext)
        {
            
        }

        //====================================================================================================
        //Method used to add new manufacturer
        //====================================================================================================
        public void AddManufacturer(ManufacturerDTO manufacturer)
        {
            ShopContext.Manufacturers.Add(manufacturer);
            int retVal = ShopContext.SaveChanges();
        }

        //====================================================================================================
        //Method used to remove manufacturer
        //====================================================================================================
        public bool DeleteManufacturer(string manufacturerName)
        {
            bool retVal = false;

            ManufacturerDTO localEntity = this.GetManufacturerEntityByName(manufacturerName);

            if (localEntity != null)
            {
                ManufacturerDTO manufacturerToDelete = new ManufacturerDTO { Id = localEntity.Id };
                ShopContext.Entry(manufacturerToDelete).State = EntityState.Deleted;
                int retValInt = ShopContext.SaveChanges();
                if (retValInt > 0) retVal = true;
            }

            return retVal;
        }

        //====================================================================================================
        //Method used to retrieve from DB ManufacturerDTO name by Id
        //====================================================================================================
        public string GetManufacturerNameById(int id)
        {
            ManufacturerDTO localManufacturer = new ManufacturerDTO();

            var query = from m in ShopContext.Manufacturers
                        where m.Id == id
                        select m;

            localManufacturer = query.SingleOrDefault();

            return localManufacturer.Name;
        }

        //====================================================================================================
        //Method used to retrieve from DB ManufacturerDTO name list
        //====================================================================================================
        public List<string> GetManufacturersNameList()
        {
            List<string> manufacturersList = new List<string>();

            foreach (var ManufacturerDTO in ShopContext.Manufacturers)
            {
                manufacturersList.Add(ManufacturerDTO.Name);
            }

            return manufacturersList;
        }

        //====================================================================================================
        //Method used to retrieve from DB ManufacturerDTO entity
        //====================================================================================================
        public ManufacturerDTO GetManufacturerEntityByName(string manufacturerName)
        {
            ManufacturerDTO localManufacturer = new ManufacturerDTO();
            var query = from m in ShopContext.Manufacturers
                        where m.Name == manufacturerName
                        select m;

            localManufacturer = query.SingleOrDefault();

            return localManufacturer;
        }
        //====================================================================================================
        //Method used to retrieve from DB ManufacturerDTO entity by ID
        //====================================================================================================
        public ManufacturerDTO GetManufacturerEntityById(int manufacturerId)
        {
            ManufacturerDTO localManufacturer = new ManufacturerDTO();

            var query = from m in ShopContext.Manufacturers
                        where m.Id == manufacturerId
                        select m;

            localManufacturer = query.SingleOrDefault();

            return localManufacturer;
        }

        //====================================================================================================
        //Method used to retrieve from DB Product entity
        //====================================================================================================
        public ManufacturerDTO GetManufacturerByProductName(string productName)
        {
            ManufacturerDTO localManufacturer = new ManufacturerDTO();

            var query = from p in ShopContext.Products
                        join m in ShopContext.Manufacturers
                        on p.ManufacturerId equals m.Id
                        where p.ProductName == productName
                        select new
                        {
                            m
                        };

            foreach (var element in query)
            {
                localManufacturer = element.m;
            }

            return localManufacturer;
        }
        //====================================================================================================
        //Method used to retrieve from DB ManufacturerDTO entity
        //====================================================================================================

        public ManufacturerDTO GetManufacturerByBarcode(string barcode)
        {
            ManufacturerDTO localManufacturer = new ManufacturerDTO();

            var query = from p in ShopContext.Products
                        join m in ShopContext.Manufacturers
                        on p.ManufacturerId equals m.Id
                        where p.BarCode == barcode
                        select new
                        {
                            m
                        };

            foreach (var element in query)
            {
                localManufacturer = element.m;
            }

            return localManufacturer;
        }
        //====================================================================================================
        //Method used to retrieve from DB all Manufacturers ents
        //====================================================================================================
        public List<ManufacturerDTO> GetAllManufacturersEnts()
        {
            List<ManufacturerDTO> manufacturersList = new List<ManufacturerDTO>();

            foreach (var manufacturer in ShopContext.Manufacturers)
            {
                manufacturersList.Add(manufacturer);
            }

            return manufacturersList;
        }

        //====================================================================================================
        //Method used to retrieve from DB manufacturer EAN barcode prefix, if exist
        //====================================================================================================
        public string GetManufacturerEanPrefixByName(string manufacturerName)
        {
            string eanPrefix = "";

            var query = from m in ShopContext.Manufacturers
                        where m.Name == manufacturerName
                        select m.BarcodeEanPrefix;

            eanPrefix = query.SingleOrDefault();

            return eanPrefix;
        }
        //====================================================================================================
        //Method used to retrieve from DB product name list, fitered by a specific manufacturer
        //====================================================================================================
        public List<string> GetProductsNameListByManufacturer(string manufacturerName)
        {
            List<string> productList = new List<string>();

            //Create query to database
            var query = from p in ShopContext.Products
                        join m in ShopContext.Manufacturers
                        on p.ManufacturerId equals m.Id
                        where m.Name == manufacturerName
                        select p;

            //Add product names to the list
            foreach (var products in query)
            {
                productList.Add(products.ProductName);
            }

            return productList;
        }

        //====================================================================================================
        //Method used to retrieve from DB barcode list, fitered by a specific manufacturer
        //====================================================================================================
        public List<string> GetBarcodesListByManufacturer(string manufacturerName)
        {
            List<string> productList = new List<string>();

            //Create query to database
            var query = from p in ShopContext.Products
                        join m in ShopContext.Manufacturers
                        on p.ManufacturerId equals m.Id
                        where m.Name == manufacturerName
                        select p;

            //Add product names to the list
            foreach (var products in query)
            {
                productList.Add(products.BarCode);
            }

            return productList;
        }
        //====================================================================================================
        //Method used to retrieve from DB ManufacturerDTO value using ManufacturerDTO name
        //====================================================================================================
        public int GetManufacturerIdByName(string manufacturerName)
        {
            int manufacturerId = -1;

            var query = from m in ShopContext.Manufacturers
                        where m.Name == manufacturerName
                        select m.Id;

            manufacturerId = query.SingleOrDefault();
           

            return manufacturerId;
        }

        //====================================================================================================
        //Method used to retrieve from DB all Manufacturers Ids
        //====================================================================================================
        public List<int> GetAllManufacturersId()
        {
            List<int> manufacturesrList = new List<int>();

            foreach (var manufacturer in ShopContext.Manufacturers)
            {
                manufacturesrList.Add(manufacturer.Id);
            }

            return manufacturesrList;
        }
        //====================================================================================================
        //Method used to check  if in DB specified ManufacturerDTO Name exist
        //====================================================================================================
        public bool CheckIfManufacturerNameExist(string manufacturerName)
        {
            bool result = false;

            var query = from m in ShopContext.Manufacturers
                        where m.Name == manufacturerName
                        select m;

            if (query.FirstOrDefault() != null) result = true;
            else result = false;



            return result;
        }

    }
}
