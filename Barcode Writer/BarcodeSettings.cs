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

        public int ShortHeight
        {
            get;
            set;
        }

        public int MediumHeight
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

        public int ModulePadding
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

        public int MaxWidth
        {
            get;
            set;
        }

        public int MaxHeight
        {
            get;
            set;
        }

        public System.Drawing.Size MaxSize
        {
            get { return new System.Drawing.Size(MaxWidth, MaxHeight); }
            set
            {
                MaxHeight = value.Height;
                MaxWidth = value.Width;
            }
        }

        public System.Drawing.Font Font
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
        }
    }
}
