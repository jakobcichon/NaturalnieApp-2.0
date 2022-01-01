using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NaturalnieApp2.Attributes;
using NaturalnieApp2.Commands;
using NaturalnieApp2.Interfaces.Barcode;
using NaturalnieApp2.Interfaces.Database;
using NaturalnieApp2.Interfaces.DataGrid;
using NaturalnieApp2.Models;
using NaturalnieApp2.Models.MenuScreens.Inventory;
using NaturalnieApp2.Services.Attributes;
using NaturalnieApp2.Services.Database.Providers;
using NaturalnieApp2.Services.DataGrid;
using NaturalnieApp2.Services.DTOs;
using NaturalnieApp2.Services.DTOs.DataModelToUserControlModel;
using NaturalnieApp2.Views.Controls.Models;

namespace NaturalnieApp2.ViewModels.MenuScreens.Inventory
{
    internal class ExecuteInventoryViewModel: ViewModelBase, IBarcodeListner, IColumnEventHandler, IProductSelectorHandler
    {
        private ProductProvider _modelProvider;

        public ProductProvider ModelProvider
        {
            get { return _modelProvider; }
            set 
            { 
                _modelProvider = value;
                OnModelProviderChange();
            }
        }

        private ProductModel _actualSelectedProductModel;

        public ProductModel ActualSelectedProductModel
        {
            get { return _actualSelectedProductModel; }
            set { _actualSelectedProductModel = value; }
        }
        private List<ProductModel> _listOfAllProductModels;

        public List<ProductModel> ListOfAllProductModels
        {
            get { return _listOfAllProductModels; }
            set { _listOfAllProductModels = value; }
        }

        public ShopProductSelectorDataModel ProductSelectorDataModel { get; set; }

        private ObservableCollection<InventoryModel> _actualState;

        public ObservableCollection<InventoryModel> ActualState
        {
            get { return _actualState; }
            set { _actualState = value; }
        }

        private ObservableCollection<InventoryModel> _toDateState;

        public ObservableCollection<InventoryModel> ToDateState
        {
            get { return _toDateState; }
            set { _toDateState = value; }
        }

        public ExecuteInventoryViewModel()
        {
            //Instance of the ProductSelectorDataModel
            ProductSelectorDataModel = new ShopProductSelectorDataModel();
            ActualSelectedProductModel = new ProductModel();
            ListOfAllProductModels = new List<ProductModel>();

            ActualState = new ObservableCollection<InventoryModel>() { new InventoryModel() { ProductName = "test1" } };
            ToDateState = new ObservableCollection<InventoryModel>() { new InventoryModel() { ProductName = "FromDB" } };
        }

        public void OnBarcodeValidAction(string barcode)
        {
            throw new NotImplementedException();
        }

        public void OnAutomaticColumnGenerating(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            AttributeCollection attributes = (e.PropertyDescriptor as PropertyDescriptor).Attributes;
            foreach (Attribute attribute in attributes)
            {
                DataGridModelDisplayServices.ApllyColumnProperties(attribute, e.Column);
            }
        }

        public void OnModelProviderChange()
        {
            //Get all products
            ListOfAllProductModels = ModelProvider.GetAllProductEntities();

            ProductSelectorDataModel = DataModelToShopSelectorModel.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);

            foreach(ShopProductSelectorDataSingleElement displayElement in ProductSelectorDataModel.Elements)
            {

                List<object> value = new List<object>();

                foreach (ProductModel element in ListOfAllProductModels)
                {
                    object? _value = DisplayModelAttributesServices.GetPropertyValueByDisplayName(displayElement.Name, element);

                    if (_value == null) continue;
                    if (value.Exists(e => e.ToString() == _value.ToString())) continue;

                    value.Add(_value);
                }

                displayElement.Value = value;
            }
        }

        public void OnFilterRequest()
        {
            throw new NotImplementedException();
        }

        public void OnElementSelected()
        {
            throw new NotImplementedException();
        }

        public void OnClearFilterRequest()
        {
            throw new NotImplementedException();
        }
    }
}
