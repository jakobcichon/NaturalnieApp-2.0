using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Windows.System;

namespace NaturalnieApp2.Services.BarcodeReaderServices
{

    /// <summary>
    /// Static class consist Barcode-related methods
    /// </summary>
    public static class BarcodeRelated
    {
        //====================================================================================================
        //User-defined exception
        #region User-defined exception
        public class BarcodeEAN8GeneratorException : Exception
        {
            public BarcodeEAN8GeneratorException()
            {
            }

            public BarcodeEAN8GeneratorException(string message)
                : base(message)
            {
            }

            public BarcodeEAN8GeneratorException(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
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

        public class ElementAlreadyExist : Exception
        {
            public ElementAlreadyExist()
            {
            }

            public ElementAlreadyExist(string message)
                : base(message)
            {
            }

            public ElementAlreadyExist(string message, Exception inner)
                : base(message, inner)
            {
            }
        }
        #endregion

        /// <summary>
        /// Method used to create EAN8. First 2 digits are the manufacturer id from DB.
        /// Last 5 digits of EAN8 are product ID from cash register.
        /// For date included it will be clarifier later.
        /// </summary>
        /// <param name="manufacturerId">Manufacturer ID sorted in DB</param>
        /// <param name="productId">Product ID in cash register, taken from DB</param>
        /// <returns>Valid EAN8 code</returns>
        #region Barcode methods
        public static string GenerateEan8(int manufacturerId, int productId)
        {
            //Local variables
            string retVal = "";
            string stringValue = "";

            if (manufacturerId >= 1 && manufacturerId <= 99)
            {
                if (productId >= 1 && productId <= 99999)
                {
                    stringValue = string.Format("{0,2}", manufacturerId.ToString()) + string.Format("{0,5}", productId.ToString());
                    stringValue = stringValue.Replace(" ", "0");
                    if (stringValue.Length == 7)
                    {
                        //Calculate checksum digit and add it to new code
                        retVal = CalcucateChekcSumOfBarcode(stringValue);
                    }
                    else throw new BarcodeEAN8GeneratorException("Błąd! Wygenerowany kod EAN8 nie ma 7 znaków!");
                }
                else throw new BarcodeEAN8GeneratorException("Błąd! Identyfikator produkty jest spoza zakresu 1-99999!");
            }
            else throw new BarcodeEAN8GeneratorException("Błąd! Identyfikator producenta jest spoza zakresu 1-99!");

            return retVal;
        }

        /// <summary>
        /// Method used to calculate EAN-13 or EAN-8 checksum character
        /// It will take 12 digits input for EAN-13 and 7 digits for EAN-8
        /// </summary>
        /// <param name="codeToCalculateFrom"></param>
        /// <returns> It will return given inputCode + calculated checksum.
        /// If wrong input code, than WrongBarcodeSeries exception will be thrown.
        /// </returns>
        public static string CalcucateChekcSumOfBarcode(string codeToCalculateFrom)
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

                //Made modulo and check what is the checksum number
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

        #endregion
    /// <summary>
    /// Class used to handle information received from Bar code reader
    /// </summary>
    #region Barcode reader
    public class BarcodeReader
    {
        //Object fields
        System.Timers.Timer timer { get; set; }
        public string BarcodeToReturn { get; set; }
        string TemporaryBarcodeValue { get; set; }
        public bool Ready { get; set; }
        public bool Valid { get; set; }
        private List<string> debugList { get; set; }

        //Register an event
        public event BarcodeValidEventHandler BarcodeValid;

        public class BarcodeValidEventArgs : EventArgs
        {
            public bool Ready { get; set; }
            public bool Valid { get; set; }
            public string RecognizedBarcodeValue { get; set; }
        }

        //Declare new event handler
        public delegate void BarcodeValidEventHandler(object sender, BarcodeValidEventArgs e);

        //Declaration of event handler
        protected virtual void OnBarcodeValid(BarcodeValidEventArgs e)
        {
            BarcodeValidEventHandler handler = BarcodeValid;
            handler?.Invoke(this, e);
        }

        /// <summary>
        /// CLass constructor
        /// </summary>
        /// <param name="barcodeReaderCharInterval"></param>
        public BarcodeReader(double barcodeReaderCharInterval)
        {
            //Initialize timer
            this.timer = new Timer(barcodeReaderCharInterval);
            this.timer.Elapsed += OnTimedEvent;
            this.timer.Enabled = true;

            this.TemporaryBarcodeValue = "";
            this.debugList = new List<string>();
            this.debugList.Add("");
            this.Ready = true;
        }


        /// <summary>
        /// Method used to recognize if valid Barcode value.
        /// It should be placed in object KEyDown event.
        /// </summary>
        /// <param name="key"></param>
        public bool CheckIfBarcodeFromReader(Key key)
        {

            //Make initialization after first call
            if (this.Ready == true)
            {
                this.timer.Start();
                this.Ready = false;
                this.BarcodeToReturn = "";
                this.Valid = false;
            }

            //Recognize only digits
            if (key == Key.D0|| key == Key.D1 || key == Key.D2 || key == Key.D3 || key == Key.D4 ||
                key == Key.D5 || key == Key.D6 || key == Key.D7 || key == Key.D8 || key == Key.D9)
            {
                //Reset timer
                this.timer.Stop();
                this.timer.Start();

                switch (key)
                {
                    case Key.D0:
                        this.TemporaryBarcodeValue += "0";
                        break;
                    case Key.D1:
                        this.TemporaryBarcodeValue += "1";
                        break;
                    case Key.D2:
                        this.TemporaryBarcodeValue += "2";
                        break;
                    case Key.D3:
                        this.TemporaryBarcodeValue += "3";
                        break;
                    case Key.D4:
                        this.TemporaryBarcodeValue += "4";
                        break;
                    case Key.D5:
                        this.TemporaryBarcodeValue += "5";
                        break;
                    case Key.D6:
                        this.TemporaryBarcodeValue += "6";
                        break;
                    case Key.D7:
                        this.TemporaryBarcodeValue += "7";
                        break;
                    case Key.D8:
                        this.TemporaryBarcodeValue += "8";
                        break;
                    case Key.D9:
                        this.TemporaryBarcodeValue += "9";
                        break;
                }

            }
            else if (key == Key.Enter)
            {
                this.timer.Stop();
                this.Ready = true;
                this.BarcodeToReturn = this.TemporaryBarcodeValue;
                this.TemporaryBarcodeValue = "";
                if (this.BarcodeToReturn.Length == 8 || this.BarcodeToReturn.Length == 12 || this.BarcodeToReturn.Length == 13)
                {
                    this.Valid = true;
                    CallBarcodeValidEvent(this.Ready, this.Valid, this.BarcodeToReturn);
                    return true;
                }
                else this.Valid = false;
            }

            this.debugList[debugList.Count - 1] += key;

            return false;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.timer.Stop();
            this.Ready = true;
            this.Valid = false;
            this.TemporaryBarcodeValue = "";
            if (this.debugList.Count >= 100) this.debugList.Clear();
        }


        private void CallBarcodeValidEvent(bool ready, bool valid, string barcode)
        {
            BarcodeValidEventArgs e = new BarcodeValidEventArgs
            {
                Ready = ready,
                Valid = valid,
                RecognizedBarcodeValue = barcode
            };
            OnBarcodeValid(e);
        }
    }
    #endregion
    
}
