using System;

namespace Barcodes2
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
