using System;
using System.Collections.Generic;

namespace Barcodes
{
    /// <summary>
    /// Codabar generator
    /// </summary>
    public class Codabar : BarcodeBase
    {
        public const string LIMITVALUEA = "A";
        public const string LIMITVALUEB = "B";
        public const string LIMITVALUEC = "C";
        public const string LIMITVALUED = "D";

        private const string _Extras = ":/.+abcdent*";
        
        protected override void Init()
        {
            AllowedCharsPattern = new System.Text.RegularExpressions.Regex(@"^[atbnc\*de][\d-$:/\.\+]+[atbnc\*de]$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            value = value.ToLower();
            string tmp = value.Replace(" ", "");

            tmp = base.ParseText(tmp, codes);

            foreach (char item in tmp.ToCharArray())
            {
                codes.Add(item);
            }

            return value.Substring(1, value.Length - 2);
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
        {
            foreach (int item in codes)
            {
                width += (PatternSet[item].NarrowCount * settings.NarrowWidth) + (PatternSet[item].WideCount * settings.WideWidth);
            }

            return base.OnCalculateWidth(width, settings, codes);
        }

        public static void AddChecksumEventHandler(object sender, AddChecksumEventArgs e)
        {
            string value = e.Text.Replace(" ", "");
            if (!System.Text.RegularExpressions.Regex.IsMatch(value, "^\\d+$"))
                throw new ArgumentException("Only numeric values can have a check digit");

            int total = 0;

            for (int i = 0; i < value.Length; i++)
            {
                if (i % 2 == 0)
                    total += int.Parse(value.Substring(i, 1));
                else
                {
                    int tmp = int.Parse(value.Substring(i, 1)) * 2;
                    total += (tmp % 9);
                }
            }

            total = total % 10;

            e.Text += total.ToString();
            e.Codes.Insert(e.Codes.Count - 1, total.ToString()[0]);
        }

        protected override void CreatePatternSet()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add('0', Pattern.Parse("nb nw nb nw nb ww wb"));
            PatternSet.Add('1', Pattern.Parse("nb nw nb nw wb ww nb"));
            PatternSet.Add('2', Pattern.Parse("nb nw nb ww nb nw wb"));
            PatternSet.Add('3', Pattern.Parse("wb ww nb nw nb nw nb"));
            PatternSet.Add('4', Pattern.Parse("nb nw wb nw nb ww nb"));
            PatternSet.Add('5', Pattern.Parse("wb nw nb nw nb ww nb"));
            PatternSet.Add('6', Pattern.Parse("nb ww nb nw nb nw wb"));
            PatternSet.Add('7', Pattern.Parse("nb ww nb nw wb nw nb"));
            PatternSet.Add('8', Pattern.Parse("nb ww wb nw nb nw nb"));
            PatternSet.Add('9', Pattern.Parse("wb nw nb ww nb nw nb"));

            PatternSet.Add('-', Pattern.Parse("nb nw nb ww wb nw nb"));
            PatternSet.Add('$', Pattern.Parse("nb nw wb ww nb nw nb"));
            PatternSet.Add(':', Pattern.Parse("wb nw nb nw wb nw wb"));
            PatternSet.Add('/', Pattern.Parse("wb nw wb nw nb nw wb"));
            PatternSet.Add('.', Pattern.Parse("wb nw wb nw wb nw nb"));
            PatternSet.Add('+', Pattern.Parse("nb nw wb nw wb nw wb"));

            PatternSet.Add('a', Pattern.Parse("nb nw wb ww nb ww nb"));
            PatternSet.Add('b', Pattern.Parse("nb ww nb ww nb nw wb"));
            PatternSet.Add('c', Pattern.Parse("nb nw nb ww nb ww wb"));
            PatternSet.Add('d', Pattern.Parse("nb nw nb ww wb ww nb"));
            PatternSet.Add('t', Pattern.Parse("nb nw wb ww nb ww nb"));
            PatternSet.Add('n', Pattern.Parse("nb ww nb ww nb nw wb"));
            PatternSet.Add('*', Pattern.Parse("nb nw nb ww nb ww wb"));
            PatternSet.Add('e', Pattern.Parse("nb nw nb ww wb ww nb"));
        }
    }
}
