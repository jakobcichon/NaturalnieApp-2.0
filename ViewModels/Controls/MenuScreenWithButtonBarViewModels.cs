using NaturalnieApp2.Interfaces;
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
    internal class MenuScreenWithButtonBarViewModels: ViewModelBase
    {
        #region Buttons collection
        private ObservableCollection<MenuScreenButtonsBarViewModel> _buttonsCollection;

        public ObservableCollection<MenuScreenButtonsBarViewModel> ButtonsCollection
        {
            get { return _buttonsCollection; }
            set { _buttonsCollection = value; }
        }
        #endregion

        

        public MenuScreenWithButtonBarViewModels(IMenuScreen menuScreen)
        {
            ButtonsCollection = new ObservableCollection<MenuScreenButtonsBarViewModel>();
            ButtonsCollection.Add(new MenuScreenButtonsBarViewModel("title", HorizontalAlignment.Left));
            MenuScreen = new NaturalnieApp2.Controls.MenuScreenWithButtonBar();
        }

        public void AddButton(string title, HorizontalAlignment horizontalAligment)
        {
            ButtonsCollection.Add(new MenuScreenButtonsBarViewModel(title, horizontalAligment));
        }

    }

    internal class MenuScreenButtonsBarViewModel: ViewModelBase
    {
        #region Buttons titles
        private string _buttonsTitles;

        public string ButtonsTitles
        {
            get { return _buttonsTitles; }
            set { _buttonsTitles = value; }
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
