using System;

namespace Barcodes.GS1
{
	public class GS1Value
	{
		public ApplicationIdentifier Identifier { get; private set; }

		public string Value { get; set; }

		public GS1Value(ApplicationIdentifier identifier)
		{
			Identifier = identifier;
		}

		public GS1Value(ApplicationIdentifier identifier, string value)
			: this(identifier)
		{
			Value = value;
		}

		public void SetValue(string value)
		{
			Value = value;
		}

		public void SetValue(int value)
		{
			Value = value.ToString();
		}

		public void SetValue(decimal value, int precision)
		{
			string tmp = (value * (decimal)Math.Pow(10, precision)).ToString();

			if (precision < 0 || precision > 6)
				throw new ArgumentOutOfRangeException("precision", "Decimal precision must be between 0 and 6.");

			Value = precision.ToString() + tmp;
		}

		public void SetValue(DateTime value, bool ignoreDay = false)
		{
			string tmp;
			if (ignoreDay)
				tmp = value.ToString("yyMM00");
			else
				tmp = value.ToString("yyMMdd");

			Value = tmp;
		}
	}
}