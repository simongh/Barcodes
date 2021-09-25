using System.Text.RegularExpressions;

namespace Barcodes.Definitions
{
	public class Code3of9 : IDefinition
	{
		private static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			Pattern.Parse('0', "232103032"),
			Pattern.Parse('1', "032123230"),
			Pattern.Parse('2', "230123230"),
			Pattern.Parse('3', "030123232"),
			Pattern.Parse('4', "232103230"),
			Pattern.Parse('5', "032103232"),
			Pattern.Parse('6', "230103232"),
			Pattern.Parse('7', "232123030"),
			Pattern.Parse('8', "032123032"),
			Pattern.Parse('9', "230123032"),
			Pattern.Parse('A', "032321230"),
			Pattern.Parse('B', "230321230"),
			Pattern.Parse('C', "030321232"),
			Pattern.Parse('D', "232301230"),
			Pattern.Parse('E', "032301232"),
			Pattern.Parse('F', "230301232"),
			Pattern.Parse('G', "232321030"),
			Pattern.Parse('H', "032321032"),
			Pattern.Parse('I', "230321032"),
			Pattern.Parse('J', "232301032"),
			Pattern.Parse('K', "032323210"),
			Pattern.Parse('L', "230323210"),
			Pattern.Parse('M', "030323212"),
			Pattern.Parse('N', "232303210"),
			Pattern.Parse('O', "032303212"),
			Pattern.Parse('P', "230303212"),
			Pattern.Parse('Q', "232323010"),
			Pattern.Parse('R', "032323012"),
			Pattern.Parse('S', "230323012"),
			Pattern.Parse('T', "232303012"),
			Pattern.Parse('U', "012323230"),
			Pattern.Parse('V', "210323230"),
			Pattern.Parse('W', "010323232"),
			Pattern.Parse('X', "212303230"),
			Pattern.Parse('Y', "012303232"),
			Pattern.Parse('Z', "210303232"),
			Pattern.Parse('-', "212323030"),
			Pattern.Parse('.', "012323032"),
			Pattern.Parse(' ', "210323032"),
			Pattern.Parse('*', "212303032"),
			Pattern.Parse('$', "212121232"),
			Pattern.Parse('/', "212123212"),
			Pattern.Parse('+', "212321212"),
			Pattern.Parse('%', "232121212")
		});

		public PatternSet PatternSet => _patternSet;

		public bool ValidateInput(string value)
		{
			if (value == null)
				return false;
			return Regex.IsMatch(value, @"^[A-Z0-9-\. *\$/+%]+$");
		}

		public string GetDisplayText(string value)
		{
			value = value.Trim('*');

			return $"*{value}*";
		}
	}
}