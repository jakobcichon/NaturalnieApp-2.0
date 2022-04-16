using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NaturalnieApp2.Views.Controls.Models
{
    public class PropertyDisplayModel
    {
        public string Header { get; set; }
        public Binding Value { get; set; }
        public VisualRepresenationType Type { get; set; }
    }
}
