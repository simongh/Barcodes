using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    /// <summary>
    /// Universal Product Code (UPC) 6 digit code
    /// Also known as UPC-E or zero compressed
    /// </summary>
    public class UPCE : EAN
    {
        private int[] _DigitGrouping;

        protected override int[] DigitGrouping
        {
            get { return _DigitGrouping; }
        }

        public UPCE()
            : base()
        {
            _DigitGrouping = new int[] { 0, 6, 0 };
        }

        protected override void Init()
        {
            base.Init();

            PatternSet.Add(33, Pattern.Parse("0 1 0 1 0 1"));

            Parity.Clear();
            Parity.Add(new bool[] { true, true, true, false, false, false });
            Parity.Add(new bool[] { true, true, false, true, false, false });
            Parity.Add(new bool[] { true, true, false, false, true, false });
            Parity.Add(new bool[] { true, true, false, false, false, true });
            Parity.Add(new bool[] { true, false, true, true, false, false });
            Parity.Add(new bool[] { true, false, false, true, true, false });
            Parity.Add(new bool[] { true, false, false, false, true, true });
            Parity.Add(new bool[] { true, false, true, false, true, false });
            Parity.Add(new bool[] { true, false, true, false, false, true });
            Parity.Add(new bool[] { true, false, false, true, false, true });

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^([01]\\d{6,7}|\\d{11,12})$");
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            if (value.Length > 7)
            {
                value = FromUpcA(value);
            }

            return base.ParseText(value, codes).Substring(1, 6);
        }

        protected override void CalculateParity(CodedValueCollection codes)
        {
            int checkdigit;
            if (codes.Count == 8)
            {
                checkdigit = codes[7];
                codes.RemoveAt(7);
            }
            else
            {
                checkdigit = CalculateCheckCode(codes);
            }

            bool[] parity = Parity[checkdigit];

            for (int i = 0; i < codes.Count; i++)
            {
                if (parity[i] == (codes[0] == 0))
                    codes[i] += 10;
            }
        }

        protected virtual int CalculateCheckCode(CodedValueCollection codes)
        {
            int ld = codes[6];

            int total = (codes[2] * 3) + codes[1];
            switch (ld)
            {
                case 0:
                case 1:
                case 2:
                    total += (codes[3] * 3) + (codes[5] * 3) + ld + codes[4];
                    break;
                case 3:
                    total += (codes[5] * 3) + codes[3] + codes[4];
                    break;
                case 4:
                    total += (codes[4] * 3) + (codes[5] * 3) + codes[3];
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    total += (codes[4] * 3) + (ld * 3) + codes[3] + codes[4];
                    break;
                default:
                    throw new ApplicationException("Unexpected digit found.");
            }

            total = 10 - (total % 10);
            return total == 10 ? 0 : total;
        }

        protected override void OnBeforeDrawModule(State state, int index)
        {
            //No inter-module behaviour
        }

        protected override void OnAfterDrawCode(State state)
        {
            DrawGuardBar(state, (GuardType)33);
        }

        /// <summary>
        /// Converts this UPC-E to a UPC-A code
        /// </summary>
        /// <param name="value">UPC-E value</param>
        /// <returns>UPC-A compatible string</returns>
        public string ToUpcA(string value)
        {
            if (!IsValidData(value))
                throw new ApplicationException("The data was not valid.");

            List<int> codes = new List<int>();
            for (int i = 0; i < value.Length; i++)
            {
                codes.Add(int.Parse(value.Substring(i, 1)));

            }

            StringBuilder result = new StringBuilder();
            result.AppendFormat("0{0}00", value.Substring(0, 2));

            int total = (codes[1] * 3) + codes[0];
            switch (codes[5])
            {
                case 0:
                case 1:
                case 2:
                    total += (codes[2] * 3) + (codes[4] * 3) + codes[5] + codes[3];
                    result.Insert(3, codes[5]);
                    result.Insert(5, "00");
                    result.Append(value.Substring(2, 3));
                    break;
                case 3:
                    total += (codes[4] * 3) + codes[2] + codes[3];
                    result.Insert(3, codes[2]);
                    result.Insert(5, "00");
                    result.AppendFormat("0{0}", value.Substring(3, 2));
                    break;
                case 4:
                    total += (codes[3] * 3) + (codes[4] * 3) + codes[2];
                    result.Insert(3, value.Substring(2, 2));
                    result.Insert(5, 0);
                    result.AppendFormat("00{0}", codes[4]);
                    break;
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    total += (codes[3] * 3) + (codes[5] * 3) + codes[2] + codes[4];
                    result.Insert(3, value.Substring(2, 3));
                    result.AppendFormat("00{0}", codes[5]);
                    break;
                default:
                    throw new ApplicationException("Unexpected character in barcode.");
            }

            total = 10 - (total % 10);
            result.Append(total == 10 ? 0 : total);

            return result.ToString();
        }

        public string FromUpcA(string value)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[01]\d{2}([012]0{4}\d{3}|[3-9]0{4}\d{2}|\d{4}0{4}\d|\d{5}0{4}[5-9])"))
                throw new ArgumentException("UPC A code cannot be compressed.");

            StringBuilder result = new StringBuilder(value);

            if (result[5] != '0')
                result.Remove(6, 4);
            else if (result[4] != '0')
            {
                result.Remove(5, 5);
                result.Insert(6, "4");
            }
            else if (result[3] != '2' && result[3] != '1' && result[3] != '0')
            {
                result.Remove(4, 5);
                result.Insert(6, "3");
            }
            else
            {
                result.Insert(11, result[3]);
                result.Remove(3, 5);
            }

            return result.ToString();
        }
    }
}
