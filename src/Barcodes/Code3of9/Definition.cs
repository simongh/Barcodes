using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BarcodeReader.Code3of9
{
	public class Definition : IDefinition
	{
		public IEnumerable<Types.Pattern> PatternSet
		{
			get
			{
				yield return Types.Pattern.Parse('0', "232103032");
				yield return Types.Pattern.Parse('1', "032123230");
				yield return Types.Pattern.Parse('2', "230123230");
				yield return Types.Pattern.Parse('3', "030123232");
				yield return Types.Pattern.Parse('4', "232103230");
				yield return Types.Pattern.Parse('5', "032103232");
				yield return Types.Pattern.Parse('6', "230103232");
				yield return Types.Pattern.Parse('7', "232123030");
				yield return Types.Pattern.Parse('8', "032123032");
				yield return Types.Pattern.Parse('9', "230123032");
				yield return Types.Pattern.Parse('A', "032321230");
				yield return Types.Pattern.Parse('B', "230321230");
				yield return Types.Pattern.Parse('C', "030321232");
				yield return Types.Pattern.Parse('D', "232301230");
				yield return Types.Pattern.Parse('E', "032301232");
				yield return Types.Pattern.Parse('F', "230301232");
				yield return Types.Pattern.Parse('G', "232321030");
				yield return Types.Pattern.Parse('H', "032321032");
				yield return Types.Pattern.Parse('I', "230321032");
				yield return Types.Pattern.Parse('J', "232301032");
				yield return Types.Pattern.Parse('K', "032323210");
				yield return Types.Pattern.Parse('L', "230323210");
				yield return Types.Pattern.Parse('M', "030323212");
				yield return Types.Pattern.Parse('N', "232303210");
				yield return Types.Pattern.Parse('O', "032303212");
				yield return Types.Pattern.Parse('P', "230303212");
				yield return Types.Pattern.Parse('Q', "232323010");
				yield return Types.Pattern.Parse('R', "032323012");
				yield return Types.Pattern.Parse('S', "230323012");
				yield return Types.Pattern.Parse('T', "232303012");
				yield return Types.Pattern.Parse('U', "012323230");
				yield return Types.Pattern.Parse('V', "210323230");
				yield return Types.Pattern.Parse('W', "010323232");
				yield return Types.Pattern.Parse('X', "212303230");
				yield return Types.Pattern.Parse('Y', "012303232");
				yield return Types.Pattern.Parse('Z', "210303232");
				yield return Types.Pattern.Parse('-', "212323030");
				yield return Types.Pattern.Parse('.', "012323032");
				yield return Types.Pattern.Parse(' ', "210323032");
				yield return Types.Pattern.Parse('*', "212303032");
				yield return Types.Pattern.Parse('$', "212121232");
				yield return Types.Pattern.Parse('/', "212123212");
				yield return Types.Pattern.Parse('+', "212321212");
				yield return Types.Pattern.Parse('%', "232121212");
			}
		}

		public bool ValidateInput(string value)
		{
			if (value == null)
				return false;
			return Regex.IsMatch(value, @"^[A-Z0-9-\. *\$/+%]+$");
		}

		public string GetDisplayText(string value)
		{
			value = value.Trim('*');

			return "*" + value + "*";
		}
	}
}