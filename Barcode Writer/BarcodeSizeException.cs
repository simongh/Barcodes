using System;

namespace Barcodes
{
	public class BarcodeSizeException : BarcodeException
	{
		public BarcodeSizeException()
			: base()
		{ }

		public BarcodeSizeException(string message)
			: base(message)
		{ }

		public BarcodeSizeException(string message, Exception innerException)
			: base(message, innerException)
		{ }
	}
}
