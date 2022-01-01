using NaturalnieApp2.Attributes;
using NaturalnieApp2.Services.Attributes;
using NaturalnieApp2.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs.DataModelToUserControlModel
{
    internal static class DataModelToShopSelectorModel
    {
        public static ShopProductSelectorDataModel FromDataModelToShopProductSelectorModel(DisplayModelAttributes dataModel)
        {
            ShopProductSelectorDataModel shopProductSelectorDataModel = new ShopProductSelectorDataModel();

            //Get properties to be displayed
            List<PropertyDescriptor> properties = DisplayModelAttributesServices.GetPropertiesToBeDisplayed(dataModel?.GetType());

            foreach (PropertyDescriptor property in properties)
            {
                string? _displayName = DisplayModelAttributesServices.GetPropertyDisplayName(property);

                if (_displayName != null) shopProductSelectorDataModel.Elements.Add(new ShopProductSelectorDataSingleElement() { Name=_displayName});
            }

            return shopProductSelectorDataModel;
        }

    }
}
