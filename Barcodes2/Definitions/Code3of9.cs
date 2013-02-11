using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes2.Definitions
{
	public class Code3of9 : DefaultDefinition
	{
		protected override Regex GetPattern()
		{
			return new Regex("^[A-Z0-9-\\. \\$/+%]+$", RegexOptions.IgnoreCase);
		}

		protected override void CreatePatternSet()
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
		}

		public override CodedValueCollection GetCodes(string value)
		{
			value = value.Trim('*');
			value = "*" + value.ToUpper() + "*";

			return base.GetCodes(value);
		}

		public override string GetDisplayText(string value)
		{
			value = value.Trim('*');
			return "*" + value.ToUpper() + "*";
		}

		public override int CalculateWidth(BarcodeSettings settings, CodedValueCollection codes)
		{
			return (codes.Count * ((3 * settings.WideWidth) + (6 * settings.NarrowWidth)));
		}
	}
}
