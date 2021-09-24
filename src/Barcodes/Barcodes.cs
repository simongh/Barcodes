namespace Barcodes
{
	public static class Barcode
	{
		public static void Code3of9(string value)
		{
			var data = new Parser(new Code3of9.Definition()).Parse(value);
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

		public static void Code11(string value)
		{
			var data = new Parser(new Code11.Definition()).Parse(value);
		}
	}
}