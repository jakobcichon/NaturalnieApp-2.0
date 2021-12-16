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
        private IHostScreen _mainScreen;

        public IHostScreen MainScreen
        {
            get { return _mainScreen; }
            set { _mainScreen = value; }
        }

        public NavigationDispatcher(IHostScreen mainScreen)
        {
            _mainScreen = mainScreen;
        }

        public void Navigate(ViewModelBase screenToShow)
        {
            _mainScreen.ShowScreen(screenToShow);
        }

    }
}
