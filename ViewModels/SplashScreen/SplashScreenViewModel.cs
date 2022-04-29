using NaturalnieApp2.Interfaces.SplashScreen;
using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo("NaturalnieApp2.Tests")]
namespace NaturalnieApp2.ViewModels.SplashScreen
{
    internal class SplashScreenViewModel: NotifyPropertyChanged, ISplashScreen
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
