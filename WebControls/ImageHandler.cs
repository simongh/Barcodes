using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Barcodes.Web
{
	/// <summary>
	/// Barcode image handler
	/// </summary>
	public class ImageHandler : System.Web.IHttpHandler
	{
		private IGeneratorService _Generator;

		public bool IsReusable
		{
			get { return true; }
		}

		public ImageHandler()
			: this(null)
		{ }

		public ImageHandler(IGeneratorService service)
		{
			_Generator = service ?? new GeneratorService();
		}

		/// <summary>
		/// Returns a barcode image
		/// </summary>
		/// <param name="context"></param>
		public void ProcessRequest(System.Web.HttpContext context)
		{
			RequestSettings s = new RequestSettings();
			s.Parse(context.Request.QueryString);

			Stream img = _Generator.GetBarcode(s);

			context.Response.ContentType = s.ContentType;

			const int chunk = 1024;
			byte[] buffer = new byte[chunk];
			int read = img.Read(buffer, 0, chunk);
			while (read == chunk)
			{
				context.Response.OutputStream.Write(buffer, 0, chunk);
				read = img.Read(buffer, 0, chunk);
			}
			context.Response.OutputStream.Write(buffer, 0, read);
		}

	}
}
