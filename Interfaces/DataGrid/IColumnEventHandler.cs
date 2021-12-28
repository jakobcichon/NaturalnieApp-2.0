using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.Interfaces.DataGrid
{
    internal interface IColumnEventHandler
    {
        void OnAutomaticColumnGenerating(object sender, DataGridAutoGeneratingColumnEventArgs e);
    }
}
