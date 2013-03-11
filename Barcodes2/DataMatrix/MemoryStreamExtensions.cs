using System.IO;

namespace Barcodes2.DataMatrix
{
	internal static class MemoryStreamExtensions
	{
		public static void WriteInt(this MemoryStream stream, int value)
		{
			stream.WriteByte((byte)value);
		}
	}
}
