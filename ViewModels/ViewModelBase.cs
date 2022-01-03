using NaturalnieApp2.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IViewScreen
    {
        public INavigateToScreen ScreenDipatcher { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void CloseScreen(ViewModelBase screenToClose)
        {
            ScreenDipatcher.CloseScreen(screenToClose);
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
