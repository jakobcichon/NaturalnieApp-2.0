using NaturalnieApp2.Commands;
using NaturalnieApp2.Stores;
using NaturalnieApp2.ViewModels.MenuScreens;
using NaturalnieApp2.Views.Menu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.ViewModels.Menu
{
    internal class MenuBarViewModel: ViewModelBase
    {
        private ObservableCollection<MainButtonViewModel> _menuBarViews;

        public ObservableCollection<MainButtonViewModel> MenuBarViews
        {
            get { return _menuBarViews; }
            set { _menuBarViews = value; }
        }

        public MenuBarViewModel()
        {
            _menuBarViews = new ObservableCollection<MainButtonViewModel>();   
        }

        public void AddMenuBarMainButton(MainButtonViewModel menuBarMainButton)
        {
            _menuBarViews.Add(menuBarMainButton);
        }

        public void AddMenuBarMainButton(List<MainButtonViewModel> menuBarMainButton)
        {
            foreach (var menuItem in menuBarMainButton) { _menuBarViews.Add(menuItem); }
            ;
        }
    }
}
