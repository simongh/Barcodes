using System;
using System.Collections.Generic;

namespace Barcodes2.Services
{
	public class DefinitionFactory : IDefinitionFactory
	{
		private IDictionary<BarcodeFormats, Definitions.IDefinition> _cache;

		public DefinitionFactory()
		{
			_cache = new Dictionary<BarcodeFormats, Definitions.IDefinition>();
		}

		public Definitions.IDefinition GetDefinition(BarcodeFormats format)
		{
			if (_cache.ContainsKey(format))
				return _cache[format];

			Definitions.IDefinition result = null;
			switch (format)
			{
				case BarcodeFormats.Code3of9:
					result = Locator.Get<Definitions.Single.Code3of9>();
					break;
				case BarcodeFormats.Code128:
					break;
				case BarcodeFormats.Codabar:
					result = Locator.Get<Definitions.Single.Codabar>();
					break;
				case BarcodeFormats.Code11:
					result = Locator.Get<Definitions.Single.Code11>();
					break;
				case BarcodeFormats.Code2of5:
					result = Locator.Get<Definitions.Single.Code2of5>();
					break;
				case BarcodeFormats.Code93:
					result = Locator.Get<Definitions.Single.Code93>();
					break;
				case BarcodeFormats.EAN128:
					break;
				case BarcodeFormats.EAN13:
					result = Locator.Get<Definitions.EAN.EAN13>();
					break;
				case BarcodeFormats.EAN8:
					break;
				case BarcodeFormats.Extended3of9:
					result = Locator.Get<Definitions.Single.ExtendedCode3of9>();
					break;
				case BarcodeFormats.Interleaved2of5:
					result = Locator.Get<Definitions.Single.Interleaved2of5>();
					break;
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

			if (result == null)
				throw new NotImplementedException();

			_cache[format] = result;
			return result;
		}
	}
}
