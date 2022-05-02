using NaturalnieApp2.Attributes;
using NaturalnieApp2.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.Services.Attributes
{
    public static class DisplayModelAttributesServices
    {

        public static string? GetPropertyNameByDisplayName(string propertyName, Type examinedObjectType)
        {
            PropertyDescriptor? property = GetPropertyToBeDisplayed(examinedObjectType, propertyName);
            return property?.Name;
        }

        public static object? GetPropertyValueByDisplayName(string propertyName, object instance)
        {
            PropertyDescriptor? property = GetPropertyToBeDisplayed(instance.GetType(), propertyName);

            if (property == null) return null;

            return property.GetValue(instance);
        }

        public static List<string> GetPropertiesNamesToBeDisplayed(Type examinedObjectType)
        {
            List<string> returnList = new List<string>();

            List<PropertyDescriptor> propertiesWithVisibilityAttributes =
                GetPropertiesOfClass(examinedObjectType, typeof(DisplayModelAttributes.VisibilityProperties));

            foreach (PropertyDescriptor property in propertiesWithVisibilityAttributes)
            {
                if (property.Attributes.OfType<DisplayModelAttributes.VisibilityProperties>()?.FirstOrDefault()?.Visible == true)
                {
                    returnList.Add(GetPropertyDisplayName(property));
                }
            }

            return returnList;
        }

        public static PropertyDescriptor? GetPropertyToBeDisplayed(Type examinedObjectType, string propertyDisplayName)
        {
            List<PropertyDescriptor> returnList = new List<PropertyDescriptor>();

            List<PropertyDescriptor> propertiesWithVisibilityAttributes =
                GetPropertiesOfClass(examinedObjectType, typeof(DisplayModelAttributes.VisibilityProperties));

            foreach (PropertyDescriptor property in propertiesWithVisibilityAttributes)
            {
                if (property.Attributes.OfType<DisplayModelAttributes.VisibilityProperties>()?.FirstOrDefault()?.Visible == true)
                {
                    if (property.Attributes.OfType<DisplayModelAttributes.NameToBeDisplayed>()?.FirstOrDefault()?.Name == propertyDisplayName)
                    {
                        return property;
                    }    
                }
            }

            return null;
        }

        public static PropertyDescriptor? GetPropertyByName(Type examinedObjectType, string propertyName)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(examinedObjectType);

            foreach (PropertyDescriptor property in properties)
            {
                if (property.Name == propertyName) return property;
            }

            return null;
        }

        public static List<PropertyDescriptor> GetPropertiesToBeDisplayed(Type examinedObjectType)
        {
            List<PropertyDescriptor> returnList = new List<PropertyDescriptor>();

            List<PropertyDescriptor> propertiesWithVisibilityAttributes = 
                GetPropertiesOfClass(examinedObjectType, typeof(DisplayModelAttributes.VisibilityProperties));

            foreach (PropertyDescriptor property in propertiesWithVisibilityAttributes)
            {
                if (property.Attributes.OfType<DisplayModelAttributes.VisibilityProperties>()?.FirstOrDefault()?.Visible == true)
                {
                    returnList.Add(property);
                }
            }

            return returnList;
        }

        public static bool CheckIfPropertyVisiable(string propertyName, Type examinedObjectType)
        {
            List<PropertyDescriptor> propertiesWithVisibilityAttributes =
                GetPropertiesOfClass(examinedObjectType, typeof(DisplayModelAttributes.VisibilityProperties));

            foreach (PropertyDescriptor property in propertiesWithVisibilityAttributes)
            {
                if (property.Name == propertyName)
                {
                    if (property.Attributes.OfType<DisplayModelAttributes.VisibilityProperties>()?.FirstOrDefault()?.Visible == true)
                    {
                        return true;
                    }
                    return false;
                }

            }

            return false;
        }

        public static bool CheckIfPropertyVisiable(PropertyDescriptor property)
        {

            if (property.Attributes.OfType<DisplayModelAttributes.VisibilityProperties>()?.FirstOrDefault()?.Visible == true)
            {
                return true;
            }
            return false;
        }

        public static bool CheckIfPropertyVisiableByDefault(string propertyName, Type examinedObjectType)
        {
            List<PropertyDescriptor> propertiesWithVisibilityAttributes =
                GetPropertiesOfClass(examinedObjectType, typeof(DisplayModelAttributes.VisibilityProperties));

            foreach (PropertyDescriptor property in propertiesWithVisibilityAttributes)
            {
                if (property.Name == propertyName)
                {
                    if (property.Attributes.OfType<DisplayModelAttributes.VisibilityProperties>()?.FirstOrDefault()?.Visible == true &&
                        property.Attributes.OfType<DisplayModelAttributes.VisibilityProperties>()?.FirstOrDefault()?.HiddenByDefault == false)
                    {
                        return true;
                    }
                    return false;
                }
            }

            return false;
        }

        public static string? GetPropertyDisplayName(PropertyDescriptor property)
        {
            return property.Attributes.OfType<DisplayModelAttributes.NameToBeDisplayed>()?.FirstOrDefault()?.Name;
        }


        public static List<PropertyDescriptor> GetPropertiesOfClass(Type examinedObjectType, Type attributeClassType)
        {
            List<PropertyDescriptor> returnList = new List<PropertyDescriptor>();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(examinedObjectType);

            foreach(PropertyDescriptor property in properties)
            {

                foreach (Attribute attribute in property.Attributes)
                {
                    if (attribute.GetType() == attributeClassType)
                    {
                        returnList.Add(property);
                    }
                }
            }
            return returnList;
        }

        public static VisualRepresenationType? GetPropertyVisualRepresentationType(PropertyDescriptor property)
        {
            return property.Attributes.OfType<DisplayModelAttributes.VisualRepresenation>()?.FirstOrDefault()?.Type;
        }

        public static ValidationRule? GetValidationClass(PropertyDescriptor property)
        {
            return property.Attributes.OfType<DisplayModelAttributes.PropertyValidationRule>()?.FirstOrDefault()?.GetValidationClass();
        }

        public static IHintListProvider? GetHintListProvider(PropertyDescriptor property)
        {
            return property.Attributes.OfType<DisplayModelAttributes.VisualRepresenation>()?.FirstOrDefault()?.GetHintListProvider();
        }

        public static int? GetHintListDefaultIndex(PropertyDescriptor property)
        {
            return property.Attributes.OfType<DisplayModelAttributes.VisualRepresenation>()?.FirstOrDefault()?.GetHintListDefaultIndex();
        }

        public static object? GetHintListDefaultElement(PropertyDescriptor property)
        {
            return property.Attributes.OfType<DisplayModelAttributes.VisualRepresenation>()?.FirstOrDefault()?.GetHintListDefaultElement();
        }
    }
}
