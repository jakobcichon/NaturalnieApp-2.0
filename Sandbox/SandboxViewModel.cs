using NaturalnieApp2.Controls.NaturalnieMessageBox;
using NaturalnieApp2.Models;
using NaturalnieApp2.Services.Database.Providers;
using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NaturalnieApp2.Sandbox
{
    internal class SandboxViewModel : NotifyPropertyChanged
    {
        private Visibility messageVisiability;

        public Visibility MessageVisiability
        {
            get { return messageVisiability; }
            set 
            { 
                messageVisiability = value; 
                OnPropertyChanged(nameof(messageVisiability));
            }
        }

        private ProductModel displayModelTest;

        public ProductModel DisplayModelTest
        {
            get { return displayModelTest; }
            set { displayModelTest = value; }
        }



        int i = 0;

        private Command _buttonClick;

        public Command ButtonClick
        {
            get 
            { 
                if( _buttonClick == null )
                {
                    _buttonClick = new Command(OnButtonClick);
                }
                return _buttonClick; 
            }
            set { _buttonClick = value; }
        }
                

        public void OnButtonClick()
        {
            if (MessageVisiability == Visibility.Visible)
            {
                MessageVisiability = Visibility.Collapsed;
                return;
            }

            MessageVisiability = Visibility.Visible;
        }

        public SandboxViewModel(TaxProvider taxProvider)
        {
            MessageVisiability = Visibility.Visible;
            DisplayModelTest = new ProductModel(taxProvider);
            DisplayModelTest.ProductName = "Test name of product";
            DisplayModelTest.TaxValue = 8;
            Debug.WriteLine( DisplayModelTest.GetHashCode());
        }


        public class Command : ICommand
        {
            public event EventHandler? CanExecuteChanged;

            private Action _action;

            public Command(Action action)
            {
                _action = action;
            }

            public bool CanExecute(object? parameter)
            {
                return true;
            }

            public void Execute(object? parameter)
            {
                _action();
            }
        }
    }


}
