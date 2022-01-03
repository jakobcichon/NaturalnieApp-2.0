using NaturalnieApp2.Interfaces.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Database
{
    internal class DatabaseBase : IDatabaseConnection
    {
        public string ConnectionString { get; set; }

        public DatabaseBase(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public enum HistoryOperationType
        {
            AddedNew,
            Deleted,
            Modified
        }
    }
}
