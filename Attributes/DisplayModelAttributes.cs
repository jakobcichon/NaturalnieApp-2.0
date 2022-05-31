using NaturalnieApp2.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace NaturalnieApp2.Attributes
{

    public class DisplayModelAttributes
    {
        [AttributeUsage(AttributeTargets.Property)]
        internal sealed class NameToBeDisplayed: Attribute
        {
            public string Name { get; }

            public NameToBeDisplayed(string name)
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
            public VisualRepresenation(VisualRepresenationType type, [CallerMemberName] string name = null)
            {
                Type = type;
            }
        }

        [AttributeUsage(AttributeTargets.Property)]
        internal sealed class PropertyValidationRule : Attribute
        {

            private readonly Type validationRule;

            public PropertyValidationRule(Type validationClassType)
            {
                if (!validationClassType.IsSubclassOf(typeof(ValidationRule))) throw new ArgumentException($"Wrong attribute type! Expected {typeof(ValidationRule)}");
                validationRule = validationClassType;
            }

            public ValidationRule GetValidationClass()
            {
                return Activator.CreateInstance(validationRule) as ValidationRule;
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
