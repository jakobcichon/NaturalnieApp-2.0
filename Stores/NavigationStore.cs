using NaturalnieApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Stores
{
    internal class NavigationStore
    {
        public ViewModelBase CurrentViewModel { get; set; }
    }
}
