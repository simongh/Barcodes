using System.Text.RegularExpressions;

namespace Barcodes.Code93
{
	public class Definition : IDefinition
	{
		private const int SHIFT1 = 43;
		private const int SHIFT2 = 44;
		private const int SHIFT3 = 45;
		private const int SHIFT4 = 46;
		private const int LIMIT = 47;
		private const int TERMINATOR = 48;

		private static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			Pattern.Parse(0,  "100010100"),
			Pattern.Parse(1,  "101001000"),
			Pattern.Parse(2,  "101000100"),
			Pattern.Parse(3,  "101000010"),
			Pattern.Parse(4,  "100101000"),
			Pattern.Parse(5,  "100100100"),
			Pattern.Parse(6,  "100100010"),
			Pattern.Parse(7,  "101010000"),
			Pattern.Parse(8,  "100010010"),
			Pattern.Parse(9,  "100001010"),
			Pattern.Parse(10, "110101000"),
			Pattern.Parse(11, "110100100"),
			Pattern.Parse(12, "110100010"),
			Pattern.Parse(13, "110010100"),
			Pattern.Parse(14, "110010010"),
			Pattern.Parse(15, "110001010"),
			Pattern.Parse(16, "101101000"),
			Pattern.Parse(17, "101100100"),
			Pattern.Parse(18, "101100010"),
			Pattern.Parse(19, "100110100"),
			Pattern.Parse(20, "100110010"),
			Pattern.Parse(21, "101011000"),
			Pattern.Parse(22, "101001100"),
			Pattern.Parse(23, "101000110"),
			Pattern.Parse(24, "100101100"),
			Pattern.Parse(25, "100010110"),
			Pattern.Parse(26, "110110100"),
			Pattern.Parse(27, "110110010"),
			Pattern.Parse(28, "110101100"),
			Pattern.Parse(29, "110100110"),
			Pattern.Parse(30, "110010110"),
			Pattern.Parse(31, "110011010"),
			Pattern.Parse(32, "101101100"),
			Pattern.Parse(33, "101100110"),
			Pattern.Parse(34, "100110110"),
			Pattern.Parse(35, "100111010"),
			Pattern.Parse(36, "100101110"),
			Pattern.Parse(37, "111010100"),
			Pattern.Parse(38, "111010010"),
			Pattern.Parse(39, "111001010"),
			Pattern.Parse(40, "101101110"),
			Pattern.Parse(41, "101110110"),
			Pattern.Parse(42, "110101110"),

			Pattern.Parse(SHIFT1, "100100110"),
			Pattern.Parse(SHIFT2, "111011010"),
			Pattern.Parse(SHIFT3, "111010110"),
			Pattern.Parse(SHIFT4, "100110010"),
			Pattern.Parse(LIMIT,  "101011110"),
			Pattern.Parse(TERMINATOR, "1"),
		});

		public PatternSet PatternSet => _patternSet;

		public string GetDisplayText(string value) => value;

		public bool ValidateInput(string value)
		{
			return Regex.IsMatch(value, ".+");
		}
	}
}