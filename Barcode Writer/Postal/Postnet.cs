using System;
using System.Collections.Generic;

namespace Barcodes
{
	/// <summary>
	/// PostNet barcode used by the US post office
	/// </summary>
	public class PostNet : BarcodeBase
	{
		private const int STARTSTOP = 11;


		protected override void Init()
		{
			DefaultSettings.ModulePadding = 0;
			DefaultSettings.WideWidth = 2 * DefaultSettings.NarrowWidth;
			DefaultSettings.IsTextShown = false;
			DefaultSettings.IsChecksumCalculated = true;

			AllowedCharsPattern = new System.Text.RegularExpressions.Regex(@"^\d{5}((\s|-)?\d{4}((\s|-)?\d{2})?)?$");

			AddChecksum += new EventHandler<AddChecksumEventArgs>(Postnet_AddChecksum);
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

		void Postnet_AddChecksum(object sender, AddChecksumEventArgs e)
		{
			int total = 0;
			for (int i = 1; i < e.Codes.Count - 1; i++)
			{
				total += e.Codes[i];
			}

			total = total % 10;

			e.Codes.Insert(e.Codes.Count - 1, total == 0 ? 0 : 10 - total);
		}

		protected override string ParseText(string value, CodedValueCollection codes)
		{
			value = base.ParseText(value, codes);

			value = value.Replace(" ", "").Replace("-", "");

			for (int i = 0; i < value.Length; i++)
			{
				codes.Add(int.Parse(value.Substring(i, 1)));
			}

			codes.Insert(0, STARTSTOP);
			codes.Add(STARTSTOP);

			return value;
		}

		protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
		{
			width += (((settings.NarrowWidth + settings.WideWidth) * 5) * (codes.Count - 2)) + (2 * settings.NarrowWidth) + settings.WideWidth;

			return base.OnCalculateWidth(width, settings, codes);
		}

		protected override int OnCalculateHeight(int height, BarcodeSettings settings, CodedValueCollection codes)
		{
			height -= (settings.BarHeight - settings.MediumHeight);
			return base.OnCalculateHeight(height, settings, codes);
		}
	}
}
