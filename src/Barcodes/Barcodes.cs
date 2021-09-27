namespace Barcodes
{
	public static class Barcode
	{
		private static EncodedData Encode(IDefinition definition, string value)
		{
			if (!definition.ValidateInput(value))
				return null;

			return new Parser(definition).Parse(value);
		}

		public static void Codabar(string value, bool addChecksum)
		{
			var definition = new Definitions.Codabar();

			var data = Encode(definition, value);
			if (data == null)
				return;

			if (addChecksum)
				definition.AddChecksum(data);
		}

		public static void Code11(string value)
		{
			var data = Encode(new Definitions.Code11(), value);
		}

		public static void Code128(string value)
		{
			var builder = new Code128Builder();
			builder.Add(value);

			var data = Encode(new Definitions.Code128(), builder.ToString());
		}

		public static void Code2of5(string value)
		{
			var data = Encode(new Definitions.Code2of5(), value);
		}

		public static void Code3of9(string value)
		{
			var data = Encode(new Definitions.Code3of9(), value);
		}

		public static void Code93(string value)
		{
			var data = Encode(new Definitions.Code93(), value);
		}

		public static void ExtendedCode(string value)
		{
			var data = Encode(new Definitions.ExtendedCode3of9(), value);
		}

		public static void Interleaved2of5(string value)
		{
			var data = Encode(new Definitions.Interleaved2of5(), value);
		}
	}
}