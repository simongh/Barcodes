using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcodes.Web
{
	/// <summary>
	/// Image generator interface
	/// </summary>
	public interface IGeneratorService
	{
		/// <summary>
		/// Create a barcode image from the specifed settings
		/// </summary>
		/// <param name="settings">settings</param>
		/// <returns>image stream</returns>
		System.IO.Stream GetBarcode(RequestSettings settings);
	}

	/// <summary>
	/// Creates an image from the specifed settings
	/// </summary>
	public class GeneratorService : IGeneratorService
	{
		private Dictionary<BarcodeFormats, BarcodeBase> _Barcodes;
		private object _Lock;

		public GeneratorService()
		{
			_Barcodes = new Dictionary<BarcodeFormats, BarcodeBase>();
			_Lock = new object();
		}

		public System.IO.Stream GetBarcode(RequestSettings settings)
		{
			BarcodeBase b = GetBarcode(settings.BarcodeFormat);
			if (!b.IsValidData(settings.Data))
				throw new ArgumentException("The data is not valid for the requested barcode");

			BarcodeSettings bs = b.DefaultSettings.Copy();
			bs.Size = settings.Size;
			bs.Scale(settings.Scale);
			bs.LeftMargin = settings.LeftMargin;
			bs.TopMargin = settings.TopMargin;
			bs.RightMargin = settings.RightMargin;
			bs.BottomMargin = settings.BottomMargin;

			System.Drawing.Bitmap result = b.Generate(settings.Data, bs);
			System.IO.MemoryStream resultstrm = new System.IO.MemoryStream();
			result.Save(resultstrm, settings.Format);

			return resultstrm;
		}

		private BarcodeBase GetBarcode(BarcodeFormats format)
		{
			if (_Barcodes.ContainsKey(format))
				return _Barcodes[format];

			lock (_Lock)
			{
				if (_Barcodes.ContainsKey(format))
					return _Barcodes[format];

				switch (format)
				{
					case BarcodeFormats.Code3of9:
						_Barcodes.Add(format, new Code3of9());
						break;
					case BarcodeFormats.Code128:
						_Barcodes.Add(format, new Code128());
						break;
					case BarcodeFormats.Codabar:
						_Barcodes.Add(format, new Codabar());
						break;
					case BarcodeFormats.Code11:
						_Barcodes.Add(format, new Code11());
						break;
					case BarcodeFormats.Code2of5:
						_Barcodes.Add(format, new Code2of5());
						break;
					case BarcodeFormats.Code93:
						_Barcodes.Add(format, new Code93());
						break;
					case BarcodeFormats.EAN128:
						_Barcodes.Add(format, new EAN128());
						break;
					case BarcodeFormats.EAN13:
						_Barcodes.Add(format, new EAN13());
						break;
					case BarcodeFormats.EAN8:
						_Barcodes.Add(format, new EAN8());
						break;
					case BarcodeFormats.Extended3of9:
						_Barcodes.Add(format, new ExtendedCode3of9());
						break;
					case BarcodeFormats.Interleaved2of5:
						_Barcodes.Add(format, new Interleaved2of5());
						break;
					case BarcodeFormats.UPC:
						_Barcodes.Add(format, new UPC());
						break;
					case BarcodeFormats.UPC2:
						_Barcodes.Add(format, new UPC2());
						break;
					case BarcodeFormats.UPC5:
						_Barcodes.Add(format, new UPC5());
						break;
					case BarcodeFormats.UPCE:
						_Barcodes.Add(format, new UPCE());
						break;
					case BarcodeFormats.CPC:
						_Barcodes.Add(format, new CPC());
						break;
					case BarcodeFormats.IntelligentMail:
						_Barcodes.Add(format, new IntelligentMail());
						break;
					case BarcodeFormats.Postnet:
						_Barcodes.Add(format, new PostNet());
						break;
					case BarcodeFormats.RM4SCC:
						_Barcodes.Add(format, new RM4SCC());
						break;
					default:
						break;
				}
			}
			return _Barcodes[format];
		}
	}
}
