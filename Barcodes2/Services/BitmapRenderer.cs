using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

			Paint(start);

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

			var state = new RenderState(location);
			PreRenderCode(state);

			for (state.Index = 0; state.Index < Codes.Count; state.Index++)
			{
				state.CurrentPattern = Definition.GetPattern(Codes[state.Index]);
				PreRenderModule(state);

				var r = DrawPattern(state.CurrentPattern, state.Location);
				if (r.Length > 0)
					Canvas.FillRectangles(Brushes.Black, r);

				PostRenderModule(state);
			}

			PostRenderCode();
			PaintText();
		}

		protected virtual void PreRenderCode(RenderState state)
		{ }

		protected virtual void PreRenderModule(RenderState state)
		{ }

		private Rectangle[] DrawPattern(Pattern pattern, Point location)
		{
			var rects = new List<Rectangle>();

			bool isGuards = false;
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

						if (isGuards)
							GuardBarTransform(rect);

						rects.Add(rect);
						break;
					case Element.WideWhite:
						left += Settings.WideWidth;
						break;
					case Element.NarrowBlack:
						rect = new Rectangle(left, location.Y, Settings.NarrowWidth, Settings.BarHeight);
						left += Settings.NarrowWidth;

						if (isGuards)
							GuardBarTransform(rect);

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
					case Element.GuardBar:
						isGuards = true;
						break;
				}
			}

			return rects.ToArray();
		}

		protected virtual void PostRenderModule(RenderState state)
		{
			var location = state.Location;
			location.X += (state.CurrentPattern.WideCount * Settings.WideWidth) + (state.CurrentPattern.NarrowCount * Settings.NarrowWidth) + Settings.ModulePadding;
			state.Location = location;
		}

		protected virtual void PostRenderCode()
		{ }

		protected virtual void GuardBarTransform(Rectangle shape)
		{
			var offset = (5 * Settings.NarrowWidth) / 2;
			shape.Inflate(0, offset);
			shape.Offset(0, offset);
		}

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

		private int GetEndPoint(Rectangle[] pattern)
		{
			var r = pattern.Last();
			return r.X + r.Width;
		}
	}
}
