using NaturalnieApp.Database;
using NaturalnieApp2.Models.MenuScreens.Inventory;
using NaturalnieApp2.Services.DataModel;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class InventoryProvider: DatabaseBase
    {
        internal InventoryProvider(ShopContext shopContext): base(shopContext)
        {

        }

        public List<InventoryModel> GetAllInventoryEntitiesByInventoryName(string? inventoryName)
        {
            List<InventoryModel> localInventoryModel = new List<InventoryModel>();
            List<InventoryDTO> inventoryDTOs = new List<InventoryDTO>();

            if (inventoryName == null) return localInventoryModel;


            var query = from i in ShopContext.Inventory
                        where i.InventoryName == inventoryName
                        select i;

            inventoryDTOs = query.ToList();


            foreach(InventoryDTO inventoryDTO in inventoryDTOs)
            {
                localInventoryModel.Add(GetInventoryModelFromInventoryDTO(inventoryDTO));
            }

            return localInventoryModel;
        }

        public bool CheckIfInventoryWithGivenNameExist(string inventoryName)
        {

            var query = from i in ShopContext.Inventory
                        where i.InventoryName == inventoryName
                        select i;

            InventoryDTO inventoryDTOs = query.SingleOrDefault();

            if (inventoryDTOs != null) return true;
            return false;


        }

        public InventoryModel? CheckIfInventoryModelExist(InventoryModel inventoryModel)
        {
            InventoryDTO inventoryDTO = GetInventoryDTOFromInventoryModel(inventoryModel);
            InventoryModel localInventoryModel;

            var query = from i in ShopContext.Inventory
                        where i.InventoryName == inventoryDTO.InventoryName &&
                        i.ProductName == inventoryDTO.ProductName
                        select i;

            inventoryDTO = query.FirstOrDefault();


            if (inventoryDTO == null) return null;
            localInventoryModel = GetInventoryModelFromInventoryDTO(inventoryDTO);
            return localInventoryModel;
        }

        public InventoryDTO? GetExistingInventoryDTOFromInventoryModel(InventoryModel inventoryModel)
        {
            InventoryDTO inventoryDTO = GetInventoryDTOFromInventoryModel(inventoryModel);

            var query = from i in ShopContext.Inventory
                        where i.InventoryName == inventoryDTO.InventoryName &&
                        i.ProductName == inventoryDTO.ProductName
                        select i;

            inventoryDTO = query.FirstOrDefault();


            return inventoryDTO;
        }

        public List<string>? GetInventoryNames()
        {
            InventoryModel localProduct = new InventoryModel();

            var query = (from i in ShopContext.Inventory
                        select i.InventoryName).Distinct();

            List<string> inventoryNames = query.ToList();

            return inventoryNames;

        }

        //====================================================================================================
        //Method used to add to inventory
        //====================================================================================================
        public void AddToInventory(InventoryModel inventoryModel)
        {
            InventoryDTO inventoryDTO = GetInventoryDTOFromInventoryModel(inventoryModel);

            inventoryDTO.LastModificationDate = DateTime.Now;

            ShopContext.Inventory.Add(inventoryDTO);
            int retVal = ShopContext.SaveChanges();

            AddOperationToInventoryHistory(inventoryDTO, HistoryOperationType.AddedNew);
        }

        //====================================================================================================
        //Method used to edit inventory
        //====================================================================================================
        public void EditInInventory(InventoryModel inventoryModel)
        {
            InventoryDTO inventoryDTO = GetExistingInventoryDTOFromInventoryModel(inventoryModel);
            if (inventoryDTO == null) throw new Exception("InventoryDTO was not found in Database");

            OverwriteInventoryDTOWithInventoryModelData(inventoryModel, inventoryDTO);

            inventoryDTO.LastModificationDate = DateTime.Now;

            ShopContext.Inventory.Add(inventoryDTO);
            ShopContext.Entry(inventoryDTO).State = EntityState.Modified;
            int retVal = ShopContext.SaveChanges();

            AddOperationToInventoryHistory(inventoryDTO, HistoryOperationType.Modified);
        }

        public void AddOperationToInventoryHistory(InventoryDTO inventoryDTO, HistoryOperationType operationType)
        {
            new InventoryHistoryProvider(ShopContext).AddToInventoryHistory(inventoryDTO, operationType);
        }

        public InventoryDTO GetInventoryDTOFromInventoryModel(InventoryModel inventoryModel)
        {
            return ModelConvertions<InventoryModel, InventoryDTO>.ConvertModels(inventoryModel);
        }

        public InventoryModel GetInventoryModelFromInventoryDTO(InventoryDTO inventoryDTO)
        {
            return ModelConvertions<InventoryDTO, InventoryModel>.ConvertModels(inventoryDTO);
        }

        public void OverwriteInventoryDTOWithInventoryModelData(InventoryModel inventoryModel, InventoryDTO inventoryDTO)
        {
            ModelConvertions<InventoryModel, InventoryDTO>.OverwriteModels(inventoryModel, inventoryDTO);
        }
    }
}
