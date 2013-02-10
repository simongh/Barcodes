#define MEASURE
#define MARKER

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
		protected void AddMeasure(State state)
		{
			int left = 0;
			bool alt = true;

			while (left < state.Canvas.VisibleClipBounds.Width)
			{
				if (alt)
					state.Canvas.FillRectangle(Brushes.Gainsboro, left, 0, 1, state.Settings.TopMargin);
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
		protected virtual void PaintText(State state)
		{
			if (!state.Settings.IsTextShown)
				return;

			string text = PadText(state);

			SizeF textSize = state.Canvas.MeasureString(text, state.Settings.Font);
			int x = (int)(state.Canvas.VisibleClipBounds.Width / 2) - ((int)textSize.Width / 2);
			int y = state.Settings.TopMargin + state.Settings.BarHeight + state.Settings.TextPadding;

			state.Canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			state.Canvas.DrawString(text, state.Settings.Font, Brushes.Black, x, y);
		}

		/// <summary>
		/// If the barcode requires the text to be limited with a character(s), this adds them
		/// </summary>
		/// <param name="value">the text to pad</param>
		/// <param name="settings">the settings in use</param>
		/// <returns>the text with any padding added</returns>
		protected string PadText(State state)
		{
			if (!state.Settings.IsTextPadded)
				return state.Text;


			StringBuilder sb = new StringBuilder();
			//sb.Append(" ");
			foreach (char item in state.Text)
			{
				sb.AppendFormat("{0} ", item);
			}
			return sb.ToString();
		}

		protected Rectangle[] DrawPattern(Pattern pattern, State state)
		{
			List<Rectangle> rects = new List<Rectangle>();

			int offset = (state.Settings.MediumHeight - state.Settings.ShortHeight);
			int left = state.Left;
			Rectangle rect;
			foreach (Elements item in pattern.Elements)
			{
				switch (item)
				{
					case Elements.WideBlack:
						rect = new Rectangle(left, state.Top, state.Settings.WideWidth, state.Settings.BarHeight);
						left += state.Settings.WideWidth;

						rects.Add(rect);
						break;
					case Elements.WideWhite:
						left += state.Settings.WideWidth;
						break;
					case Elements.NarrowBlack:
						rect = new Rectangle(left, state.Top, state.Settings.NarrowWidth, state.Settings.BarHeight);
						left += state.Settings.NarrowWidth;

						rects.Add(rect);
						break;
					case Elements.NarrowWhite:
						left += state.Settings.NarrowWidth;
						break;
					case Elements.Tracker:
						rect = new Rectangle(left, state.Top + offset, state.Settings.NarrowWidth, state.Settings.ShortHeight);
						left += state.Settings.NarrowWidth;
						rects.Add(rect);
						break;
					case Elements.Ascender:
						rect = new Rectangle(left, state.Top, state.Settings.NarrowWidth, state.Settings.MediumHeight);
						left += state.Settings.NarrowWidth;
						rects.Add(rect);
						break;
					case Elements.Descender:
						rect = new Rectangle(left, state.Top + offset, state.Settings.NarrowWidth, state.Settings.MediumHeight);
						left += state.Settings.NarrowWidth;
						rects.Add(rect);
						break;
				}
			}

			return rects.ToArray();
		}

		/// <summary>
		/// Main function to draw the barcode
		/// </summary>
		/// <param name="settings">setting in use with this barcode</param>
		/// <param name="text">text to encode</param>
		/// <returns>bitmap image of the barcode</returns>
		protected Bitmap Paint(BarcodeSettings settings, string text)
		{
			State state = new State(settings, settings.LeftMargin, settings.TopMargin);
			state.Codes = new CodedValueCollection();
			state.Text = ParseText(text, state.Codes);

			if (settings.IsChecksumCalculated)
			{
				AddChecksumEventArgs args = new AddChecksumEventArgs(state);
				OnAddChecksum(args);
				state.Text = args.Text;
			}

			Size size = GetDimensions(settings, state.Codes);

			Bitmap b = new Bitmap(size.Width, size.Height);//, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);
			state.Canvas = Graphics.FromImage(b);

			Paint(state);

			return b;
		}

		protected void Paint(State state)
		{
			state.Canvas.ScaleTransform(state.Settings.Scale, state.Settings.Scale);
			state.Canvas.Clear(Color.White);

#if MEASURE
			AddMeasure(state);
#endif
			OnBeforeDrawCode(state);

#if MARKER
			bool isGrey = true;
#endif

			for (int i = 0; i < state.Codes.Count; i++)
			{
				state.ModuleValue = (char)state.Codes[i];
				OnBeforeDrawModule(state, i);

#if MARKER
				if (isGrey)
					state.Canvas.FillRectangle(Brushes.Gray, state.Left, 0, (PatternSet[state.Codes[i]].WideCount * state.Settings.WideWidth) + (PatternSet[state.Codes[i]].NarrowCount * state.Settings.NarrowWidth), state.Canvas.VisibleClipBounds.Height);
				isGrey = !isGrey;
#endif
				Rectangle[] r = DrawPattern(PatternSet[state.Codes[i]], state);
				if (r.Length > 0)
					state.Canvas.FillRectangles(Brushes.Black, r);


				OnAfterDrawModule(state, i);
			}

			OnAfterDrawCode(state);
			PaintText(state);
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

			return new Size((int)Math.Ceiling(width * settings.Scale), (int)Math.Ceiling(height * settings.Scale));
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
