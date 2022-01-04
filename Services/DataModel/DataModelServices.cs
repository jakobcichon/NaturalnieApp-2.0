using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DataModel
{
    public static class DataModelServices<T> where T : class
    {
        public static List<T> FitlerModelByPropertyName(string propertyName, object propertyValue, List<T> listToFilter)
        {
            List<T> resultList = new List<T>();

            foreach (T model in listToFilter)
            {
                PropertyInfo? property = model.GetType().GetProperty(propertyName);
                if (property != null && property.GetValue(model)?.ToString() == propertyValue?.ToString())
                {
                    resultList.Add(model);
                }
            }

            return resultList;
        }
    }
}
