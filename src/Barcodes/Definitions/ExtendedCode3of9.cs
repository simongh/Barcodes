using System.Collections.Generic;
using System.Linq;

namespace Barcodes.Definitions
{
	public class ExtendedCode3of9 : Code3of9, IParser
	{
		public IEnumerable<Pattern> Parse(byte[] value)
		{
			var result = new List<Pattern>();

			var transformer = new AsciiTransformer
			{
				Shift1 = '$',
				Shift2 = '%',
				Shift3 = '/',
				Shift4 = '+'
			};

			foreach (var item in value)
			{
				result.AddRange(transformer
					.Transform((char)item)
					.Select(c => PatternSet.Find(c)));
			}

			return result;
		}

		public override bool ValidateInput(string value)
		{
			return true;
		}
	}
}