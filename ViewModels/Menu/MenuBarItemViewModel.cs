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
        private string? _mainMenuName;

        public string? MainMenuName
        {
            get { return _mainMenuName; }
            set { _mainMenuName = value; }
        }

        private ObservableCollection<string> _subMenuNames = new ObservableCollection<string>();

        public ObservableCollection<string> SubMenuNames { get { return _subMenuNames; } }  

        public MenuBarItemViewModel(string mainMenuName)
        {
            MainMenuName = mainMenuName;

        }

    }
}
