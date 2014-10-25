
namespace Barcodes
{
	public class Barcodes
	{
		public Factories.IBarcodeFactory BarcodeFactory { get; set; }

		public Factories.IRendererFactory RenderFactory { get; set; }

		public Barcodes()
		{ }

		public static Barcodes Initialise()
		{
			var result = new Barcodes();
			result.BarcodeFactory = new Factories.DefaultBarcodeFactory();
			result.RenderFactory = new Factories.DefaultRendererFactory();

			return result;
		}
	}
}
