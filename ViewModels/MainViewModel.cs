
using NaturalnieApp2.ViewModels.Menu;
using NaturalnieApp2.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using NaturalnieApp2.Controls;

namespace NaturalnieApp2.ViewModels
{
    internal class MainViewModel: ViewModelBase
    {
        private ViewModelBase _selectedView = new MainLayoutViewModel();

        public ViewModelBase SelectedView
        {
            get { return _selectedView; }
            set { _selectedView = value; }
        }

        private ViewModelBase _menuBarView = new MenuBarViewModel();

        public ViewModelBase MenuBarView
        {
            get { return _menuBarView; }
            set { MenuBarView = value; }
        }

        private UserControl _menuView;

        public UserControl MenuView
        {
            get { return _menuView; }
            set { _menuView = value; }
        }

        public MainViewModel()
        {
            MenuView = new MenuScreenWithButtonBar();
        }

    }
}
