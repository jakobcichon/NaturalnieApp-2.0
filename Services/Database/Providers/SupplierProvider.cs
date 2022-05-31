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
    internal class SupplierProvider: DatabaseBase<SupplierProvider>
    {
        public SupplierProvider(ShopContext shopContext) : base(shopContext)
        {

        }

        //====================================================================================================
        //Method used to retrieve from DB all supplier ents
        //====================================================================================================
        public List<SupplierDTO> GetAllSupplierEnts()
        {
            List<SupplierDTO> localSupplier = new List<SupplierDTO>();

            var query = from s in ShopContext.Suppliers
                        select s;

            localSupplier = query.ToList<SupplierDTO>();
            
            return localSupplier;
        }

        //====================================================================================================
        //Method used to retrieve from DB supplier name by ID
        //====================================================================================================
        public string GetSupplierNameById(int id)
        {
            SupplierDTO? localSupplier = new SupplierDTO();

            var query = from m in ShopContext.Suppliers
                        where m.Id == id
                        select m;

            localSupplier = query.SingleOrDefault();
            
            if (localSupplier == null) return "";
            return localSupplier.Name;
        }

        //====================================================================================================
        //Method used to retrieve from DB SupplierDTO entity
        //====================================================================================================
        public SupplierDTO? GetSupplierEntityByName(string supplierName)
        {
            SupplierDTO? localSupplier = new SupplierDTO();

            var query = from m in ShopContext.Suppliers
                        where m.Name == supplierName
                        select m;

            localSupplier = query.SingleOrDefault();
            
            return localSupplier;
        }

        //====================================================================================================
        //Method used to retrieve from DB Product and SupplierDTO entity
        //====================================================================================================
        public SupplierDTO? GetSupplierByProductName(string productName)
        {
            SupplierDTO localSupplier = new SupplierDTO();


            var query = from p in ShopContext.Products
                        join s in ShopContext.Suppliers
                        on p.SupplierId equals s.Id
                        where p.ProductName == productName
                        select new
                        {
                            s
                        };

            foreach (var element in query)
            {
                localSupplier = element.s;
            }
            
            return localSupplier;
        }

        //====================================================================================================
        //Method used to add new SupplierDTO
        //====================================================================================================
        public void AddSupplier(SupplierDTO SupplierDTO)
        {

            ShopContext.Suppliers.Add(SupplierDTO);
            int retVal = ShopContext.SaveChanges();

            
        }

        //====================================================================================================
        //Method used to edit product
        //====================================================================================================
        public void EditSupplier(SupplierDTO SupplierDTO)
        {

            ShopContext.Suppliers.Add(SupplierDTO);
            ShopContext.Entry(SupplierDTO).State = EntityState.Modified;
            int retVal = ShopContext.SaveChanges();
            
        }
    }
}
