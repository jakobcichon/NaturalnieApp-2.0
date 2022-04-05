using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Interfaces.Barcode
{
    internal interface IBarcodeGenerator
    {
        public string GenerateInternalBarcode(string valueOfTheBarcode);
    }
}
