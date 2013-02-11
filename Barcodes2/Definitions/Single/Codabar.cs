using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes2.Definitions.Single
{
	public class Codabar : DefaultDefinition
	{
		public const string LIMITVALUEA = "A";
		public const string LIMITVALUEB = "B";
		public const string LIMITVALUEC = "C";
		public const string LIMITVALUED = "D";

		private const string _Extras = ":/.+abcdent*";

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

		protected override Regex GetRegex()
		{
			return new Regex(@"^[atbnc\*de][\d-$:/\.\+]+[atbnc\*de]$", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
		}

		public override CodedValueCollection GetCodes(string value)
		{
			value = value.ToLower();
			value = value.Replace(" ", "");

			return base.GetCodes(value);
		}

		public override string GetDisplayText(string value)
		{
			value = value.ToLower();
			return value.Substring(1, value.Length - 2);
		}

		public override string AddChecksum(string value, CodedValueCollection codes)
		{
			var parsed = value.Replace(" ", "");
			if (!System.Text.RegularExpressions.Regex.IsMatch(value, "^\\d+$"))
				throw new ArgumentException("Only numeric values can have a check digit");

			int total = 0;

			for (int i = 0; i < parsed.Length; i++)
			{
				if (i % 2 == 0)
					total += int.Parse(parsed.Substring(i, 1));
				else
				{
					int tmp = int.Parse(parsed.Substring(i, 1)) * 2;
					total += (tmp % 9);
				}
			}

			total = total % 10;

			codes.Add(total.ToString()[0]);
			return value + total.ToString();
		}
	}
}
