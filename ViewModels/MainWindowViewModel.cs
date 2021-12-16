
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
using NaturalnieApp2.Interfaces;

namespace NaturalnieApp2.ViewModels
{
    internal class MainWindowViewModel: ViewModelBase, IHostScreen
    {

        private ViewModelBase _menuBarView;

        public ViewModelBase MenuBarView
        {
            get { return _menuBarView; }
            set { _menuBarView = value; }
        }

        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainWindowViewModel(ViewModelBase _menuBarViewModel, ViewModelBase initialScreen=null)
        {
            _menuBarView = _menuBarViewModel;
            CurrentView = initialScreen;
        }

        public void ShowScreen(ViewModelBase screenToShow)
        {
            CurrentView = screenToShow;
        }
    }
}
