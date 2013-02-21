
namespace Barcodes2
{
	/// <summary>
	/// Barcode settings used to set various aspects of barcode generation
	/// </summary>
	public class BarcodeSettings
	{
		/// <summary>
		/// Gets or sets the height in pixels of the bars
		/// </summary>
		public int BarHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of short bar in 4-state codes
		/// </summary>
		public int ShortHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the height of medium height bars in 4-state codes
		/// </summary>
		public int MediumHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the left margin width in pixels
		/// </summary>
		public int LeftMargin
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the right margin width in pixels
		/// </summary>
		public int RightMargin
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the top margin width in pixels
		/// </summary>
		public int TopMargin
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the bottom margin width in pixels
		/// </summary>
		public int BottomMargin
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the width in pixels of wide bars when used
		/// </summary>
		public int WideWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the width in pixels of narrow bars
		/// </summary>
		public int NarrowWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the padding in pixels between modules
		/// </summary>
		public int ModulePadding
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether text is displayed with the barcode
		/// </summary>
		public bool IsTextShown
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the padding between the barcode and the text
		/// </summary>
		public int TextPadding
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether spacing is added between individual characters in the barcode text
		/// </summary>
		public bool IsTextPadded
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the maximum width of the barcode image
		/// </summary>
		public int MaxWidth
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the maximum height of the barcode image
		/// </summary>
		public int MaxHeight
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the maximum size of the barcode image
		/// </summary>
		public System.Drawing.Size MaxSize
		{
			get { return new System.Drawing.Size(MaxWidth, MaxHeight); }
			set
			{
				MaxHeight = value.Height;
				MaxWidth = value.Width;
			}
		}

		/// <summary>
		/// Gets or sets the font to use for the text
		/// </summary>
		public System.Drawing.Font Font
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets whether the barcode will calculate & add the checksum (where defined)
		/// </summary>
		public bool IsChecksumCalculated
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the image width
		/// </summary>
		public int Width
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the image height
		/// </summary>
		public int Height
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the image size
		/// </summary>
		public System.Drawing.Size Size
		{
			get { return new System.Drawing.Size(Width, Height); }
			set
			{
				Height = value.Height;
				Width = value.Width;
			}
		}

		public float Scale
		{
			get;
			set;
		}

		public BarcodeSettings()
		{
			BarHeight = 80;
			ShortHeight = BarHeight / 3;
			MediumHeight = (BarHeight / 3) * 2;
			LeftMargin = 10;
			RightMargin = 10;
			TopMargin = 10;
			BottomMargin = 10;
			WideWidth = 6;
			NarrowWidth = 2;
			ModulePadding = 2;
			IsTextShown = true;
			TextPadding = 10;
			IsTextPadded = true;
			Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 12);
			IsChecksumCalculated = false;
			Scale = 1.0F;
		}

		/// <summary>
		/// Creates a duplicate of these settings
		/// </summary>
		/// <returns>New seetings object</returns>
		public BarcodeSettings Copy()
		{
			return (BarcodeSettings)this.MemberwiseClone();
		}

		/// <summary>
		/// Scales the barcode. The dimensions & max dimensions are not scaled.
		/// </summary>
		/// <param name="value">scale factor</param>
		//public void Scale(float value)
		//{
		//    if (value == 1)
		//        return;

		//    BarHeight = ScaleValue(value, BarHeight);
		//    ShortHeight = ScaleValue(value, ShortHeight);
		//    MediumHeight = ScaleValue(value, ShortHeight);
		//    LeftMargin = ScaleValue(value, LeftMargin);
		//    RightMargin = ScaleValue(value, RightMargin);
		//    TopMargin = ScaleValue(value, TopMargin);
		//    BottomMargin = ScaleValue(value, BottomMargin);
		//    WideWidth = ScaleValue(value, WideWidth);
		//    NarrowWidth = ScaleValue(value, NarrowWidth);
		//    ModulePadding = ScaleValue(value, ModulePadding);
		//    TextPadding = ScaleValue(value, TextPadding);
		//    Font = new System.Drawing.Font(Font.Name, Font.Size * value);
		//}

		//private int ScaleValue(float scale, int value)
		//{
		//    return (int)Math.Floor((float)value * scale);
		//}
	}
}
