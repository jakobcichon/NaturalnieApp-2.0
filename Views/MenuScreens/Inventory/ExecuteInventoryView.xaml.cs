using NaturalnieApp2.Interfaces.DataGrid;
using NaturalnieApp2.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NaturalnieApp2.Views.MenuScreens.Inventory
{
    /// <summary>
    /// Interaction logic for ExecuteInventarizationView.xaml
    /// </summary>
    public partial class ExecuteInventoryView : UserControl
    {
        public ExecuteInventoryView()
        {
            InitializeComponent();
            DataGridSettingsActualState.AddDataGridReference(DataGridActualState);
            DataGridSettingsToDateState.AddDataGridReference(DataGridToDateState);

            ProductSelektor.FilterRequest += ProductSelektor_FilterRequest;
            ProductSelektor.ElementSelected += ProductSelektor_ElementSelected;
            ProductSelektor.FilterCancel += ProductSelektor_FilterCancel;
        }

        private void ProductSelektor_FilterCancel(Controls.ShopProductSelector.CancelFilterEventArgs e)
        {
            IProductSelectorHandler productSelectorHndler = this.DataContext as IProductSelectorHandler;
            if (productSelectorHndler != null)
            {
                productSelectorHndler.OnClearFilterRequest();
            }
        }

        private void ProductSelektor_FilterRequest(Controls.ShopProductSelector.FilterRequestEventArgs e)
        {
            IProductSelectorHandler productSelectorHndler = this.DataContext as IProductSelectorHandler;
            if (productSelectorHndler != null)
            {
                productSelectorHndler.OnFilterRequest();
            }
        }

        private void ProductSelektor_ElementSelected(Controls.ShopProductSelector.ElementSelectedEventArgs e)
        {
            IProductSelectorHandler productSelectorHndler = this.DataContext as IProductSelectorHandler;
            if (productSelectorHndler != null)
            {
                productSelectorHndler.OnElementSelected();
            }
        }

        private void ActualData_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid?.DataContext != null && dataGrid?.DataContext is IColumnEventHandler)
            {
                (dataGrid?.DataContext as IColumnEventHandler).OnAutomaticColumnGenerating(sender, e);
            }
        }
    }
}
