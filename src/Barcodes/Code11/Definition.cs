﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Barcodes.Code11
{
	public class Definition : IDefinition, IChecksum, ILimits
	{
		private static readonly IEnumerable<Pattern> _patternSet = new List<Pattern>
		{
			Pattern.Parse('0', "nb nw nb nw wb"),
			Pattern.Parse('1', "wb nw nb nw wb"),
			Pattern.Parse('2', "nb ww nb nw wb"),
			Pattern.Parse('3', "wb ww nb nw nb"),
			Pattern.Parse('4', "nb nw wb nw wb"),
			Pattern.Parse('5', "wb nw wb nw nb"),
			Pattern.Parse('6', "nb ww wb nw nb"),
			Pattern.Parse('7', "nb nw nb ww wb"),
			Pattern.Parse('8', "wb nw nb ww nb"),
			Pattern.Parse('9', "wb nw nb nw nb"),
			Pattern.Parse('-', "nb nw wb nw nb"),

			Pattern.Parse('s', "nb nw wb ww nb")
		};

		public IEnumerable<Pattern> PatternSet => _patternSet;

		public bool IsChecksumRequired => true;

		public void AddChecksum(EncodedData data)
		{
			if (data.IsChecksumed)
				return;

			AddCheckDigit(data, 10);

			if (data.DisplayText.Length >= 10)
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

			for (int i = 0; i < data.DisplayText.Length; i++)
			{
				int weight = ((data.DisplayText.Length - i) % factor);
				if (weight == 0)
					weight = factor;

				tmp += (data.DisplayText[i] == '-' ? 10 : (data.DisplayText[i] - '0') * weight);
			}

			var chk = (tmp % 11) > 9 ? '-' : tmp;

			data.DisplayText += chk.ToString();
			data.Codes.Add(PatternSet.First(p => p.Value == tmp));

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
			var limit = PatternSet.First(p => p.Value == 's');

			data.Codes.Insert(0, limit);
			data.Codes.Add(limit);
		}
	}
}