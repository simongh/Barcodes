namespace BarcodeReader
{
	public static class Barcodes
	{
		public static void Code3of9(string value)
		{
			var parser = new Parser(new Code3of9.Definition());
			var data = parser.Parse(value);
			if (data == null)
				return;
		}

		public static void Codabar(string value, bool addChecksum)
		{
			var definition = new Codabar.Definition();
			var parser = new Parser(definition);

			var data = parser.Parse(value);
			if (data == null)
				return;

			if (addChecksum)
				definition.AddChecksum(data);
		}
	}
}