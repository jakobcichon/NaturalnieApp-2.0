using System.Runtime.CompilerServices;

namespace NaturalnieApp2_Shared.Attributes
{

    public class DisplayModelAttributes
    {
        [AttributeUsage(AttributeTargets.Property)]
        public sealed class NameToBeDisplayed: Attribute
        {
            public string Name { get; }

            public NameToBeDisplayed(string name)
            {
                Name = name;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class VisibilityProperties : Attribute
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
        public sealed class ModificationProperties : Attribute
        {
            public bool CanBeModified { get; }
            public ModificationProperties(bool canBeModified=false)
            {
                CanBeModified = canBeModified;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        public sealed class VisualRepresenation : Attribute
        {
            public VisualRepresenationType Type { get; }
            public VisualRepresenation(VisualRepresenationType type, [CallerMemberName] string name = null)
            {
                Type = type;
            }
        }
    }

    public enum VisualRepresenationType
    {
        Default = -1,
        List,
        Field,
        LongField,
    }
}
