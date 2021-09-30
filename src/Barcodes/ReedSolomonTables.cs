using System;
using System.Collections.Generic;

namespace Barcodes
{
	public abstract class ReedSolomonTables
	{
		protected const int SymbolSize = 0xff;
		private readonly byte[] _logTable;
		private readonly byte[] _antiLogTable;
		private readonly IDictionary<int, byte[]> _coefficients;

		protected abstract int G { get; }

		protected ReedSolomonTables()
		{
			_logTable = new byte[SymbolSize + 1];
			_antiLogTable = new byte[SymbolSize];
			_coefficients = new Dictionary<int, byte[]>();

			FillTables();
		}

		private void FillTables()
		{
			for (int p = 1, v = 0; v < SymbolSize; v++)
			{
				_antiLogTable[v] = (byte)p;
				_logTable[p] = (byte)v;
				p <<= 1;
				if (p > SymbolSize)
					p ^= G;
			}
		}

		public byte[] GetCoefficients(int length)
		{
			if (!_coefficients.ContainsKey(length))
			{
				var values = new byte[length + 1];

				values[0] = 1;
				for (int i = 1; i <= length; i++)
				{
					values[i] = 1;
					for (int k = i - 1; k > 0; k--)
					{
						if (values[k] != 0)
							values[k] = _antiLogTable[(_logTable[values[k]] + i) % SymbolSize];

						values[k] ^= values[k - 1];
					}

					values[0] = _antiLogTable[(_logTable[values[0]] + i) % SymbolSize];
				}

				var result = new byte[length];
				Array.Copy(values, result, length);
				_coefficients.Add(length, result);
			}

			return _coefficients[length];
		}

		public abstract int CodeWordCount(int length);

		public byte Calculate(int value1, int value2)
		{
			return _antiLogTable[(_logTable[value1] + _logTable[value2]) % SymbolSize];
		}
	}
}