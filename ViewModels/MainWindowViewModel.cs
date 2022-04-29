using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Interfaces.Barcode;
using NaturalnieApp2.Services.BarcodeReaderServices;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace NaturalnieApp2.ViewModels
{
    internal class MainWindowViewModel : NotifyPropertyChanged, IHostScreen, IKeyDownListner
    {
        private readonly BarcodeReader BarcodeListner;

        private NotifyPropertyChanged _menuBarView;

        public NotifyPropertyChanged MenuBarView
        {
            get { return _menuBarView; }
            set { _menuBarView = value; }
        }

        private NotifyPropertyChanged _currentView;

        public NotifyPropertyChanged CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainWindowViewModel(NotifyPropertyChanged _menuBarViewModel, NotifyPropertyChanged initialScreen = null)
        {
            _menuBarView = _menuBarViewModel;
            CurrentView = initialScreen;

            //Create BarcodeReader instance
            BarcodeListner = new BarcodeReader(100.0);
            BarcodeListner.BarcodeValid += BarcodeListner_BarcodeValid;
            
        }

        public void ShowScreen(NotifyPropertyChanged screenToShow)
        {
            CurrentView = screenToShow;
        }

        public void CloseViewModel(NotifyPropertyChanged screenToClose)
        {

        }

        #region Barcode actions
        private void BarcodeListner_BarcodeValid(object sender, BarcodeReader.BarcodeValidEventArgs e)
        {
            Debug.WriteLine($"Barcode with value {e.RecognizedBarcodeValue} has been recognized");
            (CurrentView as IBarcodeListner)?.OnBarcodeValidAction(e.RecognizedBarcodeValue);

        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            bool result = BarcodeListner.CheckIfBarcodeFromReader(e.Key);
            if (result)
            {
                e.Handled = true;
            }
        }


        #endregion
    }
}
