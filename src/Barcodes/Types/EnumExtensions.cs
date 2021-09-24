namespace BarcodeReader.Types
{
	public static class EnumExtensions
	{
		public static bool IsWhite(this Element element)
		{
			return element == Element.NarrowWhite || element == Element.WideWhite;
		}

		public static bool IsWide(this Element element)
		{
			return element == Element.WideWhite || element == Element.WideBlack;
		}
	}
}