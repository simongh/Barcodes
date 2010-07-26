using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
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
            IsChecksumCalculated = true;
        }
    }
}
