using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NaturalnieApp2.Interfaces.Barcode;

namespace NaturalnieApp2.ViewModels.MenuScreens.Inventory
{
    internal class ExecuteInventoryViewModel: ViewModelBase, IBarcodeListner
    {
        private DataTable _bindingSource;

        public DataTable BindingSource
        {
            get { return _bindingSource; }
            set { _bindingSource = value; }
        }


        public ExecuteInventoryViewModel()
        {

        }

        public void OnBarcodeValidAction(string barcode)
        {
            throw new NotImplementedException();
        }
    }
}
