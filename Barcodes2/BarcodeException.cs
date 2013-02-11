using System;

namespace Barcodes2
{
	public class BarcodeException : Exception
	{
		public BarcodeException()
		{}

		public BarcodeException(string message)
			: base(message)
		{}

		public BarcodeException(string message, Exception innerException)
			: base(message, innerException)
		{}
	}
}
