namespace Barcodes.Helpers
{
	/// <summary>
	/// Code 128 helper containing Code 128 constants
	/// </summary>
	public static class Code128Values
	{
		/// <summary>
		/// Start value for a type A code
		/// </summary>
		public const char StartVariantA = (char)153;

		/// <summary>
		/// Start value for a type B code
		/// </summary>
		public const char StartVariantB = (char)154;

		/// <summary>
		/// Start value for a type C code
		/// </summary>
		public const char StartVariantC = (char)155;

		/// <summary>
		/// Indicate following code in a Uniform Code Council code
		/// </summary>
		public const char FNC1 = (char)152;

		/// <summary>
		/// Indicate following code in a Uniform Code Council code
		/// </summary>
		public const char FNC2 = (char)147;

		/// <summary>
		/// Indicate following code in a Uniform Code Council code
		/// </summary>
		public const char FNC3 = (char)146;

		/// <summary>
		/// Indicate following code in a Uniform Code Council code
		/// </summary>
		public const char FNC4 = (char)150;

		/// <summary>
		/// Shift the code into variant A
		/// </summary>
		public const char CODEA = (char)151;

		/// <summary>
		/// Shift the code into variant B
		/// </summary>
		public const char CODEB = (char)150;

		/// <summary>
		/// Shift the code into variant C
		/// </summary>
		public const char CODEC = (char)149;

		/// <summary>
		/// Shifts the next value between A & B
		/// </summary>
		public const char SHIFT = (char)148;
	}
}