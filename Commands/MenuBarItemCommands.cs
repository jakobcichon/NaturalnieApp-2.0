using NaturalnieApp2.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Commands
{
    internal class MenuBarItemCommands : CommandBase
    {


        public override void Execute(object? parameter)
        {
            MenuBarItemViewModel menuBarItemViewModel = parameter as MenuBarItemViewModel;

            if (menuBarItemViewModel != null)
            {
                if (menuBarItemViewModel.Visibility == System.Windows.Visibility.Visible)
                {
                    menuBarItemViewModel.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    menuBarItemViewModel.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
    }
}
