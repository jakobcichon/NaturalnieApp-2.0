using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.Interfaces
{
    internal interface IScreenCreator
    {
        string mainButtonTittle { get; }
        ICommand commad { get; }
        Dictionary<string, ViewModelBase> buttonsList { get; }
    }
}
