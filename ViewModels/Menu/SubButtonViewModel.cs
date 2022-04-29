using NaturalnieApp2.Commands;
using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NaturalnieApp2.ViewModels.Menu
{

    
    internal class SubButtonViewModel: MenuButtonBase, ISubMenuButton
    {
        public NotifyPropertyChanged TargetScreen { get; set; }

        public string DisplayText { get; }

        public INavigateToScreen ScreenDispatcher { get; }

        public IServiceProvider ScreenServiceProvider { get; }


        public SubButtonViewModel(string displayText, NotifyPropertyChanged targetScreen, INavigateToScreen screenDispatcher)
        {
            TargetScreen = targetScreen;
            DisplayText = displayText;
            ScreenDispatcher = screenDispatcher;
        }

        public override void Execute(object? parameter)
        {
            ScreenDispatcher.Navigate(TargetScreen);
        }
    }
}
