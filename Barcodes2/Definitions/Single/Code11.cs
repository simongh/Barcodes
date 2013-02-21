using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Barcodes2.Definitions.Single
{
	public class Code11 : DefaultDefinition
	{
		private const int LIMIT = 's';

		public Code11()
		{
			//IsChecksumRequired = true;
		}

		protected override Regex GetRegex()
		{
			return new Regex("^[\\d-]+$");
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

		public override CodedValueCollection GetCodes(string value)
		{
			value = string.Format("{1}{0}{1}", value, (char)LIMIT);

			return base.GetCodes(value);
		}

		public override string AddChecksum(string value, CodedValueCollection codes)
		{
			codes.RemoveAt(codes.Count - 1);

			value = DoChecksumCalculation(value, 10, codes);

			if (value.Length >= 10)
				value = DoChecksumCalculation(value, 9,codes);

			codes.Add(LIMIT);

			return value;
		}

		protected static string DoChecksumCalculation(string value, int factor, CodedValueCollection codes)
		{
			int tmp = 0;
			int weight = 0;
			for (int i = 0; i < value.Length; i++)
			{
				weight = ((value.Length - i) % factor);
				if (weight == 0)
					weight = factor;
				tmp += ((value[i] == '-' ? 10 : int.Parse(value.Substring(i, 1))) * weight);
			}

			tmp = tmp % 11;
			value += tmp > 9 ? "-" : tmp.ToString();
			if (codes != null)
			{
				if (codes.Last() == LIMIT)
					codes.Insert(codes.Count - 1, tmp > 9 ? '-' : tmp + '0');
				else
					codes.Add(tmp > 9 ? '-' : tmp + '0');
			}

			return value;
		}

		public static string AddSingleCheckDigit(string value)
		{
			return DoChecksumCalculation(value, 10, null);
		}

		public static string AddDoubleCheckDigit(string value)
		{
			var tmp = DoChecksumCalculation(value, 10, null);
			return DoChecksumCalculation(tmp, 9, null);
		}
	}
}
