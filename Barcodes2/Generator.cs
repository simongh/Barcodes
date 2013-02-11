using System;

namespace Barcodes2
{
	public class Generator
	{
		private readonly Services.IRenderer _renderer;

		public BarcodeSettings Settings
		{
			get { return _renderer.Settings; }
			set { _renderer.Settings = value; }
		}

		public string RendererName
		{
			get { return _renderer.Name; }
		}

		public Generator()
			: this(null, null)
		{ }

		public Generator(BarcodeSettings settings)
			: this(null, settings)
		{ }

		public Generator(Services.IRenderer renderer, BarcodeSettings settings)
		{
			_renderer = renderer ?? new Services.BitmapRenderer();
			Settings = settings ?? new BarcodeSettings();
		}

		public object Create(string value, BarcodeFormats format)
		{
			return Create(value, GetDefinition(format));
		}

		public object Create(string value, Definitions.IDefinition definition)
		{
			if (!definition.IsDataValid(value))
				throw new BarcodeException();

			var codes = definition.GetCodes(value);
			var dt = definition.GetDisplayText(value);

			if (definition.IsChecksumRequired || Settings.IsChecksumCalculated)
				dt = definition.AddChecksum(dt, codes);

			_renderer.Definition = definition;

			return _renderer.Render(codes, dt);
		}

		private Definitions.IDefinition GetDefinition(BarcodeFormats format)
		{
			switch (format)
			{
				case BarcodeFormats.Code3of9:
					return new Definitions.Code3of9();
				case BarcodeFormats.Code128:
					break;
				case BarcodeFormats.Codabar:
					return new Definitions.Codabar();
				case BarcodeFormats.Code11:
					return new Definitions.Code11();
				case BarcodeFormats.Code2of5:
					return new Definitions.Code2of5();
				case BarcodeFormats.Code93:
					return new Definitions.Code93();
				case BarcodeFormats.EAN128:
					break;
				case BarcodeFormats.EAN13:
					break;
				case BarcodeFormats.EAN8:
					break;
				case BarcodeFormats.Extended3of9:
					break;
				case BarcodeFormats.Interleaved2of5:
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

			throw new NotImplementedException();
		}
	}
}
