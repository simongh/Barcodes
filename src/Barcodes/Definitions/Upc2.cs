using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes.Definitions
{
	public class Upc2 : EanBase, IDefinition, IParser
	{
		public override void AddLimits(EncodedData data)
		{
			data.AddToStart(PatternSet.Find(LimitStart));
			data.AddToEnd(PatternSet.Find(LimitEnd));
		}

		public IEnumerable<Pattern> Parse(byte[] value)
		{
			var m = ((value[0] * 10) + value[1]) % 4;
			var result = new List<Pattern>
			{
				PatternSet.Find(value[0] + ( m > 1 ? 10 : 0)),
				PatternSet.Find(value[1] + (( m == 1 || m == 3) ? 10 : 0)),
			};

			return result;
		}

		public bool ValidateInput(string value) => Regex.IsMatch(value, @"^\d{2}$");
	}
}