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
    public partial class ShopProductSelector : UserControl
    {
        private static Type Type { get; set; }
        private static List<PropertyDescriptor> ToDisplayProperties {get; set;}
        public static ObservableCollection<ItemToDisplay> Items { get; set; }

        public ShopProductSelector()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<ItemToDisplay>();
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
            if (e.NewValue is not IGetModelProvider modelProvider) 
                throw new ArgumentException($"Type {e.NewValue} cannot be casted to the type of {typeof(IGetModelProvider)}");

            Type = modelProvider.GetModelType();

            //Check if any attribute to be displayed
            ToDisplayProperties = DisplayModelAttributesServices.GetPropertiesToBeDisplayed(Type);

            foreach (PropertyDescriptor property in ToDisplayProperties)
            {
                AddElementsWithoutValues(property);
            }
        }

        public void UpdateModelItems()
        {
            //Get all items
            List<object> _itmes = ModelProvider.GetAllModelData();

            foreach (object item in _itmes)
            {
                foreach (PropertyDescriptor property in ToDisplayProperties)
                {
                    AddElementToItems(property, item);
                }
            }
        }

        private static void AddElementsWithoutValues(PropertyDescriptor property)
        {
            bool? exist = CheckIfElementExistInItems(property);

            if (exist != null && exist != true)
            {
                AddNewElement(property);
                return;
            }

        }

        private void AddElementToItems(PropertyDescriptor property, object item)
        {
            bool? exist = CheckIfElementExistInItems(property);

            if (exist != null && exist == true)
            {
                AddExistingElement(property, item);
                return;
            }

            AddNewElement(property, item);
        }

        private static bool? CheckIfElementExistInItems(PropertyDescriptor property)
        {
            string? _displayName = DisplayModelAttributesServices.GetPropertyDisplayName(property);
            if (string.IsNullOrEmpty(_displayName)) return null;

            bool? itemExist = Items.Any(x => x.Name == _displayName);

            return itemExist;
        }

        private bool? CheckIfElementExistInValues(PropertyDescriptor property, object item)
        {
            ItemToDisplay? foundItem = Items.FirstOrDefault(e => e.Name == DisplayModelAttributesServices.GetPropertyDisplayName(property));
            object? _value = item.GetType().GetProperty(property.Name)?.GetValue(item, null);

            if (foundItem != null)
            {
                bool? exist = foundItem.Values.Contains(_value);
                return exist;
            }

            return null;

        }

        private void AddNewElement(PropertyDescriptor property, object item)
        {
            if (item == null) return;

            Items.Add(new ItemToDisplay());
            Items.Last().Name = DisplayModelAttributesServices.GetPropertyDisplayName(property);
            Items.Last().Values.Add(item.GetType().GetProperty(property.Name)?.GetValue(item, null));
        }

        private static void AddNewElement(PropertyDescriptor property)
        {
            Items.Add(new ItemToDisplay());
            Items.Last().Name = DisplayModelAttributesServices.GetPropertyDisplayName(property);
        }

        private void AddExistingElement(PropertyDescriptor property, object item)
        {
            if (item == null) return;

            ItemToDisplay? foundItem = Items.FirstOrDefault(e => e.Name == DisplayModelAttributesServices.GetPropertyDisplayName(property));

            if (foundItem != null)
            {
                bool? exist = CheckIfElementExistInValues(property, item);
                if (exist != null && exist != true)
                {
                    foundItem.Values.Add(item.GetType().GetProperty(property.Name)?.GetValue(item, null));
                }
            }


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdateModelItems();
        }

        public class ItemToDisplay
        {
            public string Name { get; set; }
            public List<object> Values { get; set; }

            public ItemToDisplay()
            {
                Values = new List<object>();
            }
        }


        public interface DataBindingSoure
        {
            public object GetName();
            public object GetValue();
        }

    }
    
   


}
