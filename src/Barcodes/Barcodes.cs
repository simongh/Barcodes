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
			var definition = new Codabar.Definition();

			var data = Encode(definition, value);
			if (data == null)
				return;

			if (addChecksum)
				definition.AddChecksum(data);
		}

		public static void Code11(string value)
		{
			var data = Encode(new Code11.Definition(), value);
		}

		public static void Code2of5(string value)
		{
			var data = Encode(new Code2of5.Definition(), value);
		}

		public static void Code3of9(string value)
		{
			var data = Encode(new Code3of9.Definition(), value);
		}

		public static void Code93(string value)
		{
			var data = Encode(new Code93.Definition(), value);
		}
	}
}