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
        public ObservableCollection<ItemClass> Items {get; set;}


        public ShopProductSelector()
        {
            this.InitializeComponent();

            List<string> _testList = new List<string>();
            for (int i = 0; i < 5000; i++)
            {
                _testList.Add("sdgsdgsdgdsdsvgfsdgfsdgsdgdsgdsgdsgdsgdsgdsgsdgdsgsdgdsgdsgdsgsdgdsgdsgdsgwegewe23r3" + i.ToString());
            }

            ItemClass _item = new ItemClass();

            _item.Name = "Test name";
            _item.Values = _testList;

            Items = new ObservableCollection<ItemClass> { _item };
            
            for (int j = 0; j <20; j++)
            {
                Items.Add(_item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class ItemClass
    {
        public string Name { get; set; }
        public object Values { get; set; }
    }
    
   


}
