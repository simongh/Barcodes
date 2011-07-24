using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Barcodes.Web.Mvc
{
	/// <summary>
	/// Helper class to generate paths to the barcode image handler
	/// </summary>
	public static class BarcodeHelper
	{
		/// <summary>
		/// Draw a barcode. size is calculated automatically, margins set to 0
		/// </summary>
		/// <param name="helper"></param>
		/// <param name="data">barcode data</param>
		/// <param name="format">barcode format</param>
		public static void Barcode(this HtmlHelper helper, string data, BarcodeFormats format)
		{
			BarcodeHelper.Barcode(helper, data, format, null, null);
		}

		//public static void Barcode(this HtmlHelper helper, string data, BarcodeFormats format, string imageFormat)
		//{
		//    BarcodeHelper.Barcode(helper, data, format, imageFormat, null);
		//}

		public static void Barcode(this HtmlHelper helper, string data, BarcodeFormats format, string imageformat, object dimensions)
		{
			helper.ViewContext.Writer.Write(Config.Instance.Url);
			helper.ViewContext.Writer.Write("?{0}={1}", RequestSettings.DATAKEY, data);
			helper.ViewContext.Writer.Write("&{0}={1:d}", RequestSettings.BARCODEKEY, format);

			if (imageformat != null)
				helper.ViewContext.Writer.Write("&{0}={1}", RequestSettings.FORMATKEY, imageformat);

		}
	}
}
