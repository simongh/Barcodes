using System.IO;

namespace Barcodes2.DataMatrix
{
	public static class MemoryStreamExtensions
	{
		public static void WriteInt(this MemoryStream stream, int value)
		{
			stream.WriteByte((byte)value);
		}
	}
}
