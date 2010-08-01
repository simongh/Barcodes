using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class Code93 : BarcodeBase
    {
        private const int SHIFT1 = 43;
        private const int SHIFT2 = 44;
        private const int SHIFT3 = 45;
        private const int SHIFT4 = 46;
        private const int LIMIT = 47;
        private const int TERMINATOR = 48;

        protected override void Init()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add(0, Pattern.Parse("100010100"));
            PatternSet.Add(1, Pattern.Parse("101001000"));
            PatternSet.Add(2, Pattern.Parse("101000100"));
            PatternSet.Add(3, Pattern.Parse("101000010"));
            PatternSet.Add(4, Pattern.Parse("100101000"));
            PatternSet.Add(5, Pattern.Parse("100100100"));
            PatternSet.Add(6, Pattern.Parse("100100010"));
            PatternSet.Add(7, Pattern.Parse("101010000"));
            PatternSet.Add(8, Pattern.Parse("100010010"));
            PatternSet.Add(9, Pattern.Parse("100001010"));
            PatternSet.Add(10, Pattern.Parse("110101000"));
            PatternSet.Add(11, Pattern.Parse("110100100"));
            PatternSet.Add(12, Pattern.Parse("110100010"));
            PatternSet.Add(13, Pattern.Parse("110010100"));
            PatternSet.Add(14, Pattern.Parse("110010010"));
            PatternSet.Add(15, Pattern.Parse("110001010"));
            PatternSet.Add(16, Pattern.Parse("101101000"));
            PatternSet.Add(17, Pattern.Parse("101100100"));
            PatternSet.Add(18, Pattern.Parse("101100010"));
            PatternSet.Add(19, Pattern.Parse("100110100"));
            PatternSet.Add(20, Pattern.Parse("100110010"));
            PatternSet.Add(21, Pattern.Parse("101011000"));
            PatternSet.Add(22, Pattern.Parse("101001100"));
            PatternSet.Add(23, Pattern.Parse("101000110"));
            PatternSet.Add(24, Pattern.Parse("100101100"));
            PatternSet.Add(25, Pattern.Parse("100010110"));
            PatternSet.Add(26, Pattern.Parse("110110100"));
            PatternSet.Add(27, Pattern.Parse("110110010"));
            PatternSet.Add(28, Pattern.Parse("110101100"));
            PatternSet.Add(29, Pattern.Parse("110100110"));
            PatternSet.Add(30, Pattern.Parse("110010110"));
            PatternSet.Add(31, Pattern.Parse("110011010"));
            PatternSet.Add(32, Pattern.Parse("101101100"));
            PatternSet.Add(33, Pattern.Parse("101100110"));
            PatternSet.Add(34, Pattern.Parse("100110110"));
            PatternSet.Add(35, Pattern.Parse("100111010"));
            PatternSet.Add(36, Pattern.Parse("100101110"));
            PatternSet.Add(37, Pattern.Parse("111010100"));
            PatternSet.Add(38, Pattern.Parse("111010010"));
            PatternSet.Add(39, Pattern.Parse("111001010"));
            PatternSet.Add(40, Pattern.Parse("101101110"));
            PatternSet.Add(41, Pattern.Parse("101110110"));
            PatternSet.Add(42, Pattern.Parse("110101110"));

            PatternSet.Add(SHIFT1, Pattern.Parse("100100110"));
            PatternSet.Add(SHIFT2, Pattern.Parse("111011010"));
            PatternSet.Add(SHIFT3, Pattern.Parse("111010110"));
            PatternSet.Add(SHIFT4, Pattern.Parse("100110010"));

            PatternSet.Add(LIMIT, Pattern.Parse("101011110"));
            PatternSet.Add(TERMINATOR, Pattern.Parse("1"));

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex(".+");

            AddChecksum += new EventHandler<AddChecksumEventArgs>(Code93_AddChecksum);
        }

        private void Code93_AddChecksum(object sender, AddChecksumEventArgs e)
        {
            AddCheckDigit(e, 20);
            AddCheckDigit(e, 15);
        }

        private void AddCheckDigit(AddChecksumEventArgs e, int weight)
        {
            int total = 0;
            int w = 1;
            for (int i = e.Codes.Count - 2; i > 0; i--)
            {
                total += (w * e.Codes[i]);
                w++;
                if (w > weight)
                    w = 1;
            }

            total = total % 47;
            e.Codes.Insert(e.Codes.Count - 2, total);
        }

        public override BarcodeSettings GetDefaultSettings()
        {
            BarcodeSettings S = base.GetDefaultSettings();
            S.ModulePadding = 0;

            return S;
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            value = base.ParseText(value, codes);

            string tmp = null;
            foreach (char item in value.ToCharArray())
            {
                if (item == ' ')
                    codes.Add(38);
                else if (item == '$')
                    codes.Add(39);
                else if (item == '/')
                    codes.Add(40);
                else if (item == '+')
                    codes.Add(41);
                else if (item == '%')
                    codes.Add(42);

                else if (item >= '0' && item <= '9')
                    codes.Add(item - 48);
                else
                {
                    tmp = AsciiEncoder.Lookup(item);

                    switch (tmp[0])
                    {
                        case '$':
                            codes.Add(SHIFT1);
                            codes.Add(tmp[1] - 55);
                            break;
                        case '%':
                            codes.Add(SHIFT2);
                            codes.Add(tmp[1] - 55);
                            break;
                        case '/':
                            codes.Add(SHIFT3);
                            codes.Add(tmp[1] - 55);
                            break;
                        case '+':
                            codes.Add(SHIFT4);
                            codes.Add(tmp[1] - 55);
                            break;
                        default:
                            codes.Add(tmp[0]);
                            break;
                    }
                }

            }

            codes.Insert(0, LIMIT);
            codes.Add(LIMIT);
            codes.Add(TERMINATOR);

            return value;
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
        {
            width += ((codes.Count - 1) * 9 * settings.NarrowWidth) + settings.NarrowWidth;

            return base.OnCalculateWidth(width, settings, codes);
        }
    }
}
