using NaturalnieApp2.Interfaces.DataGrid;
using NaturalnieApp2.Models.MenuScreens.Inventory;
using NaturalnieApp2.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
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
    public partial class ExecuteInventoryView: UserControl
    {
        private ViewBaseHelper ViewBaseHelperClass { get; set; }

        public ExecuteInventoryView()
        {
            InitializeComponent();
/*            DataGridSettingsActualState.AddDataGridReference(DataGridActualState);
            DataGridSettingsToDateState.AddDataGridReference(DataGridToDateState);*/

            ProductSelektor.FilterRequest += ProductSelektor_FilterRequest;
            ProductSelektor.ElementSelected += ProductSelektor_ElementSelected;
            ProductSelektor.FilterCancel += ProductSelektor_FilterCancel;
            ProductSelektor.Loaded += ProductSelektor_Loaded;

            DataContextChanged += ExecuteInventoryView_DataContextChanged;
            
            (DataGridActualState.Items as ICollectionView).CollectionChanged += DataGridActualState_CollectionChanged;
            (DataGridToDateState.Items as ICollectionView).CollectionChanged += DataGridToDateState_CollectionChanged;

            ViewBaseHelperClass = new ViewBaseHelper(this);

        }
        private void DataGridActualState_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ICollectionView? _localSender = sender as ICollectionView;
            if (_localSender == null) return;

            if (DataGridToDateState.Items.SortDescriptions != _localSender.SortDescriptions)
            {
                DataGridToDateState.Items.SortDescriptions.Clear();
                _localSender.SortDescriptions.ToList().ForEach(x => DataGridToDateState.Items.SortDescriptions.Add(x));
            }

            ScrollToOffset(DataGridActualState, _localSender.CurrentPosition);
            
        }

        private void DataGridToDateState_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            ;
        }

        private void ExecuteInventoryView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IProductSelectorHandler? viewModelAsSelectorHandler = e.NewValue as IProductSelectorHandler;
            if (viewModelAsSelectorHandler != null)
            {
                viewModelAsSelectorHandler.OnDataFiltered = (obj) =>
                {
                    if (obj == true) ProductSelektor.ShowFilterCancelButton();
                    else ProductSelektor.HideFilterCancelButton();
                };
            }

            IDataGridAdditionalActionsEventHandler? viewModelAsDataGridHandler = e.NewValue as IDataGridAdditionalActionsEventHandler;
            if (viewModelAsDataGridHandler != null)
            {
                viewModelAsDataGridHandler.OnCollectionElementChange = (obj) =>
                {
                    if (obj >= 0)
                    {
                        ScrollToOffset(DataGridActualState, obj);
                        (DataGridActualState.Items as ICollectionView)?.SortDescriptions.Clear();
                        DataGridActualState.SelectedIndex = obj;
                        DataGridActualState.Focus();
                    }

                };
            }
            ;
        }


        #region Private events
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
                productSelectorHndler.OnFilterRequest(e.ElementName, e.ElementValue);
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
        private void ProductSelektor_Loaded(object sender, RoutedEventArgs e)
        {
            IProductSelectorHandler productSelectorHndler = this.DataContext as IProductSelectorHandler;
            if (productSelectorHndler != null)
            {
                productSelectorHndler.OnProductSelectorLoaded();
            }
        }

        #endregion


        private void DataGridActualState_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollToOffset(DataGridToDateState, e.VerticalOffset);

        }

        private void DataGridToDateState_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {

            ScrollToOffset(DataGridActualState, e.VerticalOffset);
        }

        private static bool ScrollToOffset(DependencyObject n, double offset)
        {
            bool terminate = false;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(n); i++)
            {
                var child = VisualTreeHelper.GetChild(n, i);
                if (child is ScrollViewer)
                {
                    (child as ScrollViewer).ScrollToVerticalOffset(offset);
                    return true;
                }
            }
            if (!terminate)
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(n); i++)
                    terminate = ScrollToOffset(VisualTreeHelper.GetChild(n, i), offset);
            return false;
        }

    }


}
