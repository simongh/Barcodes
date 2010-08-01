using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class Code2of5 : BarcodeBase
    {
        private const int START = 10;
        private const int STOP = 11;

        protected override void Init()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add(0, Pattern.Parse("nb nw nb nw wb nw wb nw nb"));
            PatternSet.Add(1, Pattern.Parse("wb nw nb nw nb nw nb nw wb"));
            PatternSet.Add(2, Pattern.Parse("nb nw wb nw nb nw nb nw wb"));
            PatternSet.Add(3, Pattern.Parse("wb nw wb nw nb nw nb nw nb"));
            PatternSet.Add(4, Pattern.Parse("nb nw nb nw wb nw nb nw wb"));
            PatternSet.Add(5, Pattern.Parse("wb nw wb nw nb nw nb nw nb"));
            PatternSet.Add(6, Pattern.Parse("nb nw wb nw wb nw nb nw nb"));
            PatternSet.Add(7, Pattern.Parse("nb nw nb nw nb nw wb nw wb"));
            PatternSet.Add(8, Pattern.Parse("wb nw nb nw nb nw wb nw nb"));
            PatternSet.Add(9, Pattern.Parse("nb nw wb nw nb nw wb nw nb"));

            PatternSet.Add(START, Pattern.Parse("nb nb nw nb nb nw nb"));
            PatternSet.Add(STOP, Pattern.Parse("nb nb nw nb nw nb nb"));

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d+");
        }

        public static void AddChecksumEventHandler(object sender, AddChecksumEventArgs e)
        {
            int total = 0;
            bool isEven = true;

            for (int i = e.Codes.Count-2; i < 0; i--)
            {
                total += isEven ? 3 * e.Codes[i] : e.Codes[i];
            }

            total = total % 10;
            total = total == 0 ? 0 : 10 - total;
            e.Codes.Insert(e.Codes.Count - 1, total);
            e.Text += total.ToString();
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            value = base.ParseText(value, codes);

            for (int i = 0; i < value.Length; i++)
            {
                codes.Add(int.Parse(value.Substring(i, 1)));
            }

            codes.Insert(0, START);
            codes.Add(STOP);

            return value;
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
        {
            width += (codes.Count * ((7 * settings.NarrowWidth) + (2 * settings.WideWidth)));
            width += (14 * settings.NarrowWidth);
            return base.OnCalculateWidth(width, settings, codes);
        }
    }
}
