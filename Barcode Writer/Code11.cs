using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class Code11 : BarcodeBase
    {
        public readonly static Code11 Instance;

        private Code11()
            : base()
        { }

        static Code11()
        {
            Instance = new Code11();
        }

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

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^[\\d-]+]$");
        }

        protected override string ParseText(string value, List<int> codes)
        {
            if (!IsValidData(value))
                throw new ApplicationException();

            value = "s" + value + "s";

            foreach (char item in value.ToCharArray())
            {
                codes.Add(item);
            }

            return value;
        }

        protected override int GetModuleWidth(BarcodeSettings settings)
        {
            return (2 * settings.WideWidth) + (3 * settings.NarrowWidth);
        }

        public static string AddCheckDigit(string value)
        {
            int tmp = 0;
            int weight = 0;
            for (int i = 0; i < value.Length; i++)
            {
                weight = ((value.Length - i) % 10);
                if (weight == 0)
                    weight = 10;
                tmp += ((value[i] == '-' ? 10 : int.Parse(value.Substring(i, 1))) * weight);
            }

            tmp = tmp % 11;
            if (tmp > 9)
                value += "-";
            else
                value = tmp.ToString();

            return value;
        }

        public static string AddDoubleCheckDigit(string value)
        {
            value = AddCheckDigit(value);

            int tmp = 0, weight = 0;
            for (int i = 0; i < value.Length; i++)
            {
                weight = (value.Length - i) % 9;
                if (weight == 0)
                    weight = 9;

                tmp += ((value[i] == '-' ? 10 : int.Parse(value.Substring(i, 1))) * weight);
            }

            tmp = tmp % 11;
            if (tmp > 9)
                return value + "-";
            else
                return value + tmp.ToString();
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, List<int> codes)
        {
            int[] shorts = new int[] { '0', '9', '-' };

            int c = (from t in codes where shorts.Contains(t) select t).Count();
            return width - settings.WideWidth + settings.NarrowWidth;
        }

        protected override void OnBeforeDrawModule(State state, int index)
        {
            if (index > 0)
                state.Left += state.Settings.ModulePadding;

            base.OnBeforeDrawModule(state, index);
        }

        protected override void OnAfterDrawModule(State state, int index)
        {
            int[] shorts = new int[] { '0', '9', '-' };
            if (shorts.Contains(state.ModuleValue))
                state.Left += state.Settings.NarrowWidth - state.Settings.WideWidth;
            else
                base.OnAfterDrawModule(state, index);
        }
    }
}
