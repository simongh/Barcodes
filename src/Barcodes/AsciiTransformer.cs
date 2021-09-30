using System;

namespace Barcodes
{
	public class AsciiTransformer
	{
		public char Shift1 { get; set; }

		public char Shift2 { get; set; }

		public char Shift3 { get; set; }

		public char Shift4 { get; set; }

		public char[] Transform(char value)
		{
			//unshifted
			if (value == ' ' || value == '$' || value == '%' || value == '+')
				return new[]
				{
					value
				};

			if (value >= '-' && value <= '9')
				return new[]
				{
					value
				};

			//shifted
			if (value == 0)
				return new[]
				{
					Shift2,
					'U'
				};

			if (value < 27)
				return new[]
				{
					Shift1,
					(char)(value - 1 + 'A'),
				};

			if (value < 32)
				return new[]
				{
					Shift2,
					(char)(value - 27 + 'A'),
				};

			if (value < 59)
				return new[]
				{
					Shift3,
					(char)(value - 33 + 'A'),
				};

			if (value < 64)
				return new[]
				{
					Shift2,
					(char)(value - 59 + 'F')
				};

			if (value == 64)
				return new[]
				{
					Shift2,
					'V'
				};

			if (value < 91)
				return new[]
				{
					value
				};

			if (value < 96)
				return new[]
				{
					Shift2,
					(char)(value - 91 + 'K'),
				};

			if (value == 96)
				return new[]
				{
					Shift2,
					'W'
				};

			if (value < 123)
				return new[]
				{
					Shift4,
					(char)(value - 97 + 'A'),
				};

			if (value < 127)
				return new[]
				{
					Shift2,
					(char)(value - 123 + 'P'),
				};

			throw new ArgumentException($"The value '{value}' is not valid for an ASCII barcode");
		}
	}
}