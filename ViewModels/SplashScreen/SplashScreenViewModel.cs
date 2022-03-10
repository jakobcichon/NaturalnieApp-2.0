using NaturalnieApp2.Interfaces.SplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.ViewModels.SplashScreen
{
    internal class SplashScreenViewModel: ViewModelBase, ISplashScreen
    {
        private string actualText = "";

        public string ActualText
        {
            get { return actualText; }
            set 
            { 
                actualText = value;
                OnPropertyChanged(nameof(ActualText));
            }
        }

        private readonly string windowTitle = "Inicjalizacja NaturalnieApp2.0";

        public string WindowTitle
        {
            get { return windowTitle; }
        }


        public void UpdateText(string text)
        {
            ActualText = text;
        }




    }
}
