using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes2.Definitions.Postal
{
	public class IntelligentMail : DefaultDefinition
	{
		protected override System.Text.RegularExpressions.Regex GetRegex()
		{
			throw new NotImplementedException();
		}

		protected override void CreatePatternSet()
		{
			PatternSet = new Dictionary<int, Pattern>();

			PatternSet.Add(IntelligentMailHelper.ASCENDER, Pattern.Parse("a"));
			PatternSet.Add(IntelligentMailHelper.DESCENDER, Pattern.Parse("d"));
			PatternSet.Add(IntelligentMailHelper.FULLBAR, Pattern.Parse("f"));
			PatternSet.Add(IntelligentMailHelper.TRACKER, Pattern.Parse("t"));
		}

		public override bool IsDataValid(string value)
		{
			value = Regex.Replace(value, "[-\\s]", "");

			if (Regex.IsMatch(value, @"^\d[0-4]\d{3}"))
				return true;

			if (Regex.IsMatch(value, @"^\d{5}([0-8]\d{5}\d{9}|9\d{8}\d{6})"))
				return true;

			return Regex.IsMatch(value, @"^\d{20}(\d{5}(\d{4}(\d{2})?)?)?$");
		}

		public override void TransformSettings(BarcodeSettings settings)
		{
			base.TransformSettings(settings);

			settings.IsTextShown = false;
			settings.IsChecksumCalculated = false;
			settings.ModulePadding = settings.NarrowWidth;
			settings.WideWidth = 0;
			settings.ShortHeight = settings.BarHeight / 3;
			settings.MediumHeight = (settings.BarHeight / 3) * 2;
		}

		public override CodedValueCollection GetCodes(string value)
		{
			var codes = new CodedValueCollection();

			value = Regex.Replace(value, "[-\\s]", "");
			byte[] data1 = IntelligentMailHelper.Instance.ConvertToBytes(IntelligentMailHelper.Instance.ConvertRoutingCode(value.Substring(20)), value.Substring(0, 20));
			int fcs = IntelligentMailHelper.Instance.CRC11(data1);

			short[] data2 = IntelligentMailHelper.Instance.ConvertToCodewords(data1);

			if (IntelligentMailHelper.Instance.CheckFcs(fcs))
				data2[0] += 659;

			IntelligentMailHelper.Instance.ConvertToCharacters(data2);

			IntelligentMailHelper.Instance.IncludeFcs(data2, fcs);

			codes.AddRange(IntelligentMailHelper.Instance.ConvertToBars(data2));

			return codes;
		}
	}
}
