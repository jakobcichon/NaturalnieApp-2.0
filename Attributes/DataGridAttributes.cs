using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Attributes
{
    public class DataGridAttributes
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class DisplayedPropertyName: Attribute
        {
            public string Name { get; set; }

            public DisplayedPropertyName(string name)
            {
                Name = name;
            }
        }
    }
}
