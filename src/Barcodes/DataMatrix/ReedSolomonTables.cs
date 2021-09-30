using System;

namespace Barcodes.DataMatrix
{
	public class ReedSolomonTables : Barcodes.ReedSolomonTables
	{
		private int _codeWords;

		private static readonly Lazy<ReedSolomonTables> _instance = new Lazy<ReedSolomonTables>(() => new ReedSolomonTables(5));

		public static ReedSolomonTables Instance => _instance.Value;

		protected override int G => 0x12D;

		public ReedSolomonTables(int codeWords)
			: base()
		{
			_codeWords = codeWords;
		}

		public override int CodeWordCount(int length)
		{
			return _codeWords;
		}
	}
}