using NaturalnieApp2.Attributes;
using NaturalnieApp2.Interfaces.Database;
using NaturalnieApp2.Models;
using NaturalnieApp2.Services.Attributes;
using NaturalnieApp2.Views.Controls.Models;
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

namespace NaturalnieApp2.Views.Controls
{
    /// <summary>
    /// Interaction logic for ShopProductSelector.xaml
    /// </summary>
    public partial class ShopProductSelector : UserControl, INotifyPropertyChanged
    {
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
 
        public ShopProductSelector()
        {
            this.InitializeComponent();
            CollapsiblePanelVisible = true;
        }

        public static readonly DependencyProperty ModelProviderProperty =
            DependencyProperty.Register("ModelProvider", typeof(IGetModelProvider), typeof(ShopProductSelector), 
                new PropertyMetadata(new PropertyChangedCallback(OnModelProviderChanged)));

        public event PropertyChangedEventHandler? PropertyChanged;

        public ShopProductSelectorDataModel ModelProvider
        {
            get { return (ShopProductSelectorDataModel)GetValue(ModelProviderProperty); }
            set { SetValue(ModelProviderProperty, value); } 
        }

        private static void OnModelProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShopProductSelector)?.OnModelProviderChanged();
        }

        public void OnModelProviderChanged()
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
    }

    
   


}
