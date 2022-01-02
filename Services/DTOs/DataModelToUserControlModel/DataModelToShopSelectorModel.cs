using NaturalnieApp2.Attributes;
using NaturalnieApp2.Services.Attributes;
using NaturalnieApp2.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NaturalnieApp2.Services.DTOs.DataModelToUserControlModel
{
    internal static class DataModelToShopSelectorModel<T> where T : class
    {
        public static ShopProductSelectorDataModel FromDataModelToShopProductSelectorModel(DisplayModelAttributes dataModel)
        {
            ShopProductSelectorDataModel shopProductSelectorDataModel = new ShopProductSelectorDataModel();

            //Get properties to be displayed
            List<PropertyDescriptor> properties = DisplayModelAttributesServices.GetPropertiesToBeDisplayed(dataModel?.GetType());

            foreach (PropertyDescriptor property in properties)
            {
                string? _displayName = DisplayModelAttributesServices.GetPropertyDisplayName(property);

                if (_displayName != null)
                {
                    shopProductSelectorDataModel.Elements.Add(new ShopProductSelectorDataSingleElement() { Name = _displayName });

                    if (DisplayModelAttributesServices.CheckIfPropertyVisiableByDefault(property.Name, dataModel?.GetType()))
                    {
                        shopProductSelectorDataModel.Elements.Last().MakeElementVisiable();
                    }

                }
            }

            return shopProductSelectorDataModel;
        }

        public static void GetAllValuesOfModelToSelectorDataModel(ShopProductSelectorDataModel selectorDataModel, 
            List<T> dataModels)
        {
            foreach (ShopProductSelectorDataSingleElement displayElement in selectorDataModel.Elements)
            {

                List<object> value = new List<object>();

                foreach (T element in dataModels)
                {
                    object? _value = DisplayModelAttributesServices.GetPropertyValueByDisplayName(displayElement.Name, element);

                    if (_value == null) continue;
                    if (value.Exists(e => e.ToString() == _value.ToString())) continue;

                    value.Add(_value);
                }

                displayElement.Value = value;
            }
        }

    }
}
