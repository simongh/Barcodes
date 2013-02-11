using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Barcodes2.Services
{
	public class BitmapRenderer : IRenderer
	{
		protected Graphics Canvas
		{
			get;
			set;
		}

		protected string DisplayText
		{
			get;
			set;
		}

		protected CodedValueCollection Codes
		{
			get;
			set;
		}

		public virtual string Name
		{
			get { return "BitmapRenderer"; }
		}

		public BarcodeSettings Settings
		{
			get;
			set;
		}

		public Definitions.IDefinition Definition
		{
			get;
			set;
		}

		public object Render(CodedValueCollection codes, string displayText)
		{
			Codes = codes;
			DisplayText = displayText;

			var size = GetDimensions();
			var b = new Bitmap(size.Width, size.Height);
			Canvas = Graphics.FromImage(b);

			var start = new Point(Settings.LeftMargin, Settings.TopMargin);

			return b;
		}

		private Size GetDimensions()
		{
			int width = Settings.LeftMargin + Settings.RightMargin + Definition.CalculateWidth(Settings, Codes);
			int height = Settings.TopMargin + Settings.BarHeight + Settings.BottomMargin;

			if (Settings.IsTextShown)
				height += Convert.ToInt32(Settings.Font.GetHeight()) + Settings.TextPadding;

			if (Settings.MaxWidth > 0 && width > Settings.MaxWidth)
				throw new BarcodeSizeException("The barcode width exceeds the maximum allowed width");
			if (Settings.MaxHeight > 0 && height > Settings.MaxHeight)
				throw new BarcodeSizeException("The barcode height exceeds the maximum allowed height");

			if (Settings.Width > 0)
			{
				if (Settings.Width < width)
					throw new BarcodeSizeException("The barcode width is larger than the defined width");

				Settings.LeftMargin = (Settings.Width - width) / 2;
				Settings.RightMargin = Settings.Width - width - Settings.LeftMargin;
				width = Settings.Width;
			}
			if (Settings.Height > 0)
			{
				if (Settings.Height < height)
					throw new BarcodeSizeException("The barcode height is larger than the defined height");
				Settings.TopMargin = (Settings.Height - height) / 2;
				Settings.BottomMargin = Settings.Height - height - Settings.TopMargin;
				height = Settings.Height;
			}

			return new Size((int)Math.Ceiling(width * Settings.Scale), (int)Math.Ceiling(height * Settings.Scale));
		}

		protected void Paint(Point location)
		{
			Canvas.ScaleTransform(Settings.Scale, Settings.Scale);
			Canvas.Clear(Color.White);

			PreRenderCode(location);

			for (int i = 0; i < Codes.Count; i++)
			{
				PreRenderModule(i, location);

				var p = Definition.GetPattern(Codes[i]);
				var r = DrawPattern(p, location);
				if (r.Length > 0)
					Canvas.FillRectangles(Brushes.Black, r);

				PostRenderModule(p, location);
			}

			PostRenderCode();
			PaintText();
		}

		protected virtual void PreRenderCode(Point start)
		{ }

		protected virtual void PreRenderModule(int index, Point location)
		{ }

		private Rectangle[] DrawPattern(Pattern pattern, Point location)
		{
			var rects = new List<Rectangle>();

			int offset = (Settings.MediumHeight - Settings.ShortHeight);
			int left = location.X;
			Rectangle rect;
			foreach (var item in pattern.Elements)
			{
				switch (item)
				{
					case Element.WideBlack:
						rect = new Rectangle(left, location.Y, Settings.WideWidth, Settings.BarHeight);
						left += Settings.WideWidth;

						rects.Add(rect);
						break;
					case Element.WideWhite:
						left += Settings.WideWidth;
						break;
					case Element.NarrowBlack:
						rect = new Rectangle(left, location.Y, Settings.NarrowWidth, Settings.BarHeight);
						left += Settings.NarrowWidth;

						rects.Add(rect);
						break;
					case Element.NarrowWhite:
						left += Settings.NarrowWidth;
						break;
					case Element.Tracker:
						rect = new Rectangle(left, location.Y + offset, Settings.NarrowWidth, Settings.ShortHeight);
						left += Settings.NarrowWidth;
						rects.Add(rect);
						break;
					case Element.Ascender:
						rect = new Rectangle(left, location.Y, Settings.NarrowWidth, Settings.MediumHeight);
						left += Settings.NarrowWidth;
						rects.Add(rect);
						break;
					case Element.Descender:
						rect = new Rectangle(left, location.Y + offset, Settings.NarrowWidth, Settings.MediumHeight);
						left += Settings.NarrowWidth;
						rects.Add(rect);
						break;
				}
			}

			return rects.ToArray();
		}

		protected virtual void PostRenderModule(Pattern pattern, Point location)
		{
			location.X += (pattern.WideCount * Settings.WideWidth) + (pattern.NarrowCount * Settings.NarrowWidth) + Settings.ModulePadding;
		}

		protected virtual void PostRenderCode()
		{ }

		private void PaintText()
		{
			if (!Settings.IsTextShown)
				return;

			string text = PadText();

			SizeF textSize = Canvas.MeasureString(text, Settings.Font);
			int x = (int)(Canvas.VisibleClipBounds.Width / 2) - ((int)textSize.Width / 2);
			int y = Settings.TopMargin + Settings.BarHeight + Settings.TextPadding;

			Canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			Canvas.DrawString(text, Settings.Font, Brushes.Black, x, y);
		}

		private string PadText()
		{
			if (!Settings.IsTextPadded)
				return DisplayText;


			var sb = new StringBuilder();
			//sb.Append(" ");
			foreach (char item in DisplayText)
			{
				sb.AppendFormat("{0} ", item);
			}
			return sb.ToString();
		}
	}
}
