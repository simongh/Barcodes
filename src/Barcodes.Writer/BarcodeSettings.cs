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
        public int BarHeight { get; set; } = 80;

        /// <summary>
        /// Gets or sets the height of short bar in 4-state codes
        /// </summary>
        public int ShortHeight { get; set; } = 26;

        /// <summary>
        /// Gets or sets the height of medium height bars in 4-state codes
        /// </summary>
        public int MediumHeight { get; set; } = 52;

        /// <summary>
        /// Gets or sets the left margin width in pixels
        /// </summary>
        public int LeftMargin { get; set; } = 10;

        /// <summary>
        /// Gets or sets the right margin width in pixels
        /// </summary>
        public int RightMargin { get; set; } = 10;

        /// <summary>
        /// Gets or sets the top margin width in pixels
        /// </summary>
        public int TopMargin { get; set; } = 10;

        /// <summary>
        /// Gets or sets the bottom margin width in pixels
        /// </summary>
        public int BottomMargin { get; set; } = 10;

        /// <summary>
        /// Gets or sets the width in pixels of wide bars when used
        /// </summary>
        public int WideWidth { get; set; } = 6;

        /// <summary>
        /// Gets or sets the width in pixels of narrow bars
        /// </summary>
        public int NarrowWidth { get; set; } = 2;

        /// <summary>
        /// Gets or sets whether text is displayed with the barcode
        /// </summary>
        public bool IsTextShown { get; set; } = true;

        /// <summary>
        /// Gets or sets the padding between the barcode and the text
        /// </summary>
        public int TextPadding { get; set; } = 10;

        /// <summary>
        /// Gets or sets whether spacing is added between individual characters in the barcode text
        /// </summary>
        public bool IsTextPadded { get; set; } = true;

        public int TextHeight { get; set; } = 12;

        /// <summary>
        /// Gets or sets the maximum width of the barcode image
        /// </summary>
        public int MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the maximum height of the barcode image
        /// </summary>
        public int MaxHeight { get; set; }

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
        /// Gets or sets whether the barcode will calculate & add the checksum (where defined)
        /// </summary>
        public bool IsChecksumCalculated { get; set; }

        /// <summary>
        /// Gets or sets the image width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the image height
        /// </summary>
        public int Height { get; set; }

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

        public float Scale { get; set; } = 1.0F;

        /// <summary>
        /// Creates a duplicate of these settings
        /// </summary>
        /// <returns>New seetings object</returns>
        public BarcodeSettings Copy()
        {
            return (BarcodeSettings)MemberwiseClone();
        }
    }
}