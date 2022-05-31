using NaturalnieApp2.Attributes;
using NaturalnieApp2.Services.Attributes;
using NaturalnieApp2.Services.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces.Database
{
    public interface IModelProvider
    {
        /// <summary>
        /// Model type for given provider
        /// </summary>
        public Type ModelType { get; init; }

        /// <summary>
        /// Method used to get properties names for given model
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <returns></returns>
        public virtual List<string?> GetModelElementsNames<T>() where T: class
        {
            List<PropertyDescriptor> propertiesList = DisplayModelAttributesServices.GetPropertiesToBeDisplayed(typeof(T));
            List<string?> returnList = propertiesList.Select(p => DisplayModelAttributesServices.GetPropertyDisplayName(p)).ToList();

            return returnList;
        }

        public TmodelDTO GetModelFromModelDTO<TmodelDTO, Tmodel>()
            where TmodelDTO : class
            where Tmodel : class;

    }
}
