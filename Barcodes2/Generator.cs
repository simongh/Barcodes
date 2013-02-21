
namespace Barcodes2
{
	public class Generator
	{
		private readonly Services.IRenderer _renderer;
		private readonly Services.DefinitionFactory _definitions;

		public BarcodeSettings Settings
		{
			get;
			set;
		}

		public string RendererName
		{
			get { return _renderer.Name; }
		}

		public Generator()
			: this(null)
		{ }

		public Generator(BarcodeSettings settings)
			: this(Services.Locator.Get<Services.BitmapRenderer>(), Services.Locator.Get<Services.DefinitionFactory>(), settings)
		{ }

		public Generator(Services.IRenderer renderer, Services.DefinitionFactory definitions, BarcodeSettings settings)
		{
			_renderer = renderer;
			_definitions = definitions;
			Settings = settings ?? new BarcodeSettings();
		}

		public object Create(string value, BarcodeFormats format)
		{
			return Create(value, _definitions.GetDefinition(format));
		}

		public object Create(string value, Definitions.IDefinition definition)
		{
			_renderer.Settings = Settings.Copy();
			definition.TransformSettings(_renderer.Settings);

			if (!definition.IsDataValid(value))
				throw new BarcodeException();

			var codes = definition.GetCodes(value);
			var dt = definition.GetDisplayText(value);

			if (definition.IsChecksumRequired || _renderer.Settings.IsChecksumCalculated)
				dt = definition.AddChecksum(dt, codes);

			_renderer.Definition = definition;

			return _renderer.Render(codes, dt);
		}
	}
}
