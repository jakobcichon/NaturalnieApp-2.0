
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

namespace NaturalnieApp2.ViewModels
{
    internal class MainViewModel: ViewModelBase
    {
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
            set { _menuView = value; }
        }

        private List<ViewModelBase> _screenCollection { get; set; }

        public MainViewModel()
        {
            //Menu bar view
            MenuBarView = new MenuBarViewModel();

            //Current menu view
            MenuView = new MainScreenViewModel();

            AddScreen(ScreensDefinitions.ExecuteInventorization);
        }

        public void ChangeScreen(object screen)
        {
            MenuView = _screenCollection.FirstOrDefault(screen) as ViewModelBase;
        }

        public void AddScreen(ViewModelBase screen)
        {
            _screenCollection.Add(screen);
        }

    }
}
