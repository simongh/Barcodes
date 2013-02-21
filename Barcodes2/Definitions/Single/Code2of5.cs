using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes2.Definitions.Single
{
	public class Code2of5 : DefaultDefinition
	{
		private const int START = 10;
		private const int STOP = 11;

		protected override Regex GetRegex()
		{
			return new System.Text.RegularExpressions.Regex("^\\d+");
		}

		protected override void CreatePatternSet()
		{
			PatternSet = new Dictionary<int, Pattern>();

			PatternSet.Add(0, Pattern.Parse("nb nw nb nw wb nw wb nw nb"));
			PatternSet.Add(1, Pattern.Parse("wb nw nb nw nb nw nb nw wb"));
			PatternSet.Add(2, Pattern.Parse("nb nw wb nw nb nw nb nw wb"));
			PatternSet.Add(3, Pattern.Parse("wb nw wb nw nb nw nb nw nb"));
			PatternSet.Add(4, Pattern.Parse("nb nw nb nw wb nw nb nw wb"));
			PatternSet.Add(5, Pattern.Parse("wb nw nb nw wb nw nb nw nb"));
			PatternSet.Add(6, Pattern.Parse("nb nw wb nw wb nw nb nw nb"));
			PatternSet.Add(7, Pattern.Parse("nb nw nb nw nb nw wb nw wb"));
			PatternSet.Add(8, Pattern.Parse("wb nw nb nw nb nw wb nw nb"));
			PatternSet.Add(9, Pattern.Parse("nb nw wb nw nb nw wb nw nb"));

			PatternSet.Add(START, Pattern.Parse("wb nw wb nw nb"));
			PatternSet.Add(STOP, Pattern.Parse("wb nw nb nw wb"));
		}

		public override CodedValueCollection GetCodes(string value)
		{
			var result = new CodedValueCollection();

			for (int i = 0; i < value.Length; i++)
			{
				result.Add(int.Parse(value.Substring(i, 1)));
			}

			result.Insert(0, START);
			result.Add(STOP);

			return result;
		}

		public override int CalculateWidth(BarcodeSettings settings, CodedValueCollection codes)
		{
			var width = (codes.Count - 2) * ((7 * settings.NarrowWidth) + (2 * settings.WideWidth) + settings.ModulePadding);
			return width + (4 * settings.WideWidth) + (6 * settings.NarrowWidth);
		}

		public override string AddChecksum(string value, CodedValueCollection codes)
		{
			int total = 0;
			bool isEven = true;

			for (int i = codes.Count - 2; i < 0; i--)
			{
				total += isEven ? 3 * codes[i] : codes[i];
			}

			total = total % 10;
			total = total == 0 ? 0 : 10 - total;
			codes.Insert(codes.Count - 1, total);
			
			return value + total.ToString();
		}
	}
}
