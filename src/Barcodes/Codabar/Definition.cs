using BarcodeReader.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BarcodeReader.Codabar
{
	public class Definition : IDefinition, Types.IChecksum
	{
		public IEnumerable<Pattern> PatternSet
		{
			get
			{
				yield return Pattern.Parse('0', "2323210");
				yield return Pattern.Parse('1', "2323012");
				yield return Pattern.Parse('2', "2321230");
				yield return Pattern.Parse('3', "0123232");
				yield return Pattern.Parse('4', "2303212");
				yield return Pattern.Parse('5', "0323212");
				yield return Pattern.Parse('6', "2123230");
				yield return Pattern.Parse('7', "2123032");
				yield return Pattern.Parse('8', "2103232");
				yield return Pattern.Parse('9', "0321232");

				yield return Pattern.Parse('-', "2321032");
				yield return Pattern.Parse('$', "2301232");
				yield return Pattern.Parse(':', "0323030");
				yield return Pattern.Parse('/', "0303230");
				yield return Pattern.Parse('.', "0303032");
				yield return Pattern.Parse('+', "2303030");

				yield return Pattern.Parse('a', "2301212");
				yield return Pattern.Parse('b', "2121230");
				yield return Pattern.Parse('c', "2321210");
				yield return Pattern.Parse('d', "2321012");
				yield return Pattern.Parse('t', "2301212");
				yield return Pattern.Parse('n', "2121230");
				yield return Pattern.Parse('*', "2321210");
				yield return Pattern.Parse('e', "2321012");
			}
		}

		public bool IsChecksumRequired => false;

		public void AddChecksum(EncodedData data)
		{
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
		}

		public string GetDisplayText(string value)
		{
			value = value.ToLower();

			return value.Substring(1, value.Length - 2);
		}

		public bool ValidateInput(string value)
		{
			return Regex.IsMatch(value, @"^[atbnc\*de][\d-$:/\.\+]+[atbnc\*de]$");
		}
	}
}