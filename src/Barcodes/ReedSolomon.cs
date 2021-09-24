using System.Collections.Generic;

namespace Barcodes
{
	public class ReedSolomon
	{
		private readonly ReedSolomonTables _tables;

		public ReedSolomon(ReedSolomonTables tables)
		{
			Guard.IsNotNull(tables, nameof(tables));

			_tables = tables;
		}

		public IEnumerable<byte> Encode(byte[] data)
		{
			var eccCount = _tables.CodeWordCount(data.Length);

			var factors = _tables.GetCoefficients(eccCount);
			var result = new byte[eccCount];
			var t = 0;

			for (int i = 0; i < data.Length; i++)
			{
				t = data[i] ^ result[result.Length - 1];

				for (int j = result.Length - 1; j > 0; j--)
				{
					result[j] = _tables.Calculate(t, factors[j]);
				}

				if ((t & factors[0]) != 0)
					result[0] = _tables.Calculate(t, factors[0]);
				else
					result[0] = 0;
			}

			return result;
		}
	}
}