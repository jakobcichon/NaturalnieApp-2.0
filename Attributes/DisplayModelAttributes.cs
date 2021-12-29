using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Attributes
{
    public class DisplayModelAttributes
    {
        [AttributeUsage(AttributeTargets.Property)]
        internal sealed class DisplayName: Attribute
        {
            private string _name;
            public string Name { get; }

            public DisplayName(string name)
            {
                Name = name;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        internal sealed class VisibilityProperties : Attribute
        {
            public bool Visible { get;}
            public bool HiddenByDefault { get;}
            public VisibilityProperties(bool visible=true, bool hiddenByDefault=false)
            {
                Visible = visible;
                HiddenByDefault = hiddenByDefault;
            }
        }


    }
}
