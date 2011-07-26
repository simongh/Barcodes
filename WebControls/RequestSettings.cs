using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcodes.Web
{
	/// <summary>
	/// Parses and validates request parameters
	/// </summary>
	public class RequestSettings
	{
		public const string MARGINKEY = "m";
		public const string SCALEKEY = "sc";
		public const string SIZEKEY = "sz";
		public const string DATAKEY = "data";
		public const string FORMATKEY = "f";
		public const string BARCODEKEY = "bc";

		/// <summary>
		/// Gets or sets the image size. 0 dimensions means the size is generated automatically
		/// </summary>
		public Size Size
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the left margin pixels
		/// </summary>
		public int LeftMargin
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the top margin pixels
		/// </summary>
		public int TopMargin
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the right margin pixels
		/// </summary>
		public int RightMargin
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the bottom margin pixels
		/// </summary>
		public int BottomMargin
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the scale factor
		/// </summary>
		public float Scale
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the image format
		/// </summary>
		public System.Drawing.Imaging.ImageFormat Format
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the MIME type for the current image format
		/// </summary>
		public string ContentType
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the barcode data
		/// </summary>
		public string Data
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the barcode format
		/// </summary>
		public BarcodeFormats BarcodeFormat
		{
			get;
			private set;
		}

		/// <summary>
		/// Parses the collection of values for parameters
		/// </summary>
		/// <param name="values">collection of parameters</param>
		public void Parse(System.Collections.Specialized.NameValueCollection values)
		{
			int c = 2;
			if (values[DATAKEY] == null)
				return;

			if (values[BARCODEKEY] == null)
				return;

			if (values[FORMATKEY] != null)
				c++;

			ParseFormat(values[FORMATKEY]);
			ParseType(values[BARCODEKEY]);
			Data = values[DATAKEY];

			Size = ParseSize(values[SIZEKEY]);
			Scale = ParseScale(values[SCALEKEY]);
			ParseMargins(values[MARGINKEY]);
		}

		/// <summary>
		/// Looks for the image format parameter. Sets the default format to PNG
		/// </summary>
		/// <param name="value">image format value</param>
		private void ParseFormat(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				Format = System.Drawing.Imaging.ImageFormat.Png;
				return;
			}

			switch (value.ToLower())
			{
				case "png":
					ContentType = "image/png";
					Format = System.Drawing.Imaging.ImageFormat.Png;
					break;
				case "jpg":
				case"jpeg":
					ContentType = "image/jpeg";
					Format = System.Drawing.Imaging.ImageFormat.Jpeg;
					break;
				case "bmp":
					ContentType = "image/bmp";
					Format = System.Drawing.Imaging.ImageFormat.Bmp;
					break;
				default:
					throw new ArgumentException("The format specified is not recognised.");
			}
		}

		/// <summary>
		/// Looks for size values in the format 'width,height'
		/// </summary>
		/// <param name="value">size parameter</param>
		/// <returns>size; empty if not specifed</returns>
		private Size ParseSize(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return Size.Empty;
			}

			string[] a = value.Split(',');
			if (a.Length != 2)
				return Size.Empty;

			Size result = new Size();
			if (a[0] == "")
				result.Width = 0;
			else
				result.Width = int.Parse(a[0]);

			if (a[1] == "")
				result.Height = 0;
			else
				result.Height = int.Parse(a[1]);

			return result;
		}

		/// <summary>
		/// Looks for a scaling factor, returns 1 if not specified
		/// </summary>
		/// <param name="value">scale parameter</param>
		/// <returns>scale value</returns>
		private float ParseScale(string value)
		{
			if (string.IsNullOrEmpty(value))
				return 1;

			return float.Parse(value);
		}

		/// <summary>
		/// Looks for margins in one of the allowed formats format 'm', 'h,v', 'l,t,r,b'
		/// </summary>
		/// <param name="value">margins parameter</param>
		private void ParseMargins(string value)
		{
			LeftMargin = 0;
			RightMargin = 0;
			TopMargin = 0;
			BottomMargin = 0;

			if (string.IsNullOrEmpty(value))
			{
				return;
			}

			string[] a = value.Split(',');

			if (a.Length == 1)
			{
				LeftMargin = int.Parse(a[0]);
				RightMargin = LeftMargin;
				TopMargin = LeftMargin;
				BottomMargin = LeftMargin;

				return;
			}

			if (a.Length == 2)
			{
				if (a[0] != "")
				{
					LeftMargin = int.Parse(a[0]);
					RightMargin = LeftMargin;
				}

				if (a[1] != "")
				{
					TopMargin = int.Parse(a[1]);
					BottomMargin = TopMargin;
				}

				return;
			}

			if (a.Length != 4)
				throw new ArgumentException("Margin format is incorrect");

			if (a[0] != "")
				LeftMargin = int.Parse(a[0]);
			if (a[1] != "")
				TopMargin = int.Parse(a[1]);
			if (a[2] != "")
				RightMargin = int.Parse(a[2]);
			if (a[3] != "")
				BottomMargin = int.Parse(a[3]);
		}

		/// <summary>
		/// looks for the barcode format parameter
		/// </summary>
		/// <param name="value">barcode format parameter</param>
		private void ParseType(string value)
		{
			if (string.IsNullOrEmpty(value))
				return;

			BarcodeFormats t = (BarcodeFormats)int.Parse(value);
			if (!Enum.IsDefined(typeof(BarcodeFormats), t))
				throw new ArgumentException("The requested barcode format is not supported");

			BarcodeFormat = t;
		}

		internal static string UrlBuilder(string data, BarcodeFormats format, IDictionary<string, object> options)
		{
			StringBuilder result = new StringBuilder();

			result.Append(System.Web.VirtualPathUtility.ToAbsolute(Config.Instance.Url));
			result.AppendFormat("?{0}={1}", RequestSettings.DATAKEY, data);
			result.AppendFormat("&{0}={1:d}", RequestSettings.BARCODEKEY, format);

			if (options.ContainsKey(RequestSettings.FORMATKEY))
				result.AppendFormat("&{0}={1}", RequestSettings.FORMATKEY, options[RequestSettings.FORMATKEY]);

			if (options.ContainsKey("scale"))
				result.AppendFormat("&{0}={1}", RequestSettings.SCALEKEY, options["scale"]);

			string tmp = GetSize(options);
			if (tmp != ",")
				result.AppendFormat("&{0}={1}", RequestSettings.SIZEKEY, tmp);

			tmp = GetMargins(options);
			if (tmp != "")
				result.AppendFormat("&{0}={1}", RequestSettings.MARGINKEY, tmp);

			return result.ToString();
		}

		private static string GetSize(IDictionary<string, object> options)
		{
			string result = "";
			if (options.ContainsKey("width"))
				result = options["width"].ToString();
			result += ",";
			if (options.ContainsKey("height"))
				result += options["height"].ToString();

			return result;
		}

		private static string GetMargins(IDictionary<string, object> options)
		{
			string result = "";

			if (options.ContainsKey("margin"))
				return options["margin"].ToString();

			return result;
		}
	}
}
