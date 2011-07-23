using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcodes.Web
{
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

		public void ProcessRequest(System.Web.HttpContext context)
		{
			
		}
	}
}
