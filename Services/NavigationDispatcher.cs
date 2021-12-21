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
        private IHostScreen _HostScreen;

        public IHostScreen HostScreen
        {
            get { return _HostScreen; }
            set { _HostScreen = value; }
        }

        public NavigationDispatcher()
        {

        }

 
        public void Navigate(ViewModelBase screenToShow)
        {
            _HostScreen.ShowScreen(screenToShow);
        }

        public void AddHostScreen(IHostScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public void OnKeyDown(Key key)
        {
            throw new NotImplementedException();
        }
    }
}
