using NaturalnieApp2.Attributes;
using NaturalnieApp2.Interfaces.Database;
using NaturalnieApp2.Models;
using NaturalnieApp2.Services.Attributes;
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
    public partial class ShopProductSelector: UserControl
    {
        
        public ShopProductSelector()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty ModelProviderProperty =
            DependencyProperty.Register("ModelProvider", typeof(IGetModelProvider), typeof(ShopProductSelector), 
                new PropertyMetadata(new PropertyChangedCallback(OnModelProviderChanged)));

        public IGetModelProvider ModelProvider
        {
            get { return (IGetModelProvider)GetValue(ModelProviderProperty); }
            set { SetValue(ModelProviderProperty, value); } 
        }

        private static void OnModelProviderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IGetModelProvider modelProvider = e.NewValue as IGetModelProvider;
            Type modelType = modelProvider.GetModelType();
            List<PropertyDescriptor> nameAttributes = DisplayModelAttributesServices.GetPropertiesOfClass(modelType, typeof(DisplayModelAttributes.DisplayName));
            ;
        }


    }
    
   


}
