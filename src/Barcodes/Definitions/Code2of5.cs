using System.Text.RegularExpressions;

namespace Barcodes.Definitions
{
	public class Code2of5 : IDefinition, IChecksum, ILimits, IConvert
	{
		private const byte START = 10;
		private const byte STOP = 11;

		private static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			Pattern.Parse(0, "232303032"),
			Pattern.Parse(1, "032323230"),
			Pattern.Parse(2, "230323230"),
			Pattern.Parse(3, "030323232"),
			Pattern.Parse(4, "232303230"),
			Pattern.Parse(5, "030323232"),
			Pattern.Parse(6, "230303232"),
			Pattern.Parse(7, "232323030"),
			Pattern.Parse(8, "032323032"),
			Pattern.Parse(9, "230323032"),

			Pattern.Parse(START,"2232232"),
			Pattern.Parse(STOP,"2232322")
		});

		public PatternSet PatternSet => _patternSet;

		public bool IsChecksumRequired => true;

		public void AddChecksum(EncodedData data)
		{
			if (data.IsChecksumed)
				return;

			var total = 0;
			var isEven = true;

			for (int i = data.Codes.Count; i >= 0; i--)
			{
				total += (isEven ? 3 : 1) * data.Codes[i].Value;
			}

			total %= 10;
			total = total == 0 ? 0 : 10 - total;

			data.AddToEnd(PatternSet.Index(total));

			data.IsChecksumed = true;
		}

		public void AddLimits(EncodedData data)
		{
			data.AddToStart(PatternSet.Index(START));
			data.AddToEnd(PatternSet.Index(STOP));
		}

		public Pattern Convert(char value) => PatternSet.Index(value);

		public string GetDisplayText(string value)
		{
			return value;
		}

		public bool ValidateInput(string value)
		{
			if (value == null)
				return false;
			return Regex.IsMatch(value, @"^\d+$");
		}
	}
}