using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Attributes
{
    internal class DataGridAttributes
    {
        [AttributeUsage(AttributeTargets.Property)]
        internal class ColumnName: Attribute
        {
            private string _name;
            public string Name { get; }

            public ColumnName(string name)
            {
                Name = name;
            }
        }
    }
}
