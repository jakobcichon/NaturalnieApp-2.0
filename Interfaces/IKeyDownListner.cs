using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NaturalnieApp2.Interfaces
{
    internal interface IKeyDownListner
    {
        public void OnKeyDown(object sender, KeyEventArgs e);
    }
}
