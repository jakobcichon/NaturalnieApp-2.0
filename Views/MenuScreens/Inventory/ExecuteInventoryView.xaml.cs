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
    public partial class ExecuteInventoryView : UserControl
    {
        private ToolTip _lastToolTip { get; set; }

        public ExecuteInventoryView()
        {
            InitializeComponent();
            DataGridSettingsActualState.AddDataGridReference(DataGridActualState);
            DataGridSettingsToDateState.AddDataGridReference(DataGridToDateState);

            ProductSelektor.FilterRequest += ProductSelektor_FilterRequest;
            ProductSelektor.ElementSelected += ProductSelektor_ElementSelected;
            ProductSelektor.FilterCancel += ProductSelektor_FilterCancel;
            ProductSelektor.Loaded += ProductSelektor_Loaded;

            DataContextChanged += ExecuteInventoryView_DataContextChanged;

            DataGridActualState.MouseRightButtonUp += DataGridActualState_MouseRightButtonUp;
            DataGridActualState.MouseMove += DataGridActualState_MouseMove;

            (DataGridActualState.Items as ICollectionView).CollectionChanged += DataGridActualState_CollectionChanged;
            (DataGridToDateState.Items as ICollectionView).CollectionChanged += DataGridToDateState_CollectionChanged;
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

        #region General methods
        private string? GetAllColumnValuesTillGivenCell(DataGrid? dataGrid, DataGridCell referenceCell)
        {
            if (dataGrid == null || referenceCell == null) return null;

            int currentItemIndex = dataGrid.SelectedIndex;
            string? propertyName = referenceCell.Column.SortMemberPath;
;
            ItemCollection items = dataGrid.Items;

            //Check if item is numeric
            PropertyInfo? propertyInfo = items[0].GetType()?.GetProperty(propertyName);
            Type itemType = propertyInfo.PropertyType;
            if(itemType == null) return null;
            try
            {
                if (itemType.IsValueType)
                {
                    dynamic itemValue = 0;
                    foreach (var item in items)
                    {
                        if (dataGrid.Items.IndexOf(item) > currentItemIndex) break;
                        dynamic currentValue = item.GetType().GetProperty(propertyName).GetValue(item);
                        if (currentValue != null) itemValue = itemValue + currentValue;
                    }
                    return $"Suma elementów kolumny '{referenceCell.Column.Header.ToString()}': " + itemValue;
                }
                else
                {
                    int itemCount = 0;
                    foreach (var item in items)
                    {
                        if (dataGrid.Items.IndexOf(item) > currentItemIndex) break;
                        itemCount += 1;
                    }
                    return $"Liczba elementów kolumny '{referenceCell.Column.Header.ToString()}': " + itemCount;
                }
            }
            catch
            {
                return null;
            }

            return null;
        }
        #endregion

        private void DataGridActualState_MouseMove(object sender, MouseEventArgs e)
        {
            if (_lastToolTip != null)
            {
                _lastToolTip.IsOpen = false;
                _lastToolTip.UpdateLayout();
                _lastToolTip = null;
            }

        }

        private void DataGridActualState_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var hit = VisualTreeHelper.HitTest((Visual)sender, e.GetPosition((IInputElement)sender));
            DependencyObject cell = VisualTreeHelper.GetParent(hit.VisualHit);
            while (cell != null && !(cell is DataGridCell)) cell = VisualTreeHelper.GetParent(cell);
            DataGridCell targetCell = cell as DataGridCell;

            if (targetCell == null) return;

            object? valueToDisplay = GetAllColumnValuesTillGivenCell(sender as DataGrid, targetCell);

            ToolTip? targetCellToolTip = targetCell.ToolTip as ToolTip;
            if (targetCellToolTip == null) targetCellToolTip = new ToolTip();

            if (_lastToolTip != null)
            {
                _lastToolTip.IsOpen = false;
                _lastToolTip.UpdateLayout();
                _lastToolTip = null;
            }


            targetCellToolTip.Content = valueToDisplay?.ToString();
            targetCellToolTip.IsOpen = true;
            _lastToolTip = targetCellToolTip;
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

        private void ActualData_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid?.DataContext != null && dataGrid?.DataContext is IDataGridAdditionalActionsEventHandler)
            {
                (dataGrid?.DataContext as IDataGridAdditionalActionsEventHandler).OnAutomaticColumnGenerating(sender, e);
            }
        }

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
