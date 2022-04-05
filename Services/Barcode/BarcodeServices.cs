using NaturalnieApp2.Interfaces.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.Barcode
{
    internal class BarcodeServices : IBarcodeGenerator
    {
        /// <summary>
        /// Method used to generate EAN8 code from given 7 digit data part
        /// </summary>
        /// <param name="dataPartOfTheBarcode">7 digit barcode data part</param>
        /// <returns></returns>
        public string GenerateInternalBarcode(string dataPartOfTheBarcode)
        {
            ValidateBarcodeValuePart(dataPartOfTheBarcode);

            return CalcucateChekcSumOfBarcode(dataPartOfTheBarcode);
        }

        private static bool ValidateBarcodeValuePart(string dataPartOfTheBarcode)
        {
            if (dataPartOfTheBarcode == null) throw new ArgumentNullException(nameof(dataPartOfTheBarcode));

            if (dataPartOfTheBarcode.Length != 7) throw new WrongBarcodeValueLength("Zła długość wartości kodu EAN8. Wymagana długość to 7 znaków." +
                $"Podano {dataPartOfTheBarcode.Length} znaków");

            return true;
        }

        /// <summary>
        /// Method used to calculate EAN-13 or EAN-8 checksum charakter
        /// It will take 12 digits input for EAN-13 and 7 digits for EAN-8
        /// </summary>
        /// <param name="codeToCalculateFrom"></param>
        /// <returns> It will return given inputCode + calculated checksum.
        /// If wrong iput code, than WrongBarcodeSeries exception will be thrown.
        /// </returns>
        private static string CalcucateChekcSumOfBarcode(string codeToCalculateFrom)
        {
            //Local variables
            string retVal = "";
            int numberOfDigits;
            string stringValue;
            int intValue = 0;

            //Check if given sring contains only digits
            Regex regEx = new Regex(@"^[0-9]*$");
            bool onlyDigits = regEx.IsMatch(codeToCalculateFrom);
            int checksumValue = 0;
            int checksumDigit = 0;
            bool multiple = true;

            //Check if length has 7 or 12 digits
            numberOfDigits = codeToCalculateFrom.Count();

            //Do calculation
            if (onlyDigits && (numberOfDigits == 7 || numberOfDigits == 12))
            {
                for (int i = numberOfDigits - 1; i >= 0; i--)
                {
                    if (multiple)
                    {

                        stringValue = codeToCalculateFrom[i].ToString();
                        intValue = Convert.ToInt32(stringValue);
                        checksumValue += (intValue * 3);
                        multiple = false;
                    }
                    else
                    {
                        stringValue = codeToCalculateFrom[i].ToString();
                        intValue = Convert.ToInt32(stringValue);
                        checksumValue += intValue;
                        multiple = true;
                    }

                }

                //Made modulo annd check what is the checksum number
                checksumDigit = 10 - (checksumValue % 10);
                if (checksumDigit == 10) checksumDigit = 0;

                retVal = (codeToCalculateFrom + checksumDigit.ToString());

            }
            else throw new WrongBarcodeSeries(string.Format("Nie można wyliczyć cyfry kontrolnej dla '{0}'. " +
                "Dopuszczalne są jednynie kody EAN8 oraz EAN13, dla których liczba cyfr bez cyfry kontrolenj to odpowiednio 7 i 12 znaków.",
                codeToCalculateFrom));

            return retVal;
        }
    }

    //User-defined exception
    #region User-defined exception
    public class WrongBarcodeSeries : Exception
    {
        public WrongBarcodeSeries()
        {
        }

        public WrongBarcodeSeries(string message)
            : base(message)
        {
        }

        public WrongBarcodeSeries(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    public class WrongBarcodeValueLength : Exception
    {
        public WrongBarcodeValueLength()
        {
        }

        public WrongBarcodeValueLength(string message)
            : base(message)
        {
        }

        public WrongBarcodeValueLength(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    #endregion
}
