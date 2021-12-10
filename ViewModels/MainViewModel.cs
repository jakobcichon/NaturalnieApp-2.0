
using NaturalnieApp2.ViewModels.Menu;
using NaturalnieApp2.ViewModels.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using NaturalnieApp2.Views.Controls;
using NaturalnieApp2.ViewModels.MenuScreens;
using NaturalnieApp2.Stores;

namespace NaturalnieApp2.ViewModels
{
    internal class MainViewModel: ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        private ViewModelBase _menuBarView;

        public ViewModelBase MenuBarView
        {
            get { return _menuBarView; }
            set { _menuBarView = value; }
        }

        private ViewModelBase _menuView;

        public ViewModelBase MenuView
        {
            get { return _menuView; }
            set 
            { 
                _menuView = value; 
                OnPropertyChanged(nameof(MenuView));
            }
        }

        public MainViewModel(NavigationStore navigationStore)
        {
            //Menu bar view
            MenuBarView = new MenuBarViewModel(navigationStore);
            _navigationStore = navigationStore;
        }

    }
}
