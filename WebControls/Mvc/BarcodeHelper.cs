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
		public static string Barcode(this HtmlHelper helper, string data, BarcodeFormats format)
		{
			return BarcodeHelper.Barcode(helper, data, format, null);
		}

		//public static void Barcode(this HtmlHelper helper, string data, BarcodeFormats format, string imageFormat)
		//{
		//    BarcodeHelper.Barcode(helper, data, format, imageFormat, null);
		//}

		public static string Barcode(this HtmlHelper helper, string data, object options)
		{
			return Barcode(helper, data, new System.Web.Routing.RouteValueDictionary(options));
		}

		public static string Barcode(this HtmlHelper helper, string data, BarcodeFormats format, IDictionary<string, object> options)
		{
			return RequestSettings.UrlBuilder(data, format, options);
		}
	}
}
