using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NaturalnieApp2_Controls.Views.Controls.Models
{
    public class ShopProductSelectorDataModel: ShopProductSelectorDataModelBase
    {
        private ObservableCollection<ShopProductSelectorDataSingleElement> _elements;

        public ObservableCollection<ShopProductSelectorDataSingleElement> Elements
        {
            get { return _elements; }
            set 
            { 
                _elements = value;
                OnPropertyChanged(nameof(Elements));
            }
        }

        public ShopProductSelectorDataModel()
        {
            Elements = new ObservableCollection<ShopProductSelectorDataSingleElement>();
        }

        public List<string> GetElementsNames()
        {
            List<string> names = new List<string>();

            names = Elements.Select( e => e.Name ).ToList();

            return names;
        }

        public ShopProductSelectorDataSingleElement? GetElementByName(string name)
        {
            return Elements.Where(e => e.Name == name)?.FirstOrDefault();
        }

        public object? GetElementValueByElementName(string name)
        {
            return GetElementByName(name)?.Value;
        }

        public void ClearAllValues()
        {
            foreach(ShopProductSelectorDataSingleElement element in Elements)
            {
                element.Value = "";
            }
        }

        public void AddElementsValues(ObservableCollection<ShopProductSelectorDataSingleElement> elements)
        {
            foreach (ShopProductSelectorDataSingleElement element in Elements)
            {
                ShopProductSelectorDataSingleElement? elementToAdd = elements.FirstOrDefault(e => e.Name == element.Name);
                if (elementToAdd != null)
                {
                    if(element.GetType() == elementToAdd.GetType())
                    {
                        element.Value = elementToAdd.Value;
                    }
                }
            }
        }

    }

    public class ShopProductSelectorDataSingleElement : ShopProductSelectorDataModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private object _value;

        public object Value
        {
            get { return _value; }
            set 
            { 
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        private Visibility elementVisible = Visibility.Collapsed;

        public Visibility ElementVisible
        {
            get { return elementVisible; }
            set 
            { 
                elementVisible = value; 
            }
        }

        public void MakeElementVisiable()
        {
            ElementVisible = Visibility.Visible;
        }

        public void MakeElementHidden()
        {
            ElementVisible = Visibility.Collapsed;
        }

    }

    public class ShopProductSelectorDataModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
