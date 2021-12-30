using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Attributes
{
    public static class DisplayModelAttributesServices
    {

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

        public static string? GetPropertyDisplayName(PropertyDescriptor property)
        {
            return property.Attributes.OfType<DisplayModelAttributes.DisplayName>()?.FirstOrDefault()?.Name;
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
    }
}
