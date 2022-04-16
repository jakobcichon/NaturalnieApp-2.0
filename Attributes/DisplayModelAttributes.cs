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

        [AttributeUsage(AttributeTargets.Property)]
        internal sealed class ModificationProperties : Attribute
        {
            public bool CanBeModified { get; }
            public ModificationProperties(bool canBeModified=false)
            {
                CanBeModified = canBeModified;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        internal sealed class VisualRepresenation : Attribute
        {
            public VisualRepresenationType Type { get; }
            public VisualRepresenation(VisualRepresenationType type)
            {
                Type = type;
            }
        }
    }

    public enum VisualRepresenationType
    {
        List,
        Field,
        LongField
    }
}
