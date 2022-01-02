using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DataModel
{
    public static class ModelConvertions<TInputModel, TOutputModel> 
        where TInputModel: class
        where TOutputModel: class
        
    {
        public static TOutputModel ConvertModels(TInputModel inputModel)
        {
            Type inputModelType = typeof(TInputModel);
            Type outputModelType = typeof(TOutputModel);

            TOutputModel outputModel = (TOutputModel) Activator.CreateInstance(outputModelType);

            foreach (PropertyInfo property in inputModelType.GetProperties().ToList())
            {
                if(outputModelType.GetProperty(property.Name) != null)
                {
                    outputModelType.GetProperty(property.Name).SetValue(outputModel, property.GetValue(inputModel));
                }
            }

            return outputModel;
        }
    }
}
