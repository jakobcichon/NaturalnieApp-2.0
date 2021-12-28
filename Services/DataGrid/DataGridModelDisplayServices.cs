using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.Services.DataGrid
{
    internal static class DataGridModelDisplayServices
    {
        public static void ApllyColumnProperties(Attribute attribute, DataGridColumn column)
        {
            if (attribute.GetType() == typeof(DataGridAttributes.DisplayName))
            {
                ColumnHeaderNameFromAttribute(attribute, column);
            }

            if (attribute.GetType() == typeof(DataGridAttributes.ColumnProperties))
            {
                ColumnDefaultVisibilityFromAttribute(attribute, column);

            }
        }

        public static void ColumnHeaderNameFromAttribute(Attribute attribute, DataGridColumn column)
        {
            column.Header = (attribute as DataGridAttributes.DisplayName).Name;
        }

        public static void ColumnDefaultVisibilityFromAttribute(Attribute attribute, DataGridColumn column)
        {
            if ((attribute as DataGridAttributes.ColumnProperties).HiddenByDefault == true)
            {
                column.Visibility = System.Windows.Visibility.Hidden;
                return;
            }

            column.Visibility = System.Windows.Visibility.Visible;
        }

    }
}
