using System;
using System.Collections.Generic;
using System.IO;

namespace Barcodes
{
	public class Code128Builder : List<char>
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

		public const char AiMarker = (char)156;

		public void Add(string value)
		{
			AddRange(value);
		}

		public override string ToString()
		{
			var variant = CODEB;
			var shifted = false;
			int codeCBuffer = -1;

			if (this[0] == StartVariantA)
				variant = CODEA;
			else if (this[0] == StartVariantC)
				variant = CODEC;

			using (var result = new StringWriter())
			{
				foreach (var item in this)
				{
					if (item == AiMarker)
						continue;

					if (shifted)
					{
						if (variant == CODEA)
							variant = CODEB;
						else
							variant = CODEA;
					}

					if (variant == CODEA)
					{
						result.Write(EncodeCodeA(item));

						if (item == CODEB || item == CODEC)
							variant = item;
					}
					else if (variant == CODEB)
					{
						result.Write(EncodeCodeB(item));

						if (item == CODEA || item == CODEC)
							variant = item;
					}
					else
					{
						if (item == CODEA || item == CODEB)
						{
							result.Write((char)(item - 50));
							variant = item;
						}
						else if (item == FNC1)
						{
							result.Write((char)(item - 50));
						}
						else
						{
							if (!char.IsDigit(item))
								throw new ArgumentException($"Unexpected Code C character {(int)item:x} encountered");

							if (codeCBuffer < 0)
							{
								codeCBuffer = (item - '0') * 10;
								continue;
							}
							else
							{
								result.Write((item - '0') + codeCBuffer);
								codeCBuffer = -1;
							}
						}
					}

					if (shifted)
					{
						if (variant == CODEA)
							variant = CODEB;
						else
							variant = CODEA;
					}

					shifted = (item == 98 && variant != CODEC);
				}

				if (codeCBuffer >= 0)
					throw new ArgumentException("Encoding of code C value was attempted, but the value was not specifed using 2 characters");

				return result.ToString();
			}
		}

		private char EncodeCodeA(char value)
		{
			int offset;
			if (value > 31 && value < 96)
				offset = -32;
			else if (value >= 0 && value < 32)
				offset = 64;
			else if (value >= FNC3 && value <= FNC1)
				offset = -50;
			else
				throw new ArgumentException($"Unssupported Code A character {(int)value:x} encountered");

			return (char)(value + offset);
		}

		private char EncodeCodeB(char value)
		{
			int offset;
			if (value > 31 && value < 127)
				offset = -32;
			else if (value == 127)
				offset = -32;
			else if (value >= FNC3 && value <= FNC1)
				offset = -50;
			else
				throw new ArgumentException($"Unssupported Code B character {(int)value:x} encountered");

			return (char)(value + offset);
		}
	}
}