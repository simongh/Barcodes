using System;

namespace BarcodeReader
{
	public static class Guard
	{
		public static void IsNotNull<T>(T item, string parameter)
		{
			if (item == null)
				throw new ArgumentNullException(parameter);
		}

		public static void IsNotEmpty(string value, string parameter)
		{
			if (string.IsNullOrEmpty(value))
				throw new ArgumentNullException(parameter);
		}
	}
}