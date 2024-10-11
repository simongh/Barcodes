using System;

namespace Barcodes.Writer
{
    public class BarcodeException : Exception
    {
        public BarcodeException()
        {
        }

        public BarcodeException(string message) : base(message)
        {
        }

        public BarcodeException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}