using System;
using System.Linq;

namespace Barcodes
{
	public static class LuhnChecksum
	{
		public static byte Calculate(byte[] data) => Calculate(data, 10);

		public static char Calculate(string value)
		{
			return (char)(Calculate(value.Select(c =>
			{
				if (!char.IsDigit(c))
					throw new ArgumentException("Luhn Checksum is only supported for digits");

				return (byte)(c - '0');
			}).ToArray()) + '0');
		}

		public static byte Calculate(byte[] data, int range)
		{
			var total = 0;
			var parity = (data.Length - 1) % 2;

			for (int i = 0; i < data.Length; i++)
			{
				var value = data[i] * ((i % 2 == parity) ? 2 : 1);
				total += (value / range) + (value % range);
			}

			return (byte)(range - (total % range));
		}

		public static byte Calculate(string value, int range)
		{
			return Calculate(value.Select(c => (byte)c).ToArray(), range);
		}
	}
}