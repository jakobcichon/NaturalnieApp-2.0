using NaturalnieApp2.Commands;
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


        /// <summary>
        /// Method used to add button to the collection
        /// </summary>
        /// <param name="name">Displayed name of the button</param>
        /// <param name="command">Command that will be executed after button pressed</param>
        public void AddSubButton(string name, ICommand command, object screenToDisplay)
        {
            _subButtonCollection.Add(new SubMenuBarItem(name, command, screenToDisplay));
        }
        #endregion



        public MenuBarItemViewModel(string mainButtonTittle)
        {
            _mainButtonTitle = mainButtonTittle;
            _mainButtonCommand = new MenuBarItemCommands();

            _subButtonCollection = new ObservableCollection<SubMenuBarItem>();
        }

    }

    internal class SubMenuBarItem
    {
        private string? _name;

        public string? Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private ICommand? _command;

        public ICommand? Command
        {
            get { return _command; }
            set { _command = value; }
        }

        private object _screenToDisplay;

        public object ScreenToDisplay
        {
            get { return _screenToDisplay; }
            set { _screenToDisplay = value; }
        }


        public SubMenuBarItem(string name, ICommand command, object screenToDisplay)
        {
            Name = name;
            Command = command;
            ScreenToDisplay = screenToDisplay;
        }


    }
}
