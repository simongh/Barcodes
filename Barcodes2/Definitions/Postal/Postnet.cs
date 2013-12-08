using System.Collections.Generic;

namespace Barcodes2.Definitions.Postal
{
	public class Postnet : DefaultDefinition
	{
		private const int STARTSTOP = 11;

		public Postnet()
			: base()
		{
			IsChecksumRequired = true;
		}

		protected override System.Text.RegularExpressions.Regex GetRegex()
		{
			return new System.Text.RegularExpressions.Regex(@"^\d{5}((\s|-)?\d{4}((\s|-)?\d{2})?)?$");
		}

		protected override void CreatePatternSet()
		{
			PatternSet = new Dictionary<int, Pattern>();

			PatternSet.Add(0, Pattern.Parse("aattt"));
			PatternSet.Add(1, Pattern.Parse("tttaa"));
			PatternSet.Add(2, Pattern.Parse("ttata"));
			PatternSet.Add(3, Pattern.Parse("ttaat"));
			PatternSet.Add(4, Pattern.Parse("tatta"));
			PatternSet.Add(5, Pattern.Parse("tatat"));
			PatternSet.Add(6, Pattern.Parse("taatt"));
			PatternSet.Add(7, Pattern.Parse("attta"));
			PatternSet.Add(8, Pattern.Parse("attat"));
			PatternSet.Add(9, Pattern.Parse("atatt"));

			PatternSet.Add(STARTSTOP, Pattern.Parse("a"));
		}

		public override void TransformSettings(BarcodeSettings settings)
		{
			base.TransformSettings(settings);

			settings.ModulePadding = 0;
			settings.WideWidth = 2 * settings.NarrowWidth;
			settings.IsTextShown = false;
			settings.IsChecksumCalculated = true;

			settings.MediumHeight = (int)((settings.BarHeight / 3f) * 2f);
			settings.ShortHeight = settings.BarHeight / 3;
		}

		public override string AddChecksum(string value, CodedValueCollection codes)
		{
			int total = 0;
			for (int i = 1; i < codes.Count - 1; i++)
			{
				total += codes[i];
			}

			total = total % 10;

			codes.Insert(codes.Count - 1, total == 0 ? 0 : 10 - total);

			return value;
		}

		public override CodedValueCollection GetCodes(string value)
		{
			var codes = new CodedValueCollection();

			value = value.Replace(" ", "").Replace("-", "");

			for (int i = 0; i < value.Length; i++)
			{
				codes.Add(int.Parse(value.Substring(i, 1)));
			}

			codes.Insert(0, STARTSTOP);
			codes.Add(STARTSTOP);

			return codes;
		}

		public override int CalculateWidth(BarcodeSettings settings, CodedValueCollection codes)
		{
			//var width = (((settings.NarrowWidth + settings.WideWidth) * 5) * (codes.Count - 2)) + (2 * settings.NarrowWidth) + settings.WideWidth;

			return base.CalculateWidth(settings, codes);
		}
	}
}
