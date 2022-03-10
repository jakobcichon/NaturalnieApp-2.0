using NaturalnieApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database
{
    internal class DatabaseCommon : DatabaseBase
    {
        public DatabaseCommon(ShopContext shopContext) : base(shopContext)
        {

        }

        /// <summary>
        /// Method used to check if conenction to database exist
        /// </summary>
        /// <returns>True if connection exist, otherwise False.</returns>
        public bool CheckDatabaseConnection()
        {
            return ShopContext.Database.Exists();
        }
    }
}
