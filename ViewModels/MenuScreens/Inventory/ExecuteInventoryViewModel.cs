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
using NaturalnieApp2.Services.DataModel;
using NaturalnieApp2.Services.DTOs;
using NaturalnieApp2.Services.DTOs.DataModelToUserControlModel;
using NaturalnieApp2.Views.Controls.Models;

namespace NaturalnieApp2.ViewModels.MenuScreens.Inventory
{
    internal class ExecuteInventoryViewModel: ViewModelBase, IBarcodeListner, IColumnEventHandler, IProductSelectorHandler
    {
        private ProductProvider _modelProvider;

        public Action<bool> OnDataFiltered { get; set; }

        private StockProvider _stockProvider;

        public StockProvider StockProvider
        {
            get { return _stockProvider; }
            set { _stockProvider = value; }
        }


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

        private ShopProductSelectorDataModel AllValuesProductSelectorDataModel { get; set; }
        private ShopProductSelectorDataModel FilteredProductSelectorDataModel { get; set; }

        public ShopProductSelectorDataModel ProductSelectorDataModel { get; set; }

        private ObservableCollection<InventoryModel> _actualState;

        public ObservableCollection<InventoryModel> ActualState
        {
            get { return _actualState; }
            set { _actualState = value; }
        }

        private ObservableCollection<InventoryModelDataFromDB> _toDateState;

        public ObservableCollection<InventoryModelDataFromDB> ToDateState
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

            ActualState = new ObservableCollection<InventoryModel>();
            ToDateState = new ObservableCollection<InventoryModelDataFromDB>();

            ActualState.CollectionChanged += ActualState_CollectionChanged;
        }

        private void ActualState_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IEnumerable<InventoryModel> localSender = sender as IEnumerable<InventoryModel>;
            if (localSender == null) return;


            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                for (int i = e.NewStartingIndex; i < e.NewItems.Count + e.NewStartingIndex; i++)
                {
                    InventoryModel inventoryModel = localSender.ToList<InventoryModel>()[i];
                    if (localSender.ToList()[i] == null) return;

                    int quantity = StockProvider.GetProductQuantityInStock(inventoryModel.ProductName);

                    InventoryModelDataFromDB inventoryModelWithStock = ModelConvertions<InventoryModel, InventoryModelDataFromDB>.ConvertModels(inventoryModel);
                    inventoryModelWithStock.ProductQuantity = quantity;

                    ToDateState.Add(inventoryModelWithStock);
                }
            }

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                for (int i = e.OldStartingIndex; i < e.OldItems.Count + e.OldStartingIndex; i++)
                {
                    ToDateState.RemoveAt(i);
                }
            }
        }

        public void OnBarcodeValidAction(string barcode)
        {
            ProductModel? product = ListOfAllProductModels?.FirstOrDefault(e => e.BarCode == barcode || e.BarCodeShort == barcode, null);
            
            if (product != null) IncrementQuantityIfExistInCollection(ModelConvertions<ProductModel, InventoryModel>.ConvertModels(product));
        }

        public void OnAutomaticColumnGenerating(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            AttributeCollection attributes = (e.PropertyDescriptor as PropertyDescriptor).Attributes;
            foreach (Attribute attribute in attributes)
            {
                DataGridModelDisplayServices.ApllyColumnProperties(attribute, e.Column);
                DataGridModelDisplayServices.ColumnModificationPropertiesFromAttribute(attribute, e.Column);
            }
        }

        public void OnModelProviderChange()
        {
            //Get all products
            ListOfAllProductModels = ModelProvider.GetAllProductEntities();

            AllValuesProductSelectorDataModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);
            FilteredProductSelectorDataModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);
            ProductSelectorDataModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);

            DataModelToShopSelectorModel<ProductModel>.GetAllValuesOfModelToSelectorDataModel(AllValuesProductSelectorDataModel, ListOfAllProductModels);

            ProductSelectorDataModel.Elements = AllValuesProductSelectorDataModel.Elements;

            ActualSelectedProductModel = ListOfAllProductModels.First();

        }

        public void OnFilterRequest(string elementName, object elementValue)
        {
            string? propertyName = DisplayModelAttributesServices.GetPropertyNameByDisplayName(elementName, typeof(ProductModel));
            if (propertyName == null) return;

            List<ProductModel> filteredModel = DataModelServices<ProductModel>.FitlerModelByPropertyName(propertyName, elementValue, ListOfAllProductModels);
            FilteredProductSelectorDataModel.ClearAllValues();
            DataModelToShopSelectorModel<ProductModel>.GetAllValuesOfModelToSelectorDataModel(FilteredProductSelectorDataModel, filteredModel);
            
            ProductSelectorDataModel.Elements = FilteredProductSelectorDataModel.Elements;

            ActualSelectedProductModel = filteredModel.First();

            DataFiltered();
        }

        public void IncrementQuantityIfExistInCollection(InventoryModel inventoryModel)
        {
            InventoryModel? existingModel = ActualState.FirstOrDefault(e => e.ProductName == inventoryModel.ProductName, null);

            if (existingModel != null)
            {
                existingModel.ProductQuantity += 1;
                return;
            }

            inventoryModel.ProductQuantity += 1;
            ActualState.Add(inventoryModel);

        }

        public void OnElementSelected()
        {
            IncrementQuantityIfExistInCollection(ModelConvertions<ProductModel, InventoryModel>.
                ConvertModels(ActualSelectedProductModel));
        }

        public void OnClearFilterRequest()
        {
            ProductSelectorDataModel.Elements = AllValuesProductSelectorDataModel.Elements;
            
            ClearDataFilter();
        }

        public void DataFiltered()
        {
            OnDataFiltered?.Invoke(true);
        }

        public void ClearDataFilter()
        {
            OnDataFiltered?.Invoke(false);
        }
    }
}
