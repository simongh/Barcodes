using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Barcodes.Codabar
{
	public class Definition : IDefinition, IChecksum
	{
		private static readonly IEnumerable<Pattern> _patternSet = new List<Pattern>
		{
			Pattern.Parse('0', "2323210"),
			Pattern.Parse('1', "2323012"),
			Pattern.Parse('2', "2321230"),
			Pattern.Parse('3', "0123232"),
			Pattern.Parse('4', "2303212"),
			Pattern.Parse('5', "0323212"),
			Pattern.Parse('6', "2123230"),
			Pattern.Parse('7', "2123032"),
			Pattern.Parse('8', "2103232"),
			Pattern.Parse('9', "0321232"),

			Pattern.Parse('-', "2321032"),
			Pattern.Parse('$', "2301232"),
			Pattern.Parse(':', "0323030"),
			Pattern.Parse('/', "0303230"),
			Pattern.Parse('.', "0303032"),
			Pattern.Parse('+', "2303030"),

			Pattern.Parse('a', "2301212"),
			Pattern.Parse('b', "2121230"),
			Pattern.Parse('c', "2321210"),
			Pattern.Parse('d', "2321012"),
			Pattern.Parse('t', "2301212"),
			Pattern.Parse('n', "2121230"),
			Pattern.Parse('*', "2321210"),
			Pattern.Parse('e', "2321012")
		};

		public IEnumerable<Pattern> PatternSet => _patternSet;

		public bool IsChecksumRequired => false;

		public void AddChecksum(EncodedData data)
		{
			if (data.IsChecksumed)
				return;

			if (!Regex.IsMatch(data.DisplayText, @"^\d+$"))
				throw new ArgumentException("Only numeric values can have a check digit");

			var total = 0;
			for (int i = 0; i < data.DisplayText.Length; i++)
			{
				if (i % 2 == 0)
					total += data.DisplayText[i] - 0x30;
				else
					total += ((data.DisplayText[i] - 0x30) * 2) % 9;
			}

			total %= 10;

			data.Codes.Add(PatternSet.First(p => p.Value == total));
			data.DisplayText += total.ToString();

			data.IsChecksumed = true;
		}

		public string GetDisplayText(string value)
		{
			value = value.ToLower();

			return value.Substring(1, value.Length - 2);
		}

		public bool ValidateInput(string value)
		{
			return Regex.IsMatch(value, @"^[abcdtne\*][\d-$:/\.\+]+[abcdtn\*e]$");
		}
	}
}