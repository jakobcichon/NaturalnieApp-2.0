using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NaturalnieApp2.Interfaces.DataGrid
{
    public interface IDataGridAdditionalActionsEventHandler
    {
        public Action<int> OnCollectionElementChange { get; set; }

    }
}
