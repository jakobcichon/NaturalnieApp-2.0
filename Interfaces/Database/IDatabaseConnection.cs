using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces.Database
{
    internal interface IDatabaseConnection
    {
        string ConnectionString { get; set; }
    }
}
