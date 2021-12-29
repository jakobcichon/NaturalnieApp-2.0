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
        public static List<PropertyDescriptor> GetPropertiesOfClass(Type examinedObjectType, Type attributeClassType)
        {
            List<PropertyDescriptor> returnList = new List<PropertyDescriptor>();
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(examinedObjectType);

            foreach(PropertyDescriptor property in properties)
            {
                if(property.PropertyType == attributeClassType)
                {
                    returnList.Add(property);
                }
            }

            return returnList;
        }
    }
}
