using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class Code11 : BarcodeBase
    {
        protected override void Init()
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

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^[\\d-]+$");

            this.AddChecksum += new EventHandler<AddChecksumEventArgs>(Code11_AddChecksum);
        }

        void Code11_AddChecksum(object sender, AddChecksumEventArgs e)
        {
            DoChecksumCalculation(e, 10);

            if (e.Text.Length >= 10)
                DoChecksumCalculation(e, 9);
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            if (!IsValidData(value))
                throw new ApplicationException();

            string tmp = "s" + value + "s";

            foreach (char item in tmp.ToCharArray())
            {
                codes.Add(item);
            }

            return value;
        }

        protected override int GetModuleWidth(BarcodeSettings settings)
        {
            return (2 * settings.WideWidth) + (3 * settings.NarrowWidth);
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
                e.Codes.Add(tmp > 9 ? '-' : tmp + '0');
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
            width += (codes.Count - 1) * settings.ModulePadding;

            int[] shorts = new int[] { '0', '9', '-' };

            int c = (from t in codes where shorts.Contains(t) select t).Count();
            return width - (c * (settings.WideWidth - settings.NarrowWidth));
        }

        protected override void OnBeforeDrawModule(State state, int index)
        {
            base.OnBeforeDrawModule(state, index);
        }

        protected override void OnAfterDrawModule(State state, int index)
        {
            state.Left += state.Settings.ModulePadding;

            int[] shorts = new int[] { '0', '9', '-' };
            if (shorts.Contains(state.ModuleValue))
                state.Left += GetModuleWidth(state.Settings) + state.Settings.NarrowWidth - state.Settings.WideWidth;
            else
                base.OnAfterDrawModule(state, index);
        }
    }
}
