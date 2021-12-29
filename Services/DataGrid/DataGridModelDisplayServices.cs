﻿using NaturalnieApp2.Attributes;
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
            if (attribute.GetType() == typeof(DisplayModelAttributes.DisplayName))
            {
                ColumnHeaderNameFromAttribute(attribute, column);
            }

            if (attribute.GetType() == typeof(DisplayModelAttributes.VisibilityProperties))
            {
                ColumnDefaultVisibilityFromAttribute(attribute, column);

            }
        }

        public static void ColumnHeaderNameFromAttribute(Attribute attribute, DataGridColumn column)
        {
            column.Header = (attribute as DisplayModelAttributes.DisplayName).Name;
        }

        public static void ColumnDefaultVisibilityFromAttribute(Attribute attribute, DataGridColumn column)
        {
            if ((attribute as DisplayModelAttributes.VisibilityProperties).HiddenByDefault == true)
            {
                column.Visibility = System.Windows.Visibility.Hidden;
                return;
            }

            column.Visibility = System.Windows.Visibility.Visible;
        }

    }
}