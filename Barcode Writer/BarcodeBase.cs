#define MEASURE
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    /// <summary>
    /// Abstract class for all barcodes
    /// </summary>
    public abstract class BarcodeBase
    {
        //public static BarcodeBase2 Instance;

        /// <summary>
        /// Gets or sets the list of patterns to draw
        /// </summary>
        protected Dictionary<int, Pattern> PatternSet
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the allowed characters expression
        /// </summary>
        protected System.Text.RegularExpressions.Regex AllowedCharsPattern
        {
            get;
            set;
        }

        protected BarcodeBase()
        {
            Init();
        }

        /// <summary>
        /// Test method for adding a measure along the top of the image
        /// </summary>
        /// <param name="settings">Settings to sue for calculations</param>
        /// <param name="width">width of the image in pixels</param>
        /// <param name="canvas">Canvas to draw on</param>
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

        /// <summary>
        /// Get the default settings for the barcode
        /// </summary>
        /// <returns>Settings object</returns>
        protected virtual BarcodeSettings GetDefaultSettings()
        {
            return new BarcodeSettings();
        }

        /// <summary>
        /// Draws the text below the barcode
        /// </summary>
        /// <param name="canvas">the canvas object on which to draw</param>
        /// <param name="settings">the settigns file in use for the barcode</param>
        /// <param name="text">the text to draw</param>
        /// <param name="width">The width of the barcode to align the text</param>
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

        /// <summary>
        /// If the barcode requires the text to be limited with a character(s), this adds them
        /// </summary>
        /// <param name="value">the text to pad</param>
        /// <param name="settings">the settings in use</param>
        /// <returns>the text with any padding added</returns>
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

        /// <summary>
        /// Main function to draw the barcode
        /// </summary>
        /// <param name="settings">setting in use with this barcode</param>
        /// <param name="text">text to encode</param>
        /// <returns>bitmap image of the barcode</returns>
        protected Bitmap Paint(BarcodeSettings settings, string text)
        {
            List<int> codes = new List<int>();
            text = ParseText(text, codes);

            int width = settings.LeftMargin + settings.RightMargin + (codes.Count * GetModuleWidth(settings)) + GetQuietSpace(settings, codes.Count);
            int height = settings.TopMargin + settings.BarHeight + settings.BottomMargin;
            if (settings.IsTextShown)
                height += Convert.ToInt32(settings.Font.GetHeight()) + settings.TextPadding;

            if (settings.MaxWidth > 0 && width > settings.MaxWidth)
                return null;
            if (settings.MaxHeight > 0 && height > settings.MaxHeight)
                return null;

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

        /// <summary>
        /// Occurs before the barcode is drawn
        /// </summary>
        /// <param name="state">drawing state object</param>
        protected virtual void OnStartCode(State state)
        {
        }

        /// <summary>
        /// Occurs inbetween characters. Each character is a "module"
        /// </summary>
        /// <param name="state">drawing state</param>
        /// <param name="index">module index in the text to encode</param>
        protected virtual void OnDrawModule(State state, int index)
        {
        }

        /// <summary>
        /// Occurs after the last module drawn
        /// </summary>
        /// <param name="state">drawing state object</param>
        protected virtual void OnEndCode(State state)
        {
        }

        /// <summary>
        /// Validates the text to ensure the barcode encoding scheme can support it.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool IsValidData(string value)
        {
            return AllowedCharsPattern.IsMatch(value);
        }

        /// <summary>
        /// Main entry point for generating the barcode
        /// </summary>
        /// <param name="text">text to encode</param>
        /// <returns>bitmap of the generated barcode</returns>
        public Bitmap Generate(string text)
        {
            return Paint(GetDefaultSettings(), text);
        }

        /// <summary>
        /// Main entry point for generating the barcode
        /// </summary>
        /// <param name="text">text to encode</param>
        /// <param name="settings">setting to use to overide the defaults</param>
        /// <returns>bitmap of the generated barcode</returns>
        public Bitmap Generate(string text, BarcodeSettings settings)
        {
            return Paint(settings, text);
        }

        #region To Implement

        /// <summary>
        /// initialises the barcode generator
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// Parses the text into encodable characters
        /// </summary>
        /// <param name="value">the text to encode</param>
        /// <param name="codes">the code set to sue for encoding</param>
        /// <returns>the text after any clean-up</returns>
        protected abstract string ParseText(string value, List<int> codes);

        /// <summary>
        /// Get the width of a module usin the supplied settings
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        protected abstract int GetModuleWidth(BarcodeSettings settings);

        /// <summary>
        /// Returns any required quiet space added to the barcode for width calculations
        /// </summary>
        /// <param name="settings">The settings to use</param>
        /// <param name="length">the length of the bardcode</param>
        /// <returns>pixels to add to the width</returns>
        protected abstract int GetQuietSpace(BarcodeSettings settings, int length);

        #endregion
    }
}
