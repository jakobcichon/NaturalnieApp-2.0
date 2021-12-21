using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Sandbox
{
    internal class SandboxViewModel : ViewModelBase
    {
        private ObservableCollection<string> _comboBoxDataSource;

        public ObservableCollection<string> ComboBoxDataSource
        {
            get { return _comboBoxDataSource; }
            set { _comboBoxDataSource = value; }
        }

        public SandboxViewModel()
        {
            ComboBoxDataSource = new ObservableCollection<string>() { "To jest test", "Orientana", "Test 2" };

       
        }

    }
}
