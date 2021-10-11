using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes.Definitions
{
	/// <summary>
	/// European Article Number (EAN) 8 digit code
	/// </summary>
	public class Ean8 : EanBase, IDefinition, IParser
	{
		private const int ParityIndex = 4;

		public override void AddLimits(EncodedData data)
		{
			data.Codes.Insert(ParityIndex, PatternSet.Find(Split));
			data.Bracket(PatternSet.Find(Limit));
		}

		public IEnumerable<Pattern> Parse(byte[] value)
		{
			var result = new List<Pattern>();

			for (int i = 0; i < value.Length; i++)
			{
				var offset = 0;

				if (i >= ParityIndex)
					offset = 20;

				result.Add(_patternSet.Find(value[i] - '0' + offset));
			}

			return result;
		}

		public bool ValidateInput(string value)
		{
			return Regex.IsMatch(value, @"^\d{7,8}");
		}
	}
}