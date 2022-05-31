using NaturalnieApp.Database;
using NaturalnieApp2.Interfaces.Services;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database.Providers;

internal class TaxProvider: DatabaseBase<TaxProvider>, IHintListProvider<List<int>>
{
    public TaxProvider(ShopContext shopContext) : base(shopContext)
    {

    }

    //====================================================================================================
    //Method used to retrieve from DB all Tax ents
    //====================================================================================================
    public List<TaxDTO> GetAllTaxEnts()
    {
        List<TaxDTO> localTax = new List<TaxDTO>();

        var query = from t in ShopContext.Tax
                    select t;

        localTax = query.ToList<TaxDTO>();
            
        return localTax;
    }

    //====================================================================================================
    //Method used to retrieve from DB Tax value by Id
    //====================================================================================================
    public int GetTaxValueById(int id)
    {
        TaxDTO? localTax = new TaxDTO();

        var query = from t in ShopContext.Tax
                    where t.Id == id
                    select t;

        localTax = query.SingleOrDefault();
        if (localTax == null) return -1;
        return localTax.TaxValue;
    }

    //====================================================================================================
    //Method used to retrieve from DB TaxDTO entity
    //====================================================================================================
    public TaxDTO GetTaxEntityByValue(int value)
    {
        TaxDTO? localTax = new TaxDTO();
        var query = from t in ShopContext.Tax
                    where t.TaxValue == value
                    select t;

        localTax = query.SingleOrDefault();
            
        return localTax;
    }

    //====================================================================================================
    //Method used to retrieve from DB TaxDTO entity by ID
    //====================================================================================================
    public TaxDTO GetTaxEntityById(int Id)
    {
        TaxDTO? localTax = new TaxDTO();

        var query = from t in ShopContext.Tax
                    where t.Id == Id
                    select t;

        localTax = query.SingleOrDefault();
            
        return localTax;
    }

    //====================================================================================================
    //Method used to retrieve from DB TaxDTO list
    //====================================================================================================
    public List<string> GetTaxList()
    {
        List<string> taxList = new List<string>();

        foreach (var TaxDTO in ShopContext.Tax)
        {
            taxList.Add(TaxDTO.TaxValue.ToString());
        }
            
        return taxList;
    }

    //====================================================================================================
    //Method used to retrieve from DB TaxDTO list
    //====================================================================================================
    public List<string> GetTaxListRetString()
    {
        List<string> taxList = new List<string>();

        foreach (var TaxDTO in ShopContext.Tax)
        {
            taxList.Add(TaxDTO.TaxValue.ToString());
        }
            
        return taxList;
    }

    //====================================================================================================
    //Method used to retrieve from DB TaxDTO value using TaxDTO name
    //====================================================================================================
    public int GetTaxIdByValue(int taxValue)
    {
        int taxId = -1;

        var query = from t in ShopContext.Tax
                    where t.TaxValue == taxValue
                    select t.Id;

        taxId = query.SingleOrDefault();
            

        return taxId;
    }

    //====================================================================================================
    //Method used to retrieve from DB Product entity
    //====================================================================================================
    public TaxDTO GetTaxByProductName(string productName)
    {
        TaxDTO localTax = new TaxDTO();

        var query = from p in ShopContext.Products
                    join t in ShopContext.Tax
                    on p.Tax.Id equals t.Id
                    where p.ProductName == productName
                    select new
                    {
                        t
                    };

        foreach (var element in query)
        {
            localTax = element.t;
        }

            
        return localTax;
    }

    //====================================================================================================
    //Method used to edit table
    //====================================================================================================
    public void EditTax(TaxDTO TaxDTO)
    {

        ShopContext.Tax.Add(TaxDTO);
        ShopContext.Entry(TaxDTO).State = EntityState.Modified;
        int retVal = ShopContext.SaveChanges();
            
    }

    /// <summary>
    /// Method used to return hint list of TaxValues
    /// </summary>
    /// <returns>Hint list of tax values</returns>
    public List<int>? GetHintList()
    {
        var query = from t in ShopContext.Tax
                    select t.TaxValue;

        return query.ToList();
    }
}
