#define MEASURE
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public abstract class BarcodeBase
    {
        //public static BarcodeBase2 Instance;

        protected Dictionary<int, Pattern> PatternSet
        {
            get;
            set;
        }

        protected System.Text.RegularExpressions.Regex AllowedCharsPattern
        {
            get;
            set;
        }

        protected BarcodeBase()
        {
            Init();
        }

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

        protected virtual BarcodeSettings GetDefaultSettings()
        {
            return new BarcodeSettings();
        }

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

        protected Bitmap Paint(BarcodeSettings settings, string text)
        {
            List<int> codes = new List<int>();
            text = ParseText(text, codes);

            int width = settings.LeftMargin + settings.RightMargin + (codes.Count * GetModuleWidth(settings)) + GetQuietSpace(settings, codes.Count);
            int height = settings.TopMargin + settings.BarHeight + settings.BottomMargin;
            if (settings.IsTextShown)
                height += Convert.ToInt32(settings.Font.GetHeight()) + settings.TextPadding;

            Bitmap b = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(Brushes.White, 0, 0, width, height);

#if MEASURE
            AddMeasure(settings, width, g);
#endif
            //int left = settings.LeftMargin + (10 * settings.NarrowWidth);
            State state = new State(g, settings, settings.LeftMargin, settings.LeftMargin);
            OnStartCode(state);

            for (int i = 0; i < codes.Count; i++)
            {
                OnDrawModule(state, i);

                foreach (Rectangle rect in PatternSet[codes[i]].Paint(settings))
                {
                    rect.Offset(state.Left, state.Top);
                    g.FillRectangle(Brushes.Black, rect);
                }

                state.Left += GetModuleWidth(settings);
            }

            OnEndCode(state);
            PaintText(g, settings, text, width);

            return b;
        }

        protected virtual void OnStartCode(State state)
        {
        }

        protected virtual void OnDrawModule(State state, int index)
        {
        }

        protected virtual void OnEndCode(State state)
        {
        }

        public virtual bool IsValidData(string value)
        {
            return AllowedCharsPattern.IsMatch(value);
        }

        public Bitmap Generate(string text)
        {
            return Paint(GetDefaultSettings(), text);
        }

        #region To Implement

        protected abstract void Init();

        protected abstract string ParseText(string value, List<int> codes);

        protected abstract int GetModuleWidth(BarcodeSettings settings);

        protected abstract int GetQuietSpace(BarcodeSettings settings, int length);

        #endregion
    }
}
