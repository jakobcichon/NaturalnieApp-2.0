using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DataModel
{
    public static class HashCode
    {
        public static string GetHashCodeFromAllClassProperties(object objectInstance)
        {
            string retHashString = "";

            Type objectType = objectInstance.GetType();
            List<PropertyInfo> properties = objectType.GetProperties().ToList();

            foreach(PropertyInfo property in properties)
            {
                if (property.Name == "HashFromModel") continue;
                object? propValue = property.GetValue(objectInstance, null);
                if (propValue == null) continue;

                retHashString += propValue?.ToString()?.GetHashCode();
            }

            return retHashString;
        }
    }
}
