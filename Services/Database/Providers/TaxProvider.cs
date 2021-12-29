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
    internal class TaxProvider: DatabaseBase
    {
        public TaxProvider(string connectionStrng) : base(connectionStrng)
        {

        }

        //====================================================================================================
        //Method used to retrieve from DB all Tax ents
        //====================================================================================================
        public List<TaxDTO> GetAllTaxEnts()
        {
            List<TaxDTO> localTax = new List<TaxDTO>();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from t in contextDB.Tax
                            select t;

                localTax = query.ToList<TaxDTO>();
            }
            return localTax;
        }

        //====================================================================================================
        //Method used to retrieve from DB Tax value by Id
        //====================================================================================================
        public int GetTaxValueById(int id)
        {
            TaxDTO? localTax = new TaxDTO();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from t in contextDB.Tax
                            where t.Id == id
                            select t;

                localTax = query.SingleOrDefault();
            }
            if (localTax == null) return -1;
            return localTax.TaxValue;
        }

        //====================================================================================================
        //Method used to retrieve from DB TaxDTO entity
        //====================================================================================================
        public TaxDTO GetTaxEntityByValue(int value)
        {
            TaxDTO? localTax = new TaxDTO();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from t in contextDB.Tax
                            where t.TaxValue == value
                            select t;

                localTax = query.SingleOrDefault();
            }
            return localTax;
        }

        //====================================================================================================
        //Method used to retrieve from DB TaxDTO entity by ID
        //====================================================================================================
        public TaxDTO GetTaxEntityById(int Id)
        {
            TaxDTO? localTax = new TaxDTO();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from t in contextDB.Tax
                            where t.Id == Id
                            select t;

                localTax = query.SingleOrDefault();
            }
            return localTax;
        }

        //====================================================================================================
        //Method used to retrieve from DB TaxDTO list
        //====================================================================================================
        public List<string> GetTaxList()
        {
            List<string> taxList = new List<string>();

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                foreach (var TaxDTO in contextDB.Tax)
                {
                    taxList.Add(TaxDTO.TaxValue.ToString());
                }
            }
            return taxList;
        }

        //====================================================================================================
        //Method used to retrieve from DB TaxDTO list
        //====================================================================================================
        public List<string> GetTaxListRetString()
        {
            List<string> taxList = new List<string>();

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                foreach (var TaxDTO in contextDB.Tax)
                {
                    taxList.Add(TaxDTO.TaxValue.ToString());
                }
            }
            return taxList;
        }

        //====================================================================================================
        //Method used to retrieve from DB TaxDTO value using TaxDTO name
        //====================================================================================================
        public int GetTaxIdByValue(int taxValue)
        {
            int taxId = -1;

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from t in contextDB.Tax
                            where t.TaxValue == taxValue
                            select t.Id;

                taxId = query.SingleOrDefault();
            }

            return taxId;
        }

        //====================================================================================================
        //Method used to retrieve from DB Product entity
        //====================================================================================================
        public TaxDTO GetTaxByProductName(string productName)
        {
            TaxDTO localTax = new TaxDTO();
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                var query = from p in contextDB.Products
                            join t in contextDB.Tax
                            on p.TaxId equals t.Id
                            where p.ProductName == productName
                            select new
                            {
                                t
                            };

                foreach (var element in query)
                {
                    localTax = element.t;
                }

            }
            return localTax;
        }

        //====================================================================================================
        //Method used to edit table
        //====================================================================================================
        public void EditTax(TaxDTO TaxDTO)
        {
            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {
                contextDB.Tax.Add(TaxDTO);
                contextDB.Entry(TaxDTO).State = EntityState.Modified;
                int retVal = contextDB.SaveChanges();
            }
        }
    }
}
