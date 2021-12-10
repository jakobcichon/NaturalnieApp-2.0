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

    internal class MenuBarItemViewModel: ViewModelBase
    {
        #region Visibility


        private Visibility _visibility;

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }
        #endregion

        #region Main button

        private ICommand _mainButtonCommand;

        public ICommand MainButtonCommand
        {
            get { return _mainButtonCommand; }
            set { _mainButtonCommand = value; }
        }

        private string _mainButtonTitle;

        public string MainButtonTittle
        {
            get { return _mainButtonTitle; }
            set { _mainButtonTitle = value; }
        }

        #endregion

        #region Submenu buttons
        private ObservableCollection<SubMenuBarItem> _subButtonCollection;


        public ObservableCollection<SubMenuBarItem> SubButtonCollection
        {
            get { return _subButtonCollection; }
            set { _subButtonCollection = value; }
        }

        private ICommand _subButtonCommand;

        public ICommand SubButtonCommand
        {
            get { return _subButtonCommand; }
            set { _subButtonCommand = value; }
        }


        /// <summary>
        /// Method used to add button to the collection
        /// </summary>
        /// <param name="name">Displayed name of the button</param>
        /// <param name="command">Command that will be executed after button pressed</param>
        public void AddSubButton(string name, ViewModelBase screenToDisplay)
        {
            _subButtonCollection.Add(new SubMenuBarItem(name, screenToDisplay));
        }
        #endregion



        public MenuBarItemViewModel(string mainButtonTittle, NavigationStore navigationStore)
        {
            MainButtonTittle = mainButtonTittle;
            MainButtonCommand = new MenuBarItemCommands();
            SubButtonCommand = new SubMenuItemCommands(navigationStore);
            SubButtonCollection = new ObservableCollection<SubMenuBarItem>();
        }

    }

    internal class SubMenuBarItem: ISelectableViewModel
    {
        private string? _name;

        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private ViewModelBase _targetViewModel { get; }

        public ViewModelBase TargetViewModel => _targetViewModel;

        public SubMenuBarItem(string name, ViewModelBase screenToDisplay)
        {
            Name = name;
            _targetViewModel = screenToDisplay;
        }


    }
}
