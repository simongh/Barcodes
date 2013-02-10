using System;

namespace Barcodes
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
