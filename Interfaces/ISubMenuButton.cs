using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.Interfaces
{
    internal interface ISubMenuButton
    {
        string DisplayText { get; }
        ViewModelBase TargetScreen { get; set; }
        INavigateToScreen ScreenDispatcher { get; }
        IServiceProvider ScreenServiceProvider { get; }

    }
}
