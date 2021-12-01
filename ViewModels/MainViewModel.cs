
using NaturalnieApp2.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
