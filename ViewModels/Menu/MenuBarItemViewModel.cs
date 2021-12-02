using NaturalnieApp2.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.ViewModels.Menu
{

    internal class MenuBarItemViewModel
    {
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

        private ObservableCollection<string> _subButtonCollection;

        public ObservableCollection<string> SubButtonCollection
        {
            get { return _subButtonCollection; }
            set { _subButtonCollection = value; }
        }

        public MenuBarItemViewModel(string mainButtonTittle)
        {
            _mainButtonTitle = mainButtonTittle;
            _subButtonCollection = new ObservableCollection<string>();

            _mainButtonCommand = new MenuBarItemCommands();
        }

        public void AddSubButton(string name)
        {
            _subButtonCollection.Add(name);
        }
    }
}
