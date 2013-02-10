using System;
using System.Collections.Generic;

namespace Barcodes
{
    public class Code11 : BarcodeBase
    {
        private const int LIMIT = 's';

        protected override void Init()
        {
            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^[\\d-]+$");

            this.AddChecksum += new EventHandler<AddChecksumEventArgs>(Code11_AddChecksum);
        }

        protected override void CreatePatternSet()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add('0', Pattern.Parse("nb nw nb nw wb"));
            PatternSet.Add('1', Pattern.Parse("wb nw nb nw wb"));
            PatternSet.Add('2', Pattern.Parse("nb ww nb nw wb"));
            PatternSet.Add('3', Pattern.Parse("wb ww nb nw nb"));
            PatternSet.Add('4', Pattern.Parse("nb nw wb nw wb"));
            PatternSet.Add('5', Pattern.Parse("wb nw wb nw nb"));
            PatternSet.Add('6', Pattern.Parse("nb ww wb nw nb"));
            PatternSet.Add('7', Pattern.Parse("nb nw nb ww wb"));
            PatternSet.Add('8', Pattern.Parse("wb nw nb ww nb"));
            PatternSet.Add('9', Pattern.Parse("wb nw nb nw nb"));
            PatternSet.Add('-', Pattern.Parse("nb nw wb nw nb"));
            PatternSet.Add('s', Pattern.Parse("nb nw wb ww nb"));
        }

        void Code11_AddChecksum(object sender, AddChecksumEventArgs e)
        {
            e.Codes.RemoveAt(e.Codes.Count - 1);
            DoChecksumCalculation(e, 10);

            if (e.Text.Length >= 10)
                DoChecksumCalculation(e, 9);

            e.Codes.Add(LIMIT);
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            if (!IsValidData(value))
                throw new ApplicationException();

            string tmp = string.Format("{1}{0}{1}", value, (char)LIMIT);

            foreach (char item in tmp.ToCharArray())
            {
                codes.Add(item);
            }

            return value;
        }

        protected void DoChecksumCalculation(AddChecksumEventArgs e, int factor)
        {
            int tmp = 0;
            int weight = 0;
            for (int i = 0; i < e.Text.Length; i++)
            {
                weight = ((e.Text.Length - i) % factor);
                if (weight == 0)
                    weight = factor;
                tmp += ((e.Text[i] == '-' ? 10 : int.Parse(e.Text.Substring(i, 1))) * weight);
            }

            tmp = tmp % 11;
            e.Text += tmp > 9 ? "-" : tmp.ToString();
            if (e.Codes != null)
            {
                if (e.Codes[e.Codes.Count - 1] == LIMIT)
                    e.Codes.Insert(e.Codes.Count - 1, tmp > 9 ? '-' : tmp + '0');
                else
                    e.Codes.Add(tmp > 9 ? '-' : tmp + '0');
            }
        }

        public string AddSingleCheckDigit(string value)
        {
            AddChecksumEventArgs e = new AddChecksumEventArgs(value, null);
            DoChecksumCalculation(e, 10);

            return e.Text;
        }

        public string AddDoubleCheckDigit(string value)
        {
            AddChecksumEventArgs e = new AddChecksumEventArgs(value, null);
            DoChecksumCalculation(e, 10);
            DoChecksumCalculation(e, 9);

            return e.Text;
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
        {
            foreach (int item in codes)
            {
                width += (PatternSet[item].WideCount * settings.WideWidth) + (PatternSet[item].NarrowCount * settings.NarrowWidth);
            }

            return base.OnCalculateWidth(width, settings, codes);
        }
    }
}
