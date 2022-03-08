using NaturalnieApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database
{
    internal class DatabaseBase
    {
        private ShopContext shopContext;

        public ShopContext ShopContext
        {
            get { return shopContext; }
            set { shopContext = value; }
        }

        public DatabaseBase(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public enum HistoryOperationType
        {
            AddedNew,
            Deleted,
            Modified
        }
    }
}
