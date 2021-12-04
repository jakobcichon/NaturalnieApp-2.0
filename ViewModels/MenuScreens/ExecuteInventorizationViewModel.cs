using NaturalnieApp2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.ViewModels.MenuScreens
{
    internal class ExecuteInventorizationViewModel: ViewModelBase, IMenuScreen
    {
        private UserControl _screen;

        public UserControl Screen
        {
            get { return _screen; }
            set { _screen = value; }
        }

        public ExecuteInventorizationViewModel()
        {
            _screen = new Views.MenuScreens.ExecuteInventarizationView();
        }
    }
}
