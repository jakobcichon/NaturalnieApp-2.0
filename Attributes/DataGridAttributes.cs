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
        internal class DisplayName: Attribute
        {
            private string _name;
            public string Name { get; }

            public DisplayName(string name)
            {
                Name = name;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        internal class ColumnProperties : Attribute
        {
            public bool Visible { get;}
            public bool HiddenByDefault { get;}
            public ColumnProperties(bool visible=true, bool hiddenByDefault=false)
            {
                Visible = visible;
                HiddenByDefault = hiddenByDefault;
            }
        }


    }
}
