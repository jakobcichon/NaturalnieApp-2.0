using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces.Barcode
{
    internal interface IBarcodeListner
    {
        public void OnBarcodeValidAction(string barcode);
    }
}
