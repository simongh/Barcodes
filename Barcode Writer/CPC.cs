using System;
using System.Collections.Generic;
using System.Text;

namespace Barcode_Writer
{
    /// <summary>
    /// Canadian Post Office bar code
    /// </summary>
    public class CPC : BarcodeBase
    {
        private const int ALIGNMENTBAR = 0x100;
        private const int ODDCOUNT = 0x101;

        private Dictionary<string, int> _Lookup;

        protected override void Init()
        {
            CreateLookups();

            PatternSet = new Dictionary<int, Pattern>();
            for (int i = 0; i < 0x10; i++)
            {
                CreatePattern(i);
            }

            PatternSet.Add(ALIGNMENTBAR, Pattern.Parse("ww nb"));
            PatternSet.Add(ODDCOUNT, Pattern.Parse("ww, nw"));

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^[A-Z-[DFIOQU]]\\d[A-Z-[DFIOQU]]\\d[A-Z-[DFIOQU]]\\d$");
        }

        /// <summary>
        /// Create the conversion table
        /// </summary>
        private void CreateLookups()
        {
            _Lookup = new Dictionary<string, int>();
            _Lookup.Add("A", 0x07);
            _Lookup.Add("B", 0x0C);
            _Lookup.Add("C", 0x0B);
            _Lookup.Add("E", 0x0D);
            _Lookup.Add("G", 0x09);
            _Lookup.Add("H", 0x08);
            _Lookup.Add("J", 0x06);
            _Lookup.Add("K", 0x03);
            _Lookup.Add("L", 0x02);
            _Lookup.Add("M", 0x04);
            _Lookup.Add("N", 0x16);
            _Lookup.Add("P", 0x1C);
            _Lookup.Add("R", 0x05);
            _Lookup.Add("S", 0x0A);
            _Lookup.Add("T", 0x14);
            _Lookup.Add("V", 0x11);
            _Lookup.Add("W", 0x18);
            _Lookup.Add("X", 0x13);
            _Lookup.Add("Y", 0x0E);
            _Lookup.Add("Z", 0x1A);

            int[] numbers = new int[] { 0xA, 0x2, 0x9, 0x3, 0xB, 0x5, 0x6, 0x7, 0xD, 0xE };
            for (int i = 0; i < numbers.Length; i++)
            {
                _Lookup.Add(i.ToString(), numbers[i]);
            }

            //string alpha = "ABCEGHJKLMNPRSTVWYZ";
            //string tmp;
            //int value;
            //for (int i = 0; i < alpha.Length; i++)
            //{
            //    tmp = alpha.Substring(i, 1);
            //    for (int j = 0; j < numbers.Length; j++)
            //    {
            //        if (_Lookup[tmp] < 0x10)
            //            value = (_Lookup[tmp] * 0x10) + numbers[i];
            //        else if (_Lookup[tmp] == 0x1a)
            //            value = numbers[i] * 0x10;
            //        else
            //            value = (_Lookup[tmp] - 0x10) + (numbers[i] * 0x10);
            //        _Lookup.Add(tmp + i.ToString(), value);
            //    }
            //}

            _Lookup.Add("X0", 0x11);
            _Lookup.Add("X1", 0x14);
            _Lookup.Add("X2", 0x1C);
            _Lookup.Add("X3", 0x41);
            _Lookup.Add("X4", 0x44);
            _Lookup.Add("X5", 0x4C);
            _Lookup.Add("X6", 0xC1);
            _Lookup.Add("X7", 0xC4);
            _Lookup.Add("X8", 0xCC);
            _Lookup.Add("X9", 0x84);
        }

        /// <summary>
        /// Create binary patterns for 0x0 - 0xf
        /// </summary>
        /// <param name="value">value to create pattern for</param>
        private void CreatePattern(int value)
        {
            Elements[] tmp = new Elements[8];
            for (int i = 0; i < 8; i++)
            {
                if (i % 2 == 1)
                    tmp[8-i] = ((value >> (i / 2)) % 2) == 1 ? Elements.NarrowBlack : Elements.NarrowWhite;
                else
                    tmp[i] = Elements.WideWhite;
            }

            PatternSet.Add(value, new Pattern(tmp));
        }
        
        protected override string ParseText(string value, CodedValueCollection codes)
        {
            value = value.Replace(" ", "").ToUpper();
            if (!IsValidData(value))
                throw new ApplicationException("The data was not valid.");

            int tmp = ParsePair(value.Substring(0, 2));
            codes.Add(tmp / 0x10);
            codes.Add(tmp % 0x10);

            tmp = _Lookup[value.Substring(2, 1)];
            codes.Add(tmp / 0x10);
            codes.Add(tmp % 0x10);

            codes.Add(_Lookup[value.Substring(3, 1)]);

            tmp = ParsePair(value.Substring(4, 2));
            codes.Add(tmp / 0x10);
            codes.Add(tmp % 0x10);

            int p = 1;
            foreach (int item in codes)
            {
                p += PatternSet[item].BlackCount;
            }

            codes.Add(ALIGNMENTBAR);
            if (p % 2 == 0)
                codes.Insert(0, ALIGNMENTBAR);
            else
                codes.Insert(0, ODDCOUNT);

            return null;
        }

        /// <summary>
        /// Take a double digit pair & get it's hex value from the conversion table and rules
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private int ParsePair(string value)
        {
            if (_Lookup.ContainsKey(value))
                return _Lookup[value];

            int ho = _Lookup[value.Substring(0, 1)];
            int lo = _Lookup[value.Substring(1,1)];
            if (ho < 0x11)
                return (ho * 0x10) + lo;
            else if (ho == 0x11)
                return 0x10 + lo;
            else if (ho == 0x1a)
                return lo * 0x10;
            else if (ho == 0x16)
                return (lo * 0x10) + 1;
            else
                return (ho - 0x10) + (lo * 0x10);
        }

        protected override void OnBeforeDrawModule(State state, int index)
        {
            //digit 3 uses only 5 bits, so move back 3 bits
            if (index == 3)
                state.Left -= (state.Settings.NarrowWidth + state.Settings.WideWidth) * 3;

            base.OnBeforeDrawModule(state, index);
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
        {
            width += (settings.WideWidth + settings.NarrowWidth) * 27; //1+8+5+4+8+1

            return base.OnCalculateWidth(width, settings, codes);
        }

        public override BarcodeSettings GetDefaultSettings()
        {
            BarcodeSettings s = base.GetDefaultSettings();
            s.WideWidth = 4;
            s.IsTextShown = false;
            s.BarHeight = 10;
            s.ModulePadding = 0;
            return s;
        }
    }
}
