using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Barcodes.Definitions
{
	public class Code93 : IDefinition, IChecksum, ILimits, IParser
	{
		private const byte SHIFT1 = 1;
		private const byte SHIFT2 = 2;
		private const byte SHIFT3 = 3;
		private const byte SHIFT4 = 4;
		private const byte LIMIT = 5;
		private const byte TERMINATOR = 6;

		private static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			Pattern.Parse('0',  "100010100"),
			Pattern.Parse('1',  "101001000"),
			Pattern.Parse('2',  "101000100"),
			Pattern.Parse('3',  "101000010"),
			Pattern.Parse('4',  "100101000"),
			Pattern.Parse('5',  "100100100"),
			Pattern.Parse('6',  "100100010"),
			Pattern.Parse('7',  "101010000"),
			Pattern.Parse('8',  "100010010"),
			Pattern.Parse('9',  "100001010"),
			Pattern.Parse('A', "110101000"),
			Pattern.Parse('B', "110100100"),
			Pattern.Parse('C', "110100010"),
			Pattern.Parse('D', "110010100"),
			Pattern.Parse('E', "110010010"),
			Pattern.Parse('F', "110001010"),
			Pattern.Parse('G', "101101000"),
			Pattern.Parse('H', "101100100"),
			Pattern.Parse('I', "101100010"),
			Pattern.Parse('J', "100110100"),
			Pattern.Parse('K', "100110010"),
			Pattern.Parse('L', "101011000"),
			Pattern.Parse('M', "101001100"),
			Pattern.Parse('N', "101000110"),
			Pattern.Parse('O', "100101100"),
			Pattern.Parse('P', "100010110"),
			Pattern.Parse('Q', "110110100"),
			Pattern.Parse('R', "110110010"),
			Pattern.Parse('S', "110101100"),
			Pattern.Parse('T', "110100110"),
			Pattern.Parse('U', "110010110"),
			Pattern.Parse('V', "110011010"),
			Pattern.Parse('W', "101101100"),
			Pattern.Parse('X', "101100110"),
			Pattern.Parse('Y', "100110110"),
			Pattern.Parse('Z', "100111010"),
			Pattern.Parse('-', "100101110"),
			Pattern.Parse('.', "111010100"),
			Pattern.Parse(' ', "111010010"),
			Pattern.Parse('$', "111001010"),
			Pattern.Parse('/', "101101110"),
			Pattern.Parse('+', "101110110"),
			Pattern.Parse('%', "110101110"),

			Pattern.Parse(SHIFT1, "100100110"),
			Pattern.Parse(SHIFT2, "111011010"),
			Pattern.Parse(SHIFT3, "111010110"),
			Pattern.Parse(SHIFT4, "100110010"),
			Pattern.Parse(LIMIT,  "101011110", true),
			Pattern.Parse(TERMINATOR, "1"),
		});

		public PatternSet PatternSet => _patternSet;

		public bool IsChecksumRequired => true;

		public void AddChecksum(EncodedData data)
		{
			if (data.IsChecksumed)
				return;

			AddChecksum(data, 20);
			AddChecksum(data, 15);

			data.IsChecksumed = true;
		}

		private void AddChecksum(EncodedData data, int weight)
		{
			int total = 0, w = 1;

			for (int i = data.Codes.Count; i >= 0; i--)
			{
				total += w * data.Codes[i].Value;
				w++;
				if (w > weight)
					w = 1;
			}

			total %= 47;

			data.AddToEnd(PatternSet.Index(total));
		}

		public string GetDisplayText(string value) => value;

		public bool ValidateInput(string value)
		{
			return Regex.IsMatch(value, ".+");
		}

		public void AddLimits(EncodedData data)
		{
			data.Bracket(PatternSet.Find(LIMIT));
			data.AddToEnd(PatternSet.Find(TERMINATOR));
		}

		public IEnumerable<Pattern> Parse(byte[] value)
		{
			var result = new List<Pattern>();

			var transformer = new AsciiTransformer
			{
				Shift1 = (char)SHIFT1,
				Shift2 = (char)SHIFT2,
				Shift3 = (char)SHIFT3,
				Shift4 = (char)SHIFT4
			};

			foreach (var item in value)
			{
				result.AddRange(transformer
					.Transform((char)item)
					.Select(c => _patternSet.Find(c)));
			}

			return result;
		}
	}
}