using NaturalnieApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database.Providers
{
    internal class SaleProvider: DatabaseBase<SaleProvider>
    {
        public SaleProvider(ShopContext shopContext) : base(shopContext)
        {

        }
    }
}
