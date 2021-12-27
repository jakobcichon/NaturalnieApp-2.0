using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NaturalnieApp2.Interfaces.Barcode;
using NaturalnieApp2.Models;

namespace NaturalnieApp2.ViewModels.MenuScreens.Inventory
{
    internal class ExecuteInventoryViewModel: ViewModelBase, IBarcodeListner
    {
        public ICommand OnAutoGeneratingColumn { get; set; }

        public ICommand TestButton { get; set; }

        private List<Product> _bindingSource;

        public List<Product> BindingSource
        {
            get { return _bindingSource; }
            set { _bindingSource = value; }
        }

        public ExecuteInventoryViewModel()
        {
            TestButton = new TestButtonClass(this);
            OnAutoGeneratingColumn = new OnAutoGeneratingColumnEvent();
            BindingSource = new List<Product>() { new Product() { ProductName = "Test name" } };

        }

        public void OnBarcodeValidAction(string barcode)
        {
            throw new NotImplementedException();
        }

        internal class TestButtonClass : ICommand
        {
            ExecuteInventoryViewModel Vm;
            public event EventHandler? CanExecuteChanged;

            public TestButtonClass(ExecuteInventoryViewModel vm)
            {
                Vm = vm;
            }

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                Vm.BindingSource[0].ProductName = "Test2";
            }
        }

        internal class OnAutoGeneratingColumnEvent : ICommand
        {
            public event EventHandler? CanExecuteChanged;

            public OnAutoGeneratingColumnEvent()
            {
            }

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                ;
            }
        }
    }
}
