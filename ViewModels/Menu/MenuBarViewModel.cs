using NaturalnieApp2.Commands;
using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Stores;
using NaturalnieApp2.ViewModels.MenuScreens;
using NaturalnieApp2.Views.Menu;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.ViewModels.Menu
{
    internal class MenuBarViewModel: NotifyPropertyChanged, ICommand
    {
        public MenuBarCollapseCommand ButtonCommand { get; set; }

        public NotifyPropertyChanged LogoClickScreen { get; }

        private INavigateToScreen ScreenDispatcher { get; }

        private ObservableCollection<MainButtonViewModel> _menuBarViews;

        public event EventHandler? CanExecuteChanged;

        public ObservableCollection<MainButtonViewModel> MenuBarViews
        {
            get { return _menuBarViews; }
            set { _menuBarViews = value; }
        }

        public MenuBarViewModel(NotifyPropertyChanged logoClickScreen, INavigateToScreen screenDispatcher)
        {
            _menuBarViews = new ObservableCollection<MainButtonViewModel>();  
            LogoClickScreen = logoClickScreen;
            ScreenDispatcher = screenDispatcher;
            ButtonCommand = new MenuBarCollapseCommand(); 
        }

        public void AddMenuBarMainButton(MainButtonViewModel menuBarMainButton)
        {
            _menuBarViews.Add(menuBarMainButton);
        }

        public void AddMenuBarMainButton(List<MainButtonViewModel> menuBarMainButton)
        {
            foreach (var menuItem in menuBarMainButton) { _menuBarViews.Add(menuItem); }
        }

        public void Execute(object? parameter)
        {
            ScreenDispatcher.Navigate(LogoClickScreen);
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }
    }
}
