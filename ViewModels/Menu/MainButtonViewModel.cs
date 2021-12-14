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

    internal class MainButtonViewModel: MenuButtonBase
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

        private ICommand _command;

        public ICommand Command
        {
            get { return _command; }
            set { _command = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        #endregion

        #region Submenu buttons
        private ObservableCollection<SubButtonViewModel> _subButtonCollection;


        public ObservableCollection<SubButtonViewModel> SubButtonCollection
        {
            get { return _subButtonCollection; }
            set { _subButtonCollection = value; }
        }

        /// <summary>
        /// Method used to add button to the collection
        /// </summary>
        /// <param name="name">Displayed name of the button</param>
        /// <param name="command">Command that will be executed after button pressed</param>
        public void AddSubButton(string name, ViewModelBase screenToDisplay)
        {
            _subButtonCollection.Add(new SubButtonViewModel(name, screenToDisplay));
        }
        #endregion

        public MainButtonViewModel()
        {
        }

        private void ToggleMenu()
        {
            if (_visibility == Visibility.Visible) _visibility = Visibility.Collapsed;
            else _visibility = Visibility.Visible;
        }

        public override void Execute(object? parameter)
        {
            
        }
    }
}
