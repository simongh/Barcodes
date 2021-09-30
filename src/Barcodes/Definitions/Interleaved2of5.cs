using System.Collections.Generic;

namespace Barcodes.Definitions
{
	public class Interleaved2of5 : IDefinition, ILimits, IParser
	{
		private const int STARTMARKER = 101;
		private const int ENDMARKER = 102;

		private static readonly PatternSet _patternSet;

		static Interleaved2of5()
		{
			var widths = new[] { "22002", "02220", "20220", "00222", "22020", "02022", "20022", "22200", "02202", "20202" };

			var patterns = new List<Pattern>();
			var elements = new List<Element>();

			for (byte i = 0; i < 100; i++)
			{
				elements.Clear();

				for (byte j = 0; j < 5; j++)
				{
					elements.Add((Element)widths[i / 10][j] - '0');
					elements.Add((Element)widths[i % 10][j] - '0' + 1);
				}

				patterns.Add(new Pattern(i, elements));
			}

			patterns.Add(Pattern.Parse(STARTMARKER, "2323"));
			patterns.Add(Pattern.Parse(ENDMARKER, "030"));

			_patternSet = new PatternSet(patterns);
		}

		public PatternSet PatternSet => _patternSet;

		public void AddLimits(EncodedData data)
		{
			data.AddToStart(PatternSet.Find(STARTMARKER));
			data.AddToEnd(PatternSet.Find(ENDMARKER));
		}

		public string GetDisplayText(string value) => value;

		public IEnumerable<Pattern> Parse(byte[] value)
		{
			var result = new List<Pattern>();

			for (int i = 0; i < value.Length; i += 2)
			{
				var number = value[i] - '0' + value[i + 1] - '0';

				result.Add(PatternSet.Find(number));
			}

			return result;
		}

		public bool ValidateInput(string value)
		{
			var result = int.TryParse(value, out var _);
			result &= value.Length % 2 == 0;

			return result;
		}
	}
}