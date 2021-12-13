using NaturalnieApp2.Interfaces;
using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Stores
{
    internal class NavigationDispatcher: INavigateToScreen
    {
        private IMainScreen _mainScreen;

        public IMainScreen MainScreen
        {
            get { return _mainScreen; }
            set { _mainScreen = value; }
        }

        public NavigationDispatcher(IMainScreen mainScreen)
        {
            _mainScreen = mainScreen;
        }

        public void Navigate(ViewModelBase screenToNavigate)
        {
            _mainScreen.ShowScreen(screenToNavigate);
        }

    }
}
