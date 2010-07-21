#define MEASURE
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    /// <summary>
    /// Code 3 of 9 barcode generator
    /// </summary>
    public class Code3of9 : BarcodeBase
    {

        internal Code3of9()
            : base()
        { }

        protected override void Init()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add('0', Pattern.Parse("nb nw nb ww wb nw wb nw nb"));
            PatternSet.Add('1', Pattern.Parse("wb nw nb ww nb nw nb nw wb"));
            PatternSet.Add('2', Pattern.Parse("nb nw wb ww nb nw nb nw wb"));
            PatternSet.Add('3', Pattern.Parse("wb nw wb ww nb nw nb nw nb"));
            PatternSet.Add('4', Pattern.Parse("nb nw nb ww wb nw nb nw wb"));
            PatternSet.Add('5', Pattern.Parse("wb nw nb ww wb nw nb nw nb"));
            PatternSet.Add('6', Pattern.Parse("nb nw wb ww wb nw nb nw nb"));
            PatternSet.Add('7', Pattern.Parse("nb nw nb ww nb nw wb nw wb"));
            PatternSet.Add('8', Pattern.Parse("wb nw nb ww nb nw wb nw nb"));
            PatternSet.Add('9', Pattern.Parse("nb nw wb ww nb nw wb nw nb"));
            PatternSet.Add('A', Pattern.Parse("wb nw nb nw nb ww nb nw wb"));
            PatternSet.Add('B', Pattern.Parse("nb nw wb nw nb ww nb nw wb"));
            PatternSet.Add('C', Pattern.Parse("wb nw wb nw nb ww nb nw nb"));
            PatternSet.Add('D', Pattern.Parse("nb nw nb nw wb ww nb nw wb"));
            PatternSet.Add('E', Pattern.Parse("wb nw nb nw wb ww nb nw nb"));
            PatternSet.Add('F', Pattern.Parse("nb nw wb nw wb ww nb nw nb"));
            PatternSet.Add('G', Pattern.Parse("nb nw nb nw nb ww wb nw wb"));
            PatternSet.Add('H', Pattern.Parse("wb nw nb nw nb ww wb nw nb"));
            PatternSet.Add('I', Pattern.Parse("nb nw wb nw nb ww wb nw nb"));
            PatternSet.Add('J', Pattern.Parse("nb nw nb nw wb ww wb nw nb"));
            PatternSet.Add('K', Pattern.Parse("wb nw nb nw nb nw nb ww wb"));
            PatternSet.Add('L', Pattern.Parse("nb nw wb nw nb nw nb ww wb"));
            PatternSet.Add('M', Pattern.Parse("wb nw wb nw nb nw nb ww nb"));
            PatternSet.Add('N', Pattern.Parse("nb nw nb nw wb nw nb ww wb"));
            PatternSet.Add('O', Pattern.Parse("wb nw nb nw wb nw nb ww nb"));
            PatternSet.Add('P', Pattern.Parse("nb nw wb nw wb nw nb ww nb"));
            PatternSet.Add('Q', Pattern.Parse("nb nw nb nw nb nw wb ww wb"));
            PatternSet.Add('R', Pattern.Parse("wb nw nb nw nb nw wb ww nb"));
            PatternSet.Add('S', Pattern.Parse("nb nw wb nw nb nw wb ww nb"));
            PatternSet.Add('T', Pattern.Parse("nb nw nb nw wb nw wb ww nb"));
            PatternSet.Add('U', Pattern.Parse("wb ww nb nw nb nw nb nw wb"));
            PatternSet.Add('V', Pattern.Parse("nb ww wb nw nb nw nb nw wb"));
            PatternSet.Add('W', Pattern.Parse("wb ww wb nw nb nw nb nw nb"));
            PatternSet.Add('X', Pattern.Parse("nb ww nb nw wb nw nb nw wb"));
            PatternSet.Add('Y', Pattern.Parse("wb ww nb nw wb nw nb nw nb"));
            PatternSet.Add('Z', Pattern.Parse("nb ww wb nw wb nw nb nw nb"));
            PatternSet.Add('-', Pattern.Parse("nb ww nb nw nb nw wb nw wb"));
            PatternSet.Add('.', Pattern.Parse("wb ww nb nw nb nw wb nw nb"));
            PatternSet.Add(' ', Pattern.Parse("nb ww wb nw nb nw wb nw nb"));
            PatternSet.Add('*', Pattern.Parse("nb ww nb nw wb nw wb nw nb"));
            PatternSet.Add('$', Pattern.Parse("nb ww nb ww nb ww nb nw nb"));
            PatternSet.Add('/', Pattern.Parse("nb ww nb ww nb nw nb ww nb"));
            PatternSet.Add('+', Pattern.Parse("nb ww nb nw nb ww nb ww nb"));
            PatternSet.Add('%', Pattern.Parse("nb nw nb ww nb ww nb ww nb"));

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^[A-Z0-9-\\. \\$/+%]+$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        protected override string ParseText(string value, List<int> codes)
        {
            value = value.Trim('*');
            if (!IsValidData(value))
                throw new ApplicationException("Invalid data for this barcode.");

            value = "*" + value.ToUpper() + "*";

            foreach (char item in value.ToCharArray())
            {
                codes.Add(item);
            }

            return value;
        }

        protected override int GetModuleWidth(BarcodeSettings settings)
        {
            return (3 * settings.WideWidth) + (6 * settings.NarrowWidth);
        }

        protected override void OnBeforeDrawModule(State state, int index)
        {
            if (index > 0)
                state.Left += state.Settings.ModulePadding;
            base.OnBeforeDrawModule(state, index);
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, List<int> codes)
        {
            return width + (settings.ModulePadding * (codes.Count - 1));
        }
    }
}
