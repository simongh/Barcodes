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

		// EAN/UPC
		public static void EAN13(string value)
		{
			var data = Encode(new Definitions.Ean13(), value);
		}

		public static void EAN8(string value)
		{
			var data = Encode(new Definitions.Ean8(), value);
		}

		public static void UPC(string value)
		{
			var data = Encode(new Definitions.Upc(), value);
		}

		public static void UPC2(string value)
		{
			var data = Encode(new Definitions.Upc2(), value);
		}

		// mail
		public static void IntelligentMail(string value)
		{
			var definition = new Definitions.IntelligentMail();

			if (!definition.ValidateInput(value))
				return;

			var builder = new IntelligentMailBuilder();
			var data = new Parser(definition).Parse(builder.Build(value), value);
		}
	}
}