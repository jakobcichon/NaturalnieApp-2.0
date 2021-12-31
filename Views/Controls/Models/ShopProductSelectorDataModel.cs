using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Views.Controls.Models
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
