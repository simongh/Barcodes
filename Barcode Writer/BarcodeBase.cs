#define MEASURE
#define MARKER

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Barcodes
{
	/// <summary>
	/// Abstract class for all barcodes
	/// </summary>
	public abstract class BarcodeBase
	{
		protected static object TheLock = new object();

		//public static BarcodeBase2 Instance;
		public event EventHandler<AddChecksumEventArgs> AddChecksum;

		/// <summary>
		/// Gets or sets the list of patterns to draw
		/// </summary>
		protected static Dictionary<int, Pattern> PatternSet
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

		/// <summary>
		/// Gets the default settings for the barcode
		/// </summary>
		public BarcodeSettings DefaultSettings
		{
			get;
			protected set;
		}

		public BarcodeBase()
			: this(new BarcodeSettings())
		{ }

		public BarcodeBase(BarcodeSettings settings)
		{
			DefaultSettings = settings;

			Init();

			if (PatternSet == null)
			{
				lock (TheLock)
				{
					if (PatternSet == null)
						CreatePatternSet();
				}
			}
		}

		#region Drawing methods

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
			//sb.Append(" ");
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
			CodedValueCollection codes = new CodedValueCollection();
			text = ParseText(text, codes);

			if (settings.IsChecksumCalculated)
			{
				AddChecksumEventArgs args = new AddChecksumEventArgs(text, codes);
				OnAddChecksum(args);
				text = args.Text;
			}

			Size size = GetDimensions(settings, codes);

			Bitmap b = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			Graphics g = Graphics.FromImage(b);
			g.FillRectangle(Brushes.White, 0, 0, size.Width, size.Height);

#if MEASURE
			AddMeasure(settings, size.Width, g);
#endif
			State state = new State(g, settings, settings.LeftMargin, settings.LeftMargin);
			OnBeforeDrawCode(state);

#if MARKER
		   bool isGrey = true;
#endif

			for (int i = 0; i < codes.Count; i++)
			{
				state.ModuleValue = (char)codes[i];
				OnBeforeDrawModule(state, i);

#if MARKER
				if (isGrey)
					g.FillRectangle(Brushes.Gray, state.Left, 0, (PatternSet[codes[i]].WideCount * settings.WideWidth) + (PatternSet[codes[i]].NarrowCount * settings.NarrowWidth), size.Height);
				isGrey = !isGrey;
#endif

				foreach (Rectangle rect in PatternSet[codes[i]].Paint(settings))
				{
					rect.Offset(state.Left, state.Top);
					g.FillRectangle(Brushes.Black, rect);
				}


				OnAfterDrawModule(state, i);
			}

			OnAfterDrawCode(state);
			PaintText(g, settings, text, size.Width);

			return b;
		}

		/// <summary>
		/// Calculates the size of the barcode image &amp; sets the margins if a size is defined
		/// </summary>
		/// <param name="settings">setting in use with this barcode</param>
		/// <param name="codes">The encoded barcode data</param>
		/// <returns>size of the resultant image</returns>
		private Size GetDimensions(BarcodeSettings settings, CodedValueCollection codes)
		{
			int width = OnCalculateWidth(0, settings, codes);
			int height = OnCalculateHeight(0, settings, codes);

			if (settings.MaxWidth > 0 && width > settings.MaxWidth)
				throw new BarcodeSizeException("The barcode width exceeds the maximum allowed width");
			if (settings.MaxHeight > 0 && height > settings.MaxHeight)
				throw new BarcodeSizeException("The barcode height exceeds the maximum allowed height");

			if (settings.Width > 0)
			{
				if (settings.Width < width)
					throw new BarcodeSizeException("The barcode width is larger than the defined width");

				settings.LeftMargin = (settings.Width - width) / 2;
				settings.RightMargin = settings.Width - width - settings.LeftMargin;
				width = settings.Width;
			}
			if (settings.Height > 0)
			{
				if (settings.Height < height)
					throw new BarcodeSizeException("The barcode height is larger than the defined height");
				settings.TopMargin = (settings.Height - height) / 2;
				settings.BottomMargin = settings.Height - height - settings.TopMargin;
				height = settings.Height;
			}

			return new Size(width, height);
		}

		#endregion

		/// <summary>
		/// Parses the text into encodable characters
		/// </summary>
		/// <param name="value">the text to encode</param>
		/// <param name="codes">the code set to sue for encoding</param>
		/// <returns>the text after any clean-up</returns>
		protected virtual string ParseText(string value, CodedValueCollection codes)
		{
			if (!IsValidData(value))
				throw new ApplicationException();

			return value;
		}

		/// <summary>
		/// Main entry point for generating the barcode
		/// </summary>
		/// <param name="text">text to encode</param>
		/// <returns>bitmap of the generated barcode</returns>
		public Bitmap Generate(string text)
		{
			return Generate(text, DefaultSettings);
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

		/// <summary>
		/// Validates the text to ensure the barcode encoding scheme can support it.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public virtual bool IsValidData(string value)
		{
			return AllowedCharsPattern.IsMatch(value);
		}

		#region Event Raises

		/// <summary>
		/// Modify the width calculation
		/// </summary>
		/// <param name="width">calculated pixel width</param>
		/// <param name="codes">parsed value</param>
		/// <returns>new pixel width</returns>
		protected virtual int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
		{
			return width + settings.LeftMargin + settings.RightMargin + ((codes.Count - 1) * settings.ModulePadding);
		}

		/// <summary>
		/// Modify the height
		/// </summary>
		/// <param name="height">calculated pixel height</param>
		/// <param name="codes">parsed value</param>
		/// <returns>new pixel height</returns>
		protected virtual int OnCalculateHeight(int height, BarcodeSettings settings, CodedValueCollection codes)
		{
			
			height = settings.TopMargin + settings.BarHeight + settings.BottomMargin;
			
			if (settings.IsTextShown)
				height += Convert.ToInt32(settings.Font.GetHeight()) + settings.TextPadding;

			return height;
		}

		/// <summary>
		/// Occurs before the barcode is drawn
		/// </summary>
		/// <param name="state">drawing state object</param>
		protected virtual void OnBeforeDrawCode(State state)
		{
		}

		/// <summary>
		/// Occurs inbetween characters. Each character is a "module"
		/// </summary>
		/// <param name="state">drawing state</param>
		/// <param name="index">module index in the text to encode</param>
		protected virtual void OnBeforeDrawModule(State state, int index)
		{
		}

		/// <summary>
		/// Occurs after a module has been drawn
		/// </summary>
		/// <param name="state"></param>
		/// <param name="index"></param>
		protected virtual void OnAfterDrawModule(State state, int index)
		{
			state.Left += (PatternSet[state.ModuleValue].WideCount * state.Settings.WideWidth) + (PatternSet[state.ModuleValue].NarrowCount * state.Settings.NarrowWidth) + state.Settings.ModulePadding;
		}

		/// <summary>
		/// Occurs after the last module drawn
		/// </summary>
		/// <param name="state">drawing state object</param>
		protected virtual void OnAfterDrawCode(State state)
		{
		}

		/// <summary>
		/// Fires the add checksum event
		/// </summary>
		/// <param name="e">Arguments for the add checksum event</param>
		protected virtual void OnAddChecksum(AddChecksumEventArgs e)
		{
			if (AddChecksum == null)
				return;

			AddChecksum(this, e);
		}

		#endregion

		/// <summary>
		/// If the barcode supports a check digit, add it to the string
		/// </summary>
		/// <param name="value">barcode value to use for calculation</param>
		/// <returns>check digit to return</returns>
		public virtual string AddCheckDigit(string value)
		{
			CodedValueCollection codes = new CodedValueCollection();

			value = ParseText(value, codes);
			AddChecksumEventArgs e = new AddChecksumEventArgs(value, codes);
			OnAddChecksum(e);

			return e.Text;
		}

		#region To Implement

		/// <summary>
		/// initialises the barcode generator
		/// </summary>
		protected abstract void Init();

		/// <summary>
		/// Create the patterns in the static Patternset
		/// </summary>
		protected abstract void CreatePatternSet();

		#endregion
	}
}
