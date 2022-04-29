using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Interfaces.Barcode;
using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.Stores
{
    internal class NavigationDispatcher: INavigateToScreen
    {
        private IHostScreen _hostScreen;

        public IHostScreen HostScreen
        {
            get { return _hostScreen; }
            set { _hostScreen = value; }
        }

        public NavigationDispatcher()
        {

        }

 
        public void Navigate(NotifyPropertyChanged screenToShow)
        {
            _hostScreen.ShowScreen(screenToShow);
        }

        public void AddHostScreen(IHostScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public void OnKeyDown(Key key)
        {
            throw new NotImplementedException();
        }

        public void CloseScreen(NotifyPropertyChanged screenToClose)
        {
            _hostScreen.CloseViewModel(screenToClose);
        }
    }
}
