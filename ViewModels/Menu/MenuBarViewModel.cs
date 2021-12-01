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
        private ObservableCollection<MenuBarItemView> _MenuBarViews = new ObservableCollection<MenuBarItemView>();

        public ObservableCollection<MenuBarItemView> MenuBarViews
        {
            get { return _MenuBarViews; }
            set { _MenuBarViews = value; }
        }

        public MenuBarViewModel()
        {
            MenuBarViews.Add(new MenuBarItemView());
        }
    }
}
