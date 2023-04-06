using NaturalnieApp2_Controls.Attributes;
using NaturalnieApp2_Controls.Interfaces.Database;
using NaturalnieApp2_Controls.Models;
using NaturalnieApp2_Controls.Services.Attributes;
using NaturalnieApp2_Controls.Views.Controls.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace NaturalnieApp2_Controls.Views.Controls
{
    /// <summary>
    /// Interaction logic for ShopProductSelector.xaml
    /// </summary>
    public partial class ShopProductSelector : UserControl, INotifyPropertyChanged
    {
        #region Events args
        public class ElementSelectedEventArgs: EventArgs
        {
        }

        public class CancelFilterEventArgs : EventArgs
        {
        }
        public class FilterRequestEventArgs : EventArgs
        {
            public string ElementName { get; set; }
            public object ElementValue { get; set; }
        }
        #endregion

        #region Events
        public delegate void ElementSelectedHandler(ElementSelectedEventArgs e);
        public event ElementSelectedHandler ElementSelected;
        private void OnElementSelected()
        {
            ElementSelected?.Invoke(new ElementSelectedEventArgs());
        }

        public delegate void CancelFilterEventHandler(CancelFilterEventArgs e);
        public event CancelFilterEventHandler FilterCancel;
        private void OnCancelFilter()
        {
            FilterCancel?.Invoke(new CancelFilterEventArgs());
        }

        public delegate void FilterRequestEventHandler(FilterRequestEventArgs e);
        public event FilterRequestEventHandler FilterRequest;
        private void OnFilterRequest(string elementName, object? elementValue)
        {
            FilterRequest?.Invoke(new FilterRequestEventArgs() { ElementName=elementName, ElementValue=elementValue});
        }

        #endregion

        #region Dependecy properties


        public ShopProductSelectorDataModel ElementsToBeDisplayed
        {
            get { return (ShopProductSelectorDataModel)GetValue(ElementsToBeDisplayedProperty); }
            set { SetValue(ElementsToBeDisplayedProperty, value); }
        }

        public static readonly DependencyProperty ElementsToBeDisplayedProperty =
            DependencyProperty.Register("ElementsToBeDisplayed", typeof(ShopProductSelectorDataModel), typeof(ShopProductSelector), 
                new PropertyMetadata(null));

        #endregion

        private bool FilteringSource { get; set; }

        private bool collapsiblePanelVisible;
        public bool CollapsiblePanelVisible
        {
            get { return collapsiblePanelVisible; }
            set 
            { 
                collapsiblePanelVisible = value;
                OnPropertyChanged(nameof(CollapsiblePanelVisible));
            }
        }

        private Visibility _clearFilterButtonVisibility;

        public Visibility ClearFilterButtonVisibility
        {
            get { return _clearFilterButtonVisibility; }
            set 
            { 
                _clearFilterButtonVisibility = value; 
                OnPropertyChanged(nameof(ClearFilterButtonVisibility));
            }
        }

        public ShopProductSelector()
        {
            this.InitializeComponent();
            this.FilteringSource = false;
            this.CollapsiblePanelVisible = true;
            this.ClearFilterButtonVisibility = Visibility.Collapsed;

        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
/*            ComboBox? localSender = (sender as ComboBox);
            if (localSender == null) return;

            var originalSource = localSender.ItemsSource;

            localSender.ItemsSource = null;

            if (!FilteringSource)
            {
                object selectedItem = localSender?.SelectedItem;
                FilteringSource = true;

                string elementName = GetComboBoxElementName(localSender);
                if (elementName != null) OnFilterRequest(elementName, selectedItem);
                FilteringSource = false;
                localSender.ItemsSource = originalSource;
            }*/
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox? localSender = (sender as ComboBox);
            if (localSender == null) return;

            object selectedItem = localSender?.SelectedItem;

            string elementName = GetComboBoxElementName(localSender);
            if (elementName != null) OnFilterRequest(elementName, selectedItem);
        }

        private void CollapseButton_Click(object sender, RoutedEventArgs e)
        {
            if (CollapsablePanel.Visibility == Visibility.Collapsed)
            {
                CollapsablePanel.Visibility = Visibility.Visible;
                CollapsiblePanelVisible = true;
                return;
            }
            CollapsablePanel.Visibility = Visibility.Collapsed;
            CollapsiblePanelVisible = false;

        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Public methods
        public void ShowFilterCancelButton()
        {
            ClearFilterButtonVisibility = Visibility.Visible;
        }

        public void HideFilterCancelButton()
        {
            ClearFilterButtonVisibility = Visibility.Collapsed;
        }
        #endregion

        private string? GetComboBoxElementName(ComboBox comboBox)
        {
            FrameworkElement frameworkElement = comboBox as FrameworkElement;
            if (frameworkElement == null) return null;

            StackPanel? stackPanel = frameworkElement.Parent as StackPanel;
            if (stackPanel == null) return null;

            foreach (UIElement child in stackPanel.Children)
            {
                if (child as Label != null)
                {
                    return (child as Label).Content.ToString();
                }
            }

            return null;
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            OnElementSelected();
        }

        private void ClearFilterButton_Click(object sender, RoutedEventArgs e)
        {
            OnCancelFilter();
        }
    }





}
