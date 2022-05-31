using NaturalnieApp2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NaturalnieApp2.Attributes.DisplayModelAttributes;

namespace NaturalnieApp2.Models.MenuScreens
{
    internal record TaxModel: ModelBase
    {
        [NameToBeDisplayed("Nazwa dostawcy")]
        [VisibilityProperties(true, true)]
        [VisualRepresenation(VisualRepresenationType.Field)]
        public int TaxValue { get; set; }

    }
}
