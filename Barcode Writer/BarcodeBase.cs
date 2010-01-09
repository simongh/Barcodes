using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public abstract class BarcodeBase
    {
        internal static BarcodeBase Instance;

        protected Dictionary<char, Pattern> PatternSet
        {
            get;
            set;
        }

        protected abstract string AllowedChars
        {
            get;
        }

        public abstract bool IsCaseSensitive
        {
            get;
        }

        protected BarcodeBase()
        {
            Init();
        }

        public virtual Bitmap Paint(string text)
        {
            return Paint(new BarcodeSettings(), text);
        }

        public abstract Bitmap Paint(BarcodeSettings settings, string text);

        protected virtual void PaintText(Graphics canvas, BarcodeSettings settings, string text, int width)
        {
            if (!settings.IsTextShown)
                return;

            text = PadText(text, settings);

            SizeF textSize = canvas.MeasureString(text, settings.Font);
            int x = (width / 2) - ((int)textSize.Width / 2);
            int y = settings.TopMargin + settings.BarHeight + settings.TextPadding;

            canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            canvas.DrawString(text, settings.Font, Brushes.Black, x, y);
        }

        protected string PadText(string value, BarcodeSettings settings)
        {
            if (!settings.IsTextPadded)
                return value;


            StringBuilder sb = new StringBuilder();
            sb.Append(" ");
            foreach (char item in value)
            {
                sb.AppendFormat("{0} ", item);
            }
            return sb.ToString();
        }

        public virtual bool IsValidText(string value)
        {
            if (!IsCaseSensitive)
                value = value.ToUpper();

            foreach (char item in value)
            {
                if (!AllowedChars.Contains(item))
                    return false;
            }

            return true;
        }

        protected abstract void Init();

        protected void AddMeasure(BarcodeSettings settings, int width, Graphics canvas)
        {
            int left = 0;
            bool alt = true;

            while (left < width)
            {
                if (alt)
                    canvas.FillRectangle(Brushes.Gainsboro, left, 0, 1, settings.TopMargin);
                left++;
                alt = !alt;
            }
        }

        //public static Bitmap Generate(string text)
        //{
        //    return Instance.Paint(new BarcodeSettings(), text);
        //}


    }
}
