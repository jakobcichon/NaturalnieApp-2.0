using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NaturalnieApp2.Sandbox
{
    internal class SandboxViewModel : ViewModelBase
    {
        int i = 0;

        public void OnButtonClick()
        {
            //if(i==1) ComboBoxDataSource = new ObservableCollection<string> () { "Zmienione zrodlo"};
            if (i == 2) ComboBoxDataSource.Add("asfdasf");
            i++;
            ;

        }

        private ObservableCollection<string> _comboBoxDataSource;

        public ObservableCollection<string> ComboBoxDataSource
        {
            get { return _comboBoxDataSource; }
            set 
            { 
                _comboBoxDataSource = value; 
            }
        }

        public SandboxViewModel()
        {
            ComboBoxDataSource = new ObservableCollection<string>() { "To jest test", "Orientana", "Test 2" };
        }

    }


}
