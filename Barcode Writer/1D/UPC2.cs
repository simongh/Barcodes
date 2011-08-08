using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Barcodes
{
	/// <summary>
	/// Universal Product Code (UPC) 2 digit code
	/// </summary>
	public class UPC2 : EAN
	{
		private readonly int[] _digitGrouping;

		protected override int[] DigitGrouping
		{
			get { return _digitGrouping; }
		}

		public UPC2()
			: base()
		{
			_digitGrouping = new int[] { 0, 2, 0 };
		}

		protected override void Init()
		{
			base.Init();

			DefaultSettings.TextPadding = 2;

			PatternSet.Add(33, Pattern.Parse("nb nw nb nb"));
			PatternSet.Add(34, Pattern.Parse("nw nb"));

			Parity.Clear();

			AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d{2}$");
		}

		protected override void CalculateParity(CodedValueCollection codes)
		{
			int m = ((codes[0] * 10) + codes[1]) % 4;

			if (m > 1)
				codes[0] += 10;

			if (m == 1 || m == 3)
				codes[1] += 10;
		}

		protected override void OnBeforeDrawModule(State state, int index)
		{
			if (index == 1)
				state.Left -= 3 * state.Settings.NarrowWidth;

			if (index == 3)
				state.Left -= 5 * state.Settings.NarrowWidth;
		}

		protected override void OnBeforeDrawCode(State state)
		{
			state.Top += Convert.ToInt32(state.Settings.Font.GetHeight()) + state.Settings.TextPadding;
		}

		protected override void OnAfterDrawCode(State state)
		{
			//Do nothing
		}

		protected override void PaintText(State state)
		{
			if (!state.Settings.IsTextShown)
				return;

			string text = PadText(state);

			SizeF textSize = state.Canvas.MeasureString(text, state.Settings.Font);
			int x = (int)(state.Canvas.VisibleClipBounds.Width / 2) - ((int)textSize.Width / 2) - 4;
			int y = state.Settings.TopMargin;

			state.Canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			state.Canvas.DrawString(text, state.Settings.Font, Brushes.Black, x, y);
		}

		protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
		{
			width += (-8 * settings.NarrowWidth);

			return base.OnCalculateWidth(width, settings, codes);
		}

		protected override string ParseText(string value, CodedValueCollection codes)
		{
			value = base.ParseText(value, codes);

			codes.Insert(0, 33);
			codes.Insert(2, 34);

			return value;
		}

	}
}
