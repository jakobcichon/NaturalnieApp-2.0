using NaturalnieApp2_Shared.Attributes;
using System.Windows.Data;

namespace NaturalnieApp2_Controls.Views.Controls.Models
{
    public class PropertyDisplayModel
    {
        public string Header { get; set; }
        public Binding Value { get; set; }
        public VisualRepresenationType Type { get; set; }
    }
}
