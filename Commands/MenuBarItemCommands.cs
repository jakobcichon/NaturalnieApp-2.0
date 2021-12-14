using NaturalnieApp2.ViewModels.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaturalnieApp2.ViewModels;
using NaturalnieApp2.Stores;
using NaturalnieApp2.Interfaces;

namespace NaturalnieApp2.Commands
{
    internal class MenuBarItemCommands : CommandBase
    {
        public override void Execute(object? parameter)
        {
            MainButtonViewModel menuBarItemViewModel = parameter as MainButtonViewModel;

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

    internal class SubMenuItemCommands: CommandBase
    {


        public SubMenuItemCommands()
        {
        }

        public override void Execute(object? parameter)
        {


        }
    }
}
