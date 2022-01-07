using System.Windows.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using NaturalnieApp2.Services.DataGrid;

namespace NaturalnieApp2.Views.Controls
{
    public class DataGridNaturalnieApp: DataGrid
    {
        private ToolTip _lastToolTip { get; set; }

        public DataGridNaturalnieApp()
        {
            this.MouseRightButtonUp += DataGridNaturalnieApp_MouseRightButtonUp;
            this.MouseMove += DataGridNaturalnieApp_MouseMove;
            this.AutoGeneratingColumn += DataGridNaturalnieApp_AutoGeneratingColumn;
        }

        private void DataGridNaturalnieApp_AutoGeneratingColumn(object? sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            AttributeCollection attributes = (e.PropertyDescriptor as PropertyDescriptor).Attributes;
            foreach (Attribute attribute in attributes)
            {
                DataGridModelDisplayServices.ApllyColumnProperties(attribute, e.Column);
                DataGridModelDisplayServices.ColumnModificationPropertiesFromAttribute(attribute, e.Column);
            }
        }

        private void DataGridNaturalnieApp_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (_lastToolTip != null)
            {
                _lastToolTip.IsOpen = false;
                _lastToolTip.UpdateLayout();
                _lastToolTip = null;
            }
        }

        private void DataGridNaturalnieApp_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
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
            if (itemType == null) return null;
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
    }
        
}
