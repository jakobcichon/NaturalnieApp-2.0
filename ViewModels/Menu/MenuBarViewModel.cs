using NaturalnieApp2.Views.Menu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.ViewModels.Menu
{
    public class MenuBarViewModel: ViewModelBase
    {
        private ObservableCollection<MenuBarItemViewModel> _MenuBarViews;

        public ObservableCollection<MenuBarItemViewModel> MenuBarViews
        {
            get { return _MenuBarViews; }
            set { _MenuBarViews = value; }
        }

        public MenuBarViewModel()
        {
            MenuBarViews = new ObservableCollection<MenuBarItemViewModel>();
            MenuBarViews.Add(new MenuBarItemViewModel("Test1"));
            MenuBarViews.Add(new MenuBarItemViewModel("Test2"));

        }
    }
}
