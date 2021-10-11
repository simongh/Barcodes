using System;
using System.Text.RegularExpressions;

namespace Barcodes.Definitions
{
	public class IntelligentMail : IDefinition
	{
		private static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			new Pattern(IntelligentMailBuilder.ASCENDER, new []{ Element.Ascender }),
			new Pattern(IntelligentMailBuilder.DESCENDER, new []{ Element.Descender }),
			new Pattern(IntelligentMailBuilder.FULLBAR, new []{ Element.NarrowBlack }),
			new Pattern(IntelligentMailBuilder.TRACKER, new []{ Element.Tracker }),
		});

		public PatternSet PatternSet => _patternSet;

		public bool ValidateInput(string value)
		{
			value = Regex.Replace(value, "[-\\s]", "");

			if (!Regex.IsMatch(value, @"^\d[0-4]\d{3}"))
				throw new ApplicationException("The barcode identifier or service type was invalid.");

			if (!Regex.IsMatch(value, @"^\d{5}([0-8]\d{5}\d{9}|9\d{8}\d{6})"))
				throw new ApplicationException("The customer identifer or sequence number were invalid.");

			if (!Regex.IsMatch(value, @"^\d{20}(\d{5}(\d{4}(\d{2})?)?)?$"))
				throw new ApplicationException("The delivery point ZIP code was invalid.");

			return true;
		}
	}
}