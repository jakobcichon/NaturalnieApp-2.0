using NaturalnieApp2.ViewModels.MenuScreens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NaturalnieApp2.ViewModels.Controls
{
    public class MenuScreenWithButtonBarViewModels: ViewModelBase
    {
        #region Buttons collection
        private ObservableCollection<MenuScreenButtonsBarViewModel> _buttonsCollection;

        public ObservableCollection<MenuScreenButtonsBarViewModel> ButtonsCollection
        {
            get { return _buttonsCollection; }
            set { _buttonsCollection = value; }
        }
        #endregion

        private ViewModelBase _currentScreen;

        public ViewModelBase CurrentScreen
        {
            get { return _currentScreen; }
            set { _currentScreen = value; }
        }


        public MenuScreenWithButtonBarViewModels()
        {
            ButtonsCollection = new ObservableCollection<MenuScreenButtonsBarViewModel>();
            ButtonsCollection.Add(new MenuScreenButtonsBarViewModel("Przycisk testowy", HorizontalAlignment.Left));
            ButtonsCollection.Add(new MenuScreenButtonsBarViewModel("Drugis asfafs sf afs ", HorizontalAlignment.Right));
            CurrentScreen = new ExecuteInventorizationViewModel();
        }

        public void AddButton(string title, HorizontalAlignment horizontalAligment)
        {
            ButtonsCollection.Add(new MenuScreenButtonsBarViewModel(title, horizontalAligment));
        }

    }

    public class MenuScreenButtonsBarViewModel: ViewModelBase
    {
        #region Buttons titles
        private string _buttonsTitles;

        public string ButtonsTitles
        {
            get { return _buttonsTitles; }
            set 
            { 
                _buttonsTitles = value; 
            }
        }
        #endregion

        private HorizontalAlignment _horizontalAligment;

        public HorizontalAlignment HorizontalAligment
        {
            get { return _horizontalAligment; }
            set { _horizontalAligment = value; }
        }

        public MenuScreenButtonsBarViewModel(string buttonTitle, HorizontalAlignment buttonHorizontalAligment)
        {
            _buttonsTitles= buttonTitle;
            _horizontalAligment= buttonHorizontalAligment;
        }

    }
}
