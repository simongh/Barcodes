using System;

namespace Barcodes2.Services
{
	public class DefinitionFactory : IDefinitionFactory
	{
		public Definitions.IDefinition GetDefinition(BarcodeFormats format)
		{
			switch (format)
			{
				case BarcodeFormats.Code3of9:
					return Locator.Get<Definitions.Single.Code3of9>();
				case BarcodeFormats.Code128:
					break;
				case BarcodeFormats.Codabar:
					return Locator.Get<Definitions.Single.Codabar>();
				case BarcodeFormats.Code11:
					return Locator.Get<Definitions.Single.Code11>();
				case BarcodeFormats.Code2of5:
					return Locator.Get<Definitions.Single.Code2of5>();
				case BarcodeFormats.Code93:
					return Locator.Get<Definitions.Single.Code93>();
				case BarcodeFormats.EAN128:
					break;
				case BarcodeFormats.EAN13:
					break;
				case BarcodeFormats.EAN8:
					break;
				case BarcodeFormats.Extended3of9:
					return Locator.Get<Definitions.Single.ExtendedCode3of9>();
				case BarcodeFormats.Interleaved2of5:
					return Locator.Get<Definitions.Single.Interleaved2of5>();
				case BarcodeFormats.UPC:
					break;
				case BarcodeFormats.UPC2:
					break;
				case BarcodeFormats.UPC5:
					break;
				case BarcodeFormats.UPCE:
					break;
				case BarcodeFormats.CPC:
					break;
				case BarcodeFormats.IntelligentMail:
					break;
				case BarcodeFormats.Postnet:
					break;
				case BarcodeFormats.RM4SCC:
					break;
				default:
					break;
			}

			throw new NotImplementedException();
		}
	}
}
