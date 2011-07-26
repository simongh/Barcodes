using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace Barcodes.Web.UI
{
	public class Barcode : WebControl
	{
		public string Data
		{
			get;
			set;
		}

		public BarcodeFormats Format
		{
			get;
			set;
		}

		public System.Drawing.Size ImageSize
		{
			get;
			set;
		}

		public float Scale
		{
			get;
			set;
		}

		public int TopMargin
		{
			get;
			set;
		}

		public int BottomMargin
		{
			get;
			set;
		}

		public int LeftMargin
		{
			get;
			set;
		}

		public int RightMargin
		{
			get;
			set;
		}

		protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
		{
			Image img = new Image();
			img.CssClass = this.CssClass;

			img.ImageUrl = RequestSettings.UrlBuilder(Data, Format, new System.Web.Routing.RouteValueDictionary(null));

			img.RenderControl(writer);
		}
	}
}
