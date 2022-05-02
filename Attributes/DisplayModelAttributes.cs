using NaturalnieApp2.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
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
            private IHintListProvider? hintListProvider = null;
            private int? defaultIndex = null;
            private object? defaultElement = null;

            public VisualRepresenationType Type { get; }
            public VisualRepresenation(VisualRepresenationType type)
            {
                Type = type;

                if (type == VisualRepresenationType.List)
                {
                    throw new Exception("Selecting List type for visual presenter, IHintListPRovider must be assigned");
                }
            }

            public VisualRepresenation(VisualRepresenationType type, IHintListProvider listProvider, int defaultIndex = 0)
            {
                Type = type;
                hintListProvider = listProvider;
                this.defaultIndex = defaultIndex;
            }

            public VisualRepresenation(VisualRepresenationType type, IHintListProvider listProvider, object defaultSelection)
            {
                Type = type;
                hintListProvider = listProvider;
                this.defaultElement = defaultSelection;
            }

            public IHintListProvider? GetHintListProvider()
            {
                return hintListProvider;
            }

            public int? GetHintListDefaultIndex()
            {
                return defaultIndex;
            }

            public object? GetHintListDefaultElement()
            {
                return defaultElement;
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
        List,
        Field,
        LongField
    }
}
