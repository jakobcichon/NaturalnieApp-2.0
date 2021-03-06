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
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        #endregion

        #region Submenu buttons
        private ObservableCollection<ISubMenuButton> _subButtonCollection;


        public ObservableCollection<ISubMenuButton> SubButtonCollection
        {
            get { return _subButtonCollection; }
            set { _subButtonCollection = value; }
        }

        /// <summary>
        /// Method used to add button to the collection
        /// </summary>
        /// <param name="name">Displayed name of the button</param>
        /// <param name="command">Command that will be executed after button pressed</param>
        public void AddSubButton(List<ISubMenuButton> button)
        {
            foreach (var subButton in button)
            {
                _subButtonCollection.Add(subButton);
            }
        }
        #endregion

        public MainButtonViewModel(string displayText)
        {
            Name = displayText;
            _subButtonCollection = new ObservableCollection<ISubMenuButton>();
        }

        private void ToggleMenu()
        {
            if (Visibility == Visibility.Visible) Visibility = Visibility.Collapsed;
            else Visibility = Visibility.Visible;
        }

        public override void Execute(object? parameter)
        {
            ToggleMenu();
        }
    }
}
