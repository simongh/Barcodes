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
	}
}