using NaturalnieApp2.Interfaces;
using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.ScreenCreator
{
    internal class InitialScreen
    { 
        public string mainButtonTittle;

        public ICommand commad;
         
        public Dictionary<string, NotifyPropertyChanged> buttonsList;

        public InitialScreen()
        {
            mainButtonTittle = "Inwentaryzacja";

        }
    }
}
