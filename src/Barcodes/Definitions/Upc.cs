using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes.Definitions
{
	/// <summary>
	/// Universal Product Code (UPC) 12 digit code
	/// Also known as UPC-A
	/// </summary>
	public class Upc : EanBase, IDefinition, IParser
	{
		private const int ParityIndex = 6;

		public override void AddLimits(EncodedData data)
		{
			data.Codes.Insert(ParityIndex, PatternSet.Find(Split));
			data.Bracket(PatternSet.Find(Limit));
		}

		public string GetDisplayText(string value) => value;

		public IEnumerable<Pattern> Parse(string value)
		{
			var result = new List<Pattern>
			{
				PatternSet.Find(0)
			};

			var parity = _parity[value[0] - '0'];

			for (int i = 1; i < value.Length; i++)
			{
				var offset = 0;

				if (i < ParityIndex && parity[i - 1])
					offset = 10;
				else if (i >= 6)
					offset = 20;

				result.Add(_patternSet.Find(value[i] - '0' + offset));
			}

			return result;
		}

		public bool ValidateInput(string value)
		{
			return Regex.IsMatch(value, @"^\d{11,12}$");
		}
	}
}