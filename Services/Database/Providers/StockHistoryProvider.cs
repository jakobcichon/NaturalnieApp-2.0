using NaturalnieApp.Database;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class StockHistoryProvider: DatabaseBase
    {
        public StockHistoryProvider(string connectionStrng) : base(connectionStrng)
        {

        }

        //====================================================================================================
        //Method used to add to stock
        //====================================================================================================
        public void AddToStockHistory(StockDTO stockPiece, StockOperationType operationType, string? salesUniqueIdForAutomaticUpdate = null)
        {
            //Local variable
            StockHistoryDTO stockHistory = new StockHistoryDTO();
            stockHistory.ProductId = stockPiece.ProductId;
            stockHistory.Quantity = stockPiece.ActualQuantity - stockPiece.LastQuantity;
            stockHistory.DateAndTime = DateTime.Now;
            stockHistory.OperationType = operationType.ToString();
            stockHistory.SalesIdForAutomaticUpdate = salesUniqueIdForAutomaticUpdate;

            using (ShopContext contextDB = new ShopContext(ConnectionString))
            {

                contextDB.StockHistory.Add(stockHistory);
                int retVal = contextDB.SaveChanges();
            }
        }
    }
}

