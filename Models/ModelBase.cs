using NaturalnieApp2.Attributes;
using NaturalnieApp2.Interfaces;
using NaturalnieApp2.Interfaces.Services.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Models
{
    public record ModelBase: IDisplayableModel, INotifyPropertyChanged
    {

        public bool HasDisplayableMembers => true;

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
