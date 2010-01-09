using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class BarcodeSettings
    {
        public int BarHeight
        {
            get;
            set;
        }

        public int LeftMargin
        {
            get;
            set;
        }

        public int RightMargin
        {
            get;
            set;
        }

        public int TopMargin
        {
            get;
            set;
        }

        public int BottomMargin
        {
            get;
            set;
        }

        public int WideWidth
        {
            get;
            set;
        }

        public int NarrowWidth
        {
            get;
            set;
        }

        public int BarSpacing
        {
            get;
            set;
        }

        public bool IsTextShown
        {
            get;
            set;
        }

        public int TextPadding
        {
            get;
            set;
        }

        public bool IsTextPadded
        {
            get;
            set;
        }

        public System.Drawing.Font Font
        {
            get;
            set;
        }

        public BarcodeSettings()
        {
            BarHeight = 80;
            LeftMargin = 10;
            RightMargin = 10;
            TopMargin = 10;
            BottomMargin = 10;
            WideWidth = 6;
            NarrowWidth = 2;
            BarSpacing = 2;
            IsTextShown = true;
            TextPadding = 10;
            IsTextPadded = true;
            Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 12);
        }
    }
}
