using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Interfaces.Barcode;
using NaturalnieApp2.Services.BarcodeReaderServices;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace NaturalnieApp2.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase, IHostScreen, IKeyDownListner
    {
        private readonly BarcodeReader BarcodeListner;

        private ViewModelBase _menuBarView;

        public ViewModelBase MenuBarView
        {
            get { return _menuBarView; }
            set { _menuBarView = value; }
        }

        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainWindowViewModel(ViewModelBase _menuBarViewModel, ViewModelBase initialScreen = null)
        {
            _menuBarView = _menuBarViewModel;
            CurrentView = initialScreen;

            //Create BarcodeReader instance
            BarcodeListner = new BarcodeReader(1000.0);
            BarcodeListner.BarcodeValid += BarcodeListner_BarcodeValid;
            
        }

        public void ShowScreen(ViewModelBase screenToShow)
        {
            CurrentView = screenToShow;
        }

        #region Barcode actions
        private void BarcodeListner_BarcodeValid(object sender, BarcodeReader.BarcodeValidEventArgs e)
        {
            Debug.WriteLine($"Barcode with value {e.RecognizedBarcodeValue} has been recognized");
            (CurrentView as IBarcodeListner)?.OnBarcodeValidAction(e.RecognizedBarcodeValue);

        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            BarcodeListner.CheckIfBarcodeFromReader(e.Key);
        }

        #endregion
    }
}
