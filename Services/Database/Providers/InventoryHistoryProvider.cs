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
    internal class InventoryHistoryProvider: DatabaseBase<InventoryHistoryProvider>
    {

        internal InventoryHistoryProvider(ShopContext shopContext): base(shopContext)
        {
        }

      
        //====================================================================================================
        //Method used to add to inventory
        //====================================================================================================
        public void AddToInventoryHistory(InventoryDTO inventoryDTO, HistoryOperationType operationType)
        {

            InventoryHistoryDTO inventoryHistoryDTO = GetInventoryHistoryDTOFromInventoryDTO(inventoryDTO);
            inventoryHistoryDTO.OperationType = operationType.ToString();
            inventoryHistoryDTO.OperationDateTime = DateTime.Now;

            ShopContext.InventoryHistory.Add(inventoryHistoryDTO);
            int retVal = ShopContext.SaveChanges();

        }

        public InventoryHistoryDTO GetInventoryHistoryDTOFromInventoryDTO(InventoryDTO inventoryDTO)
        {
            return ModelConvertions<InventoryDTO, InventoryHistoryDTO>.ConvertModels(inventoryDTO);
        }

    }
}
