using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.ViewModels.Menu
{

    public class MenuBarItemViewModel: ViewModelBase
    {
        private string _menuName = "Default menu";
        public string MenuName
        {
            get { return _menuName; }
            set { _menuName = value; }
        }

        private ObservableCollection<Button> _subMenus = new ObservableCollection<Button>();

        public ObservableCollection<Button> SubMenus { get { return _subMenus; } }  

        public MenuBarItemViewModel()
        {
            SubMenus.Add(new Button() { Content="Test1"});
            SubMenus.Add(new Button() { Content = "Test2" });
        }

    }
}
