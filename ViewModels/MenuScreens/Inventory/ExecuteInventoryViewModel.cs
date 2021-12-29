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
using NaturalnieApp2.Services.Database.Providers;
using NaturalnieApp2.Services.DataGrid;
using NaturalnieApp2.Services.DTOs;

namespace NaturalnieApp2.ViewModels.MenuScreens.Inventory
{
    internal class ExecuteInventoryViewModel: ViewModelBase, IBarcodeListner, IColumnEventHandler
    {
        public ProductProvider ModelProvider { get; set; }

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


    }
}
