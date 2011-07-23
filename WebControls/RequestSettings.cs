using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcodes.Web
{
	public class RequestSettings
	{
		public const string MARGINKEY = "m";
		public const string SCALEKEY = "sc";
		public const string SIZEKEY = "sz";
		public const string DATAKEY = "data";
		public const string FORMATKEY = "f";

		public Size Size
		{
			get;
			private set;
		}

		public int LeftMargin
		{
			get;
			private set;
		}

		public int TopMargin
		{
			get;
			private set;
		}

		public int RightMargin
		{
			get;
			private set;
		}

		public int BottomMargin
		{
			get;
			private set;
		}

		public float Scale
		{
			get;
			private set;
		}

		public System.Drawing.Imaging.ImageFormat Format
		{
			get;
			private set;
		}

		public string Data
		{
			get;
			private set;
		}

		public void Parse(System.Collections.Specialized.NameValueCollection values)
		{
			int c = 1;
			if (values[DATAKEY] == null)
				return;

			if (values[FORMATKEY] != null)
				c++;

			ParseFormat(values[FORMATKEY]);
			Data = values[DATAKEY];

			if (values.Count == c)
				return;

			Size = ParseSize(values[SIZEKEY]);
			Scale = ParseScale(values[SCALEKEY]);
			ParseMargins(values[MARGINKEY]);
		}

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
					Format = System.Drawing.Imaging.ImageFormat.Png;
					break;
				case "jpg":
				case"jpeg":
					Format = System.Drawing.Imaging.ImageFormat.Jpeg;
					break;
				case "bmp":
					Format = System.Drawing.Imaging.ImageFormat.Bmp;
					break;
				default:
					throw new ArgumentException("The format specified is not recognised.");
			}
		}

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

		private float ParseScale(string value)
		{
			if (string.IsNullOrEmpty(value))
				return 1;

			return float.Parse(value);
		}

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
	}
}
