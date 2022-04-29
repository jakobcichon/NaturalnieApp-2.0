using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.ViewModels.Controls
{
    internal class BottomButtonBarViewModel: NotifyPropertyChanged
    {
        private ObservableCollection<BottomButtonBarButton> _leftAlignedButtons;

        public ObservableCollection<BottomButtonBarButton> LeftAlignedButtons
        {
            get { return _leftAlignedButtons; }
            set { _leftAlignedButtons = value; }
        }

        private ObservableCollection<BottomButtonBarButton> _rightAlignedButtons;

        public ObservableCollection<BottomButtonBarButton> RightAlignedButtons
        {
            get { return _rightAlignedButtons; }
            set { _rightAlignedButtons = value; }
        }

        public BottomButtonBarViewModel()
        {
            LeftAlignedButtons = new ObservableCollection<BottomButtonBarButton>();
            RightAlignedButtons = new ObservableCollection<BottomButtonBarButton>();
        }

        /// <summary>
        /// Method used to add left aligned button to the collection
        /// </summary>
        /// <param name="name">Displayed name of the button</param>
        /// <param name="command">Command that will be executed after button pressed</param>
        public void AddLeftButton(string name, ICommand command)
        {
            LeftAlignedButtons.Add(new BottomButtonBarButton(name, command));
        }

        /// <summary>
        /// Method used to add Right aligned button to the collection
        /// </summary>
        /// <param name="name">Displayed name of the button</param>
        /// <param name="command">Command that will be executed after button pressed</param>
        public void AddRightButton(string name, ICommand command)
        {
            RightAlignedButtons.Add(new BottomButtonBarButton(name, command));
        }

    }

    internal class BottomButtonBarButton
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private ICommand _command;

        public ICommand Command
        {
            get { return _command; }
            set { _command = value; }
        }

        public BottomButtonBarButton(string name, ICommand command)
        {
            Name = name;
            Command = command;
        }

    }
}
