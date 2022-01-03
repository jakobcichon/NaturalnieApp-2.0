using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.Views.Controls.Models
{
    public  class BottomButtonBarModel
    {
        public BottomButtonBarModel()
        {
            LeftButtons = new ObservableCollection<SignleButtonModel>();
            RightButtons = new ObservableCollection<SignleButtonModel>();  
        }

        private ObservableCollection<SignleButtonModel> _leftButtons;

        public ObservableCollection<SignleButtonModel> LeftButtons
        {
            get { return _leftButtons; }
            set { _leftButtons = value; }
        }

        private ObservableCollection<SignleButtonModel> _rightButtons;

        public ObservableCollection<SignleButtonModel> RightButtons
        {
            get { return _rightButtons; }
            set { _rightButtons = value; }
        }

    }

    public class SignleButtonModel
    {
        public SignleButtonModel(string buttonName, ICommand buttonCommand, Action additionalAction=null)
        {
            Name = buttonName;
            Command = buttonCommand;
            AdditionalAction = additionalAction;
        }

        public string Name { get; set; }
        public ICommand Command { get; set; }
        public Action AdditionalAction { get; set; }

        
    }

}
