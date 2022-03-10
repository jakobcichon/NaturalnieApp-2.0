using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NaturalnieApp2.Attributes;
using NaturalnieApp2.Commands;
using NaturalnieApp2.Commands.MenuScreens.Inventory;
using NaturalnieApp2.Interfaces;
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
    internal class ExecuteInventoryViewModel: ViewModelBase, IBarcodeListner, IProductSelectorHandler, IDataGridAdditionalActionsEventHandler
    {
        public ExecuteInventoryViewModel()
        {
            //Instance of the ProductSelectorDataModel
            ActualSelectedProductModel = new ProductModel();
            ListOfAllProductModels = new List<ProductModel>();

            ActualState = new ObservableCollection<InventoryModel>();
            ToDateState = new ObservableCollection<InventoryModelDataFromDB>();

            ActualState.CollectionChanged += ActualState_CollectionChanged;

            BottomButtonPanel = new BottomButtonBarModel();
            BottomButtonPanel.LeftButtons.Add(new SignleButtonModel("Zamknij", 
                new BottomButtonPanelCommands(), OnCloseViewAction));

            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Pobierz inwentaryzację z bazy danych",
                new BottomButtonPanelCommands(), OnGetInventoryFromDB));
            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Wyczyść listę produktów",
                new BottomButtonPanelCommands(), OnClearList));
            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Zapisz do bazy danych",
                new BottomButtonPanelCommands(), OnSaveToDatabase));
            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Odśwież dane produktu",
                new BottomButtonPanelCommands(), OnRefreshProductData));
        }

        private bool modelHasBeenCreated = false;

        private ICommand screenUpdates;

        public ICommand ScreenUpdates
        {
            get 
            {
                if (screenUpdates == null) return new ScreenCommands(CreateModelProvider);
                return screenUpdates; 
            }
            set { screenUpdates = value; }
        }


        public BottomButtonBarModel BottomButtonPanel { get;}

        public IMessageServer MessageServer { get; }

        public Action<bool> OnDataFiltered { get; set; }

        public string? ActualInventoryName { get => "Inwentaryzacja 2021"; }

        private StockProvider _stockProvider;
        public StockProvider StockProvider
        {
            get { return _stockProvider; }
            set { _stockProvider = value; }
        }

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

        private InventoryProvider _inventoryProvider;
        public InventoryProvider InventoryProvider
        {
            get { return _inventoryProvider; }
            set { _inventoryProvider = value; }
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

        private ShopProductSelectorDataModel EmptyModel { get; set; }
        private ShopProductSelectorDataModel AllValuesProductSelectorDataModel { get; set; }
        private ShopProductSelectorDataModel FilteredProductSelectorDataModel { get; set; }

        private ShopProductSelectorDataModel productSelectorDataModel;

        public ShopProductSelectorDataModel ProductSelectorDataModel
        {
            get { return productSelectorDataModel; }
            set 
            { 
                productSelectorDataModel = value; 
                OnPropertyChanged(nameof(ProductSelectorDataModel));
            }
        }


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

        public Action<int> OnCollectionElementChange { get; set; }

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

            if (product == null)
            {
                SystemSounds.Hand.Play();
                return;
            }
            AddModelToList(product);
        }

        public void OnModelProviderChange()
        {
        }

        public InventoryModel? GetInventoryModelFromList(IEnumerable<InventoryModel> inventoryList, InventoryModel inventoryModelToSearch)
        {
            InventoryModel? existingModel = inventoryList.FirstOrDefault(e => e.ProductName == inventoryModelToSearch.ProductName, null);

            return existingModel;
        }

        public void IncrementQuantityIfExistInCollection(InventoryModel inventoryModel)
        {
            InventoryModel? existingModel = GetInventoryModelFromList(ActualState, inventoryModel);
            int modifiedIndex = -1;

            if (existingModel != null)
            {
                existingModel.ProductQuantity += 1;
                modifiedIndex = ActualState.IndexOf(existingModel);
                OnCollectionElementChange?.Invoke(modifiedIndex);
                return;
            }

            inventoryModel.ProductQuantity += 1;
            ActualState.Add(inventoryModel);

            modifiedIndex = ActualState.IndexOf(inventoryModel);
            OnCollectionElementChange?.Invoke(modifiedIndex);

        }

        public void AddModelToList(ProductModel productModel)
        {
            InventoryModel inventoryModel = ModelConvertions<ProductModel, InventoryModel>.ConvertModels(productModel);

            //Check if inventory model already exist in the list. if yes, do not check state from DB
            InventoryModel? existingModel = GetInventoryModelFromList(ActualState, inventoryModel);

            if(existingModel != null)
            {
                IncrementQuantityIfExistInCollection(existingModel);
                return;
            }

            inventoryModel.InventoryName = ActualInventoryName;
            //Check if product exist in Inventory database
            InventoryModel? inventoryFromDB = InventoryProvider.CheckIfInventoryModelExist(inventoryModel);

            if (inventoryFromDB != null)
            {
                IncrementQuantityIfExistInCollection(inventoryFromDB);
                return;
            }
            IncrementQuantityIfExistInCollection(inventoryModel);
        }

        public void OnElementSelected()
        {
            AddModelToList(ActualSelectedProductModel);

        }

        public void OnClearFilterRequest()
        {
            //ProductSelectorDataModel.Elements = EmptyModel.Elements;
            //ProductSelectorDataModel.Elements = AllValuesProductSelectorDataModel.Elements;
            ProductSelectorDataModel.AddElementsValues(AllValuesProductSelectorDataModel.Elements);

            ClearDataFilter();
        }

        public void OnFilterRequest(string elementName, object elementValue)
        {
            string? propertyName = DisplayModelAttributesServices.GetPropertyNameByDisplayName(elementName, typeof(ProductModel));
            if (propertyName == null) return;

            List<ProductModel> filteredModel = DataModelServices<ProductModel>.FitlerModelByPropertyName(propertyName, elementValue, ListOfAllProductModels);
            FilteredProductSelectorDataModel.ClearAllValues();
            DataModelToShopSelectorModel<ProductModel>.GetAllValuesOfModelToSelectorDataModel(FilteredProductSelectorDataModel, filteredModel);

            //ProductSelectorDataModel.Elements = FilteredProductSelectorDataModel.Elements;
            ProductSelectorDataModel.ClearAllValues();
            ProductSelectorDataModel.AddElementsValues(FilteredProductSelectorDataModel.Elements);

            ActualSelectedProductModel = filteredModel.First();

            DataFiltered();
        }

        public void OnProductSelectorLoaded()
        {
            if(ProductSelectorDataModel?.Elements == FilteredProductSelectorDataModel?.Elements)
            {
                DataFiltered();
                return;
            }

            ClearDataFilter();
        }

        public void CreateModelProvider()
        {
            if (!modelHasBeenCreated)
            {
                try
                {
                    //Get all products
                    ListOfAllProductModels = ModelProvider.GetAllProductEntities();

                    EmptyModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);
                    AllValuesProductSelectorDataModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);
                    FilteredProductSelectorDataModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);
                    ProductSelectorDataModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);

                    DataModelToShopSelectorModel<ProductModel>.GetAllValuesOfModelToSelectorDataModel(AllValuesProductSelectorDataModel, ListOfAllProductModels);
                    List<ProductModel> firstElement = new List<ProductModel>();
                    firstElement.Add(ListOfAllProductModels.First());
                    DataModelToShopSelectorModel<ProductModel>.GetAllValuesOfModelToSelectorDataModel(EmptyModel, firstElement);

                    //ProductSelectorDataModel.Elements = AllValuesProductSelectorDataModel.Elements;
                    ProductSelectorDataModel.ClearAllValues();
                    ProductSelectorDataModel.AddElementsValues(AllValuesProductSelectorDataModel.Elements);

                    ActualSelectedProductModel = ListOfAllProductModels.First();

                    //Set auxiliary field, to prevent reinitialization
                    modelHasBeenCreated = true;
                }
                catch (System.Data.Entity.Core.ProviderIncompatibleException ex)
                {
                    MessageBox.Show("Nie można połączyć się z bazą danych");
                    Debug.WriteLine(ex.Message);
                }
            }

        }
        public void UpdateModelProvider()
        {
            //Get all products
            ListOfAllProductModels = ModelProvider.GetAllProductEntities();

            AllValuesProductSelectorDataModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);
            FilteredProductSelectorDataModel = DataModelToShopSelectorModel<ProductModel>.FromDataModelToShopProductSelectorModel(ActualSelectedProductModel);

            DataModelToShopSelectorModel<ProductModel>.GetAllValuesOfModelToSelectorDataModel(AllValuesProductSelectorDataModel, ListOfAllProductModels);
            List<ProductModel> firstElement = new List<ProductModel>();
            firstElement.Add(ListOfAllProductModels.First());
            DataModelToShopSelectorModel<ProductModel>.GetAllValuesOfModelToSelectorDataModel(EmptyModel, firstElement);

            //ProductSelectorDataModel.Elements = AllValuesProductSelectorDataModel.Elements;
            ProductSelectorDataModel.ClearAllValues();
            ProductSelectorDataModel.AddElementsValues(AllValuesProductSelectorDataModel.Elements);

            ActualSelectedProductModel = ListOfAllProductModels.First();
        }

        public void DataFiltered()
        {
            OnDataFiltered?.Invoke(true);
        }

        public void ClearDataFilter()
        {
            OnDataFiltered?.Invoke(false);
        }

        public void OnCloseViewAction()
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zamknąc okno?", "Zamknięcie okna", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes) CloseScreen(this);
        }

        public void OnRefreshProductData()
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz odświeżyć dane?", "Odświeżenie danych", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ClearDataFilter();
                UpdateModelProvider();
            }

        }

        public void OnClearList()
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz wyczyścić listę produktów?", "Czyszczenie listy produktów", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ActualState.Clear();
                ToDateState.Clear();
            }
        }

        public void OnGetInventoryFromDB()
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz pobrać pełną listę inwentaryzacji z bazy danych?\nObecna lista zostanie utracona...", "Czyszczenie listy produktów", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                ActualState.Clear();
                ToDateState.Clear();

                List<InventoryModel> inventoryModelsFromDb = InventoryProvider.GetAllInventoryEntitiesByInventoryName(ActualInventoryName);

                inventoryModelsFromDb.ForEach(x => ActualState.Add(x));
            }
        }

        public void OnSaveToDatabase()
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zapisać produkty do bazy danych?", "Zapis do bazy danych", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                int errors = 0;
                int success = 0;


                foreach (InventoryModel inventoryModel in ActualState)
                {
                    //Check if exist
                    InventoryModel? inventoryFromDB = InventoryProvider.CheckIfInventoryModelExist(inventoryModel);
                    if (inventoryFromDB != null)
                    {
                        try
                        {
                            InventoryProvider.EditInInventory(inventoryModel);
                            success++;
                            continue;
                        }
                        catch (Exception ex)
                        {
                            errors++;
                            MessageServer?.AddMessage(this, ex.Message);
                            continue;
                        }
                    }

                    try
                    {
                        InventoryProvider.AddToInventory(inventoryModel);
                        success++;
                        continue;
                    }
                    catch (Exception ex)
                    {
                        errors++;
                        MessageServer?.AddMessage(this, ex.Message);
                        continue;
                    }
                }

                if (errors > 0)
                {
                    MessageBox.Show($"Uwaga! Nie wszystkie elementy zostały pomyślnie zapisane do bazy danych!" +
                        $" \nPomyślnie zapisane elementy: {success}\nBłędnie zapisane elementy: {errors}." +
                        $"\nWięcej informacji w dzienniku zdarzeń.");
                }

                MessageBox.Show($"Wszystkie elementy zostały pomyślnie zapisane do bazy danych.\nLiczba zapisanych elementów: {success}");


            }
        }

        private class ScreenCommands : ICommand
        {
            public event EventHandler? CanExecuteChanged;

            private readonly Action loadedAction;

            public ScreenCommands(Action loadedAction)
            {
                this.loadedAction = loadedAction;
            }

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                if ("Loaded" == parameter?.ToString())
                {
                    loadedAction();
                }
            }
        }
    }


}
