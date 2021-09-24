﻿using System.Text.RegularExpressions;

namespace Barcodes.Code2of5
{
	public class Definition : IDefinition, IChecksum, ILimits, IParser
	{
		private const int START = 10;
		private const int STOP = 11;

		private static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			Pattern.Parse(0, "nb nw nb nw wb nw wb nw nb"),
			Pattern.Parse(1, "wb nw nb nw nb nw nb nw wb"),
			Pattern.Parse(2, "nb nw wb nw nb nw nb nw wb"),
			Pattern.Parse(3, "wb nw wb nw nb nw nb nw nb"),
			Pattern.Parse(4, "nb nw nb nw wb nw nb nw wb"),
			Pattern.Parse(5, "wb nw wb nw nb nw nb nw nb"),
			Pattern.Parse(6, "nb nw wb nw wb nw nb nw nb"),
			Pattern.Parse(7, "nb nw nb nw nb nw wb nw wb"),
			Pattern.Parse(8, "wb nw nb nw nb nw wb nw nb"),
			Pattern.Parse(9, "nb nw wb nw nb nw wb nw nb"),

			Pattern.Parse(START,"nb nb nw nb nb nw nb"),
			Pattern.Parse(STOP,"nb nb nw nb nw nb nb")
		});

		public PatternSet PatternSet => _patternSet;

		public bool IsChecksumRequired => true;

		public void AddChecksum(EncodedData data)
		{
			var total = 0;
			var isEven = true;

			for (int i = data.Codes.Count; i >= 0; i--)
			{
				total += (isEven ? 3 : 1) * data.Codes[i].Value;
			}

			total = total % 10;
			total = total == 0 ? 0 : 10 - total;

			data.AddToEnd(PatternSet.Find(total));
		}

		public void AddLimits(EncodedData data)
		{
			data.AddToStart(PatternSet.Find(START));
			data.AddToEnd(PatternSet.Find(STOP));
		}

		public Pattern Convert(char value) => PatternSet.Find(value - '0');

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