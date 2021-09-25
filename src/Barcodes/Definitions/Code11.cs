using System.Text.RegularExpressions;

namespace Barcodes.Definitions
{
	public class Code11 : IDefinition, IChecksum, ILimits
	{
		private static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			Pattern.Parse('0', "23230"),
			Pattern.Parse('1', "03230"),
			Pattern.Parse('2', "21230"),
			Pattern.Parse('3', "01232"),
			Pattern.Parse('4', "23030"),
			Pattern.Parse('5', "03032"),
			Pattern.Parse('6', "21032"),
			Pattern.Parse('7', "23210"),
			Pattern.Parse('8', "03212"),
			Pattern.Parse('9', "03232"),
			Pattern.Parse('-', "23032"),

			Pattern.Parse('s', "23012")
		});

		public PatternSet PatternSet => _patternSet;

		public bool IsChecksumRequired => true;

		public void AddChecksum(EncodedData data)
		{
			if (data.IsChecksumed)
				return;

			AddCheckDigit(data, 10);

			if (data.Codes.Count >= 10)
				AddCheckDigit(data, 9);
		}

		public void AddSingleCheckDigit(EncodedData data)
		{
			AddCheckDigit(data, 10);
		}

		public void AddDoubleCheckDigit(EncodedData data)
		{
			AddCheckDigit(data, 10);
			AddCheckDigit(data, 9);
		}

		private void AddCheckDigit(EncodedData data, int factor)
		{
			int tmp = 0;

			for (int i = 0; i < data.Codes.Count; i++)
			{
				int weight = ((data.Codes.Count - i) % factor);
				if (weight == 0)
					weight = factor;

				tmp += (data.Codes[i].Value == '-' ? 10 : (data.Codes[i].Value - '0') * weight);
			}

			var chk = (tmp % 11) > 9 ? '-' : tmp;

			data.DisplayText += chk.ToString();
			data.Codes.Add(PatternSet.Index(tmp));

			data.IsChecksumed = true;
		}

		public string GetDisplayText(string value)
		{
			return value;
		}

		public bool ValidateInput(string value)
		{
			if (value == null)
				return false;
			return Regex.IsMatch(value, @"^[0-9\- ]+$");
		}

		public void AddLimits(EncodedData data)
		{
			data.Bracket(PatternSet.Find('s'));
		}
	}
}