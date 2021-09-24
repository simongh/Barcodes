namespace BarcodeReader.DataMatrix
{
	public class ReedSolomonTables : BarcodeReader.ReedSolomonTables
	{
		private int _codeWords;

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