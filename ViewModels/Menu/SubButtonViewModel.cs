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
        public ViewModelBase TargetScreen { get; }

        public string DisplayText { get; }

        private IScreenDispatcher _screenDispatcher { get; }

        public SubButtonViewModel(string displayText, ViewModelBase targetScreen, IScreenDispatcher screenDispatcher)
        {
            TargetScreen = targetScreen;
            DisplayText = displayText;


            _screenDispatcher = screenDispatcher;
        }

        public override void Execute(object? parameter)
        {
            _screenDispatcher.Navigate(TargetScreen);
        }
    }
}
