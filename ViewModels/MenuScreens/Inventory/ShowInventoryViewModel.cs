using Microsoft.Win32;
using NaturalnieApp2.Commands.MenuScreens.Inventory;
using NaturalnieApp2.Models.MenuScreens.Inventory;
using NaturalnieApp2.Services.Database.Providers;
using NaturalnieApp2.Services.ExcelServices;
using NaturalnieApp2.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NaturalnieApp2.ViewModels.MenuScreens.Inventory
{
    internal class ShowInventoryViewModel:ViewModelBase
    {
        public ShowInventoryViewModel()
        {

            BottomButtonPanel = new BottomButtonBarModel();
            BottomButtonPanel.LeftButtons.Add(new SignleButtonModel("Zamknij",
                new BottomButtonPanelCommands(), OnCloseViewAction));

            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Pobierz inwentaryzację z bazy danych",
                new BottomButtonPanelCommands(), OnGetInventoryFromDB));
            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Wyczyść listę produktów",
                new BottomButtonPanelCommands(), OnClearList));
            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Zapisz do excel",
                new BottomButtonPanelCommands(), OnSaveToExcel));
            BottomButtonPanel.RightButtons.Add(new SignleButtonModel("Odśwież dane produktu",
                new BottomButtonPanelCommands(), OnRefreshProductData));

            InventoryNames = new ObservableCollection<string>();

            InventoryModelList = new ObservableCollection<InventoryModel>();

        }

        public BottomButtonBarModel BottomButtonPanel { get; }

        private InventoryProvider _inventoryProvider;
        public InventoryProvider InventoryProvider
        {
            get { return _inventoryProvider; }
            set { _inventoryProvider = value; }
        }

        private ObservableCollection<InventoryModel> _inventoryModelList;
        public ObservableCollection<InventoryModel> InventoryModelList
        {
            get { return _inventoryModelList; }
            set { _inventoryModelList = value; }
        }

        public ObservableCollection<string> InventoryNames { get; set; }
        public string SelectedInventoryName { get; set; }

        public void AddInventoryNames(List<string>? namesList)
        {
            if (namesList == null) return;
            InventoryNames.Clear();
            namesList.ForEach(x => InventoryNames.Add(x));
        }

        private void OnRefreshProductData()
        {
            AddInventoryNames(InventoryProvider.GetInventoryNames());
        }

        private void OnSaveToExcel()
        {
            OpenFileDialog folderBrowser = new OpenFileDialog();
            // Set validate names and check file exists to false otherwise windows will
            // not let you select "Folder Selection."
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            // Always default to Folder Selection.
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == true)
            {
                string? folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                if(folderPath == null) return;

                
                
            }
        }

        private void OnClearList()
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz wyczyścić listę inwentaryzacji?", "Czyszczenie listy", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes) InventoryModelList.Clear();
        }

        private void OnGetInventoryFromDB()
        {
            if(string.IsNullOrEmpty(SelectedInventoryName))
            {
                MessageBox.Show("Wybierz nazwę inwentaryzacji i ponów próbę!");
                return;
            }

            if(InventoryModelList.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Lista inventaryzacji nie jest pusta. " +
                    "Jeśli pobierzesz informacje z bazy danych, obecnie wyświetlane dane zostaną ustracone." +
                    "Czy chcesz kontunuować", "Pobranie informacji z bazy danych", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.No) return;

                InventoryModelList.Clear();
            }

            List<InventoryModel> _inventoryModels = InventoryProvider.GetAllInventoryEntitiesByInventoryName(SelectedInventoryName);

            if (_inventoryModels.Count == 0)
            {
                MessageBox.Show("Nie znaleziono żadnej pozycji dla wybranej nazwy inwentaryzacji");
                return;
            }

            _inventoryModels.ToList().ForEach(x => InventoryModelList.Add(x));
            ;
        }

        private void OnCloseViewAction()
        {
            ;
        }
    }
}
