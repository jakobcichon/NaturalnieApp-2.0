using NaturalnieApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database
{
    internal class DatabaseCommon : DatabaseBase<DatabaseCommon>
    {
        public DatabaseCommon(ShopContext shopContext) : base(shopContext)
        {

        }

    }
}
