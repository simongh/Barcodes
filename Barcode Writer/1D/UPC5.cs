using System;
using System.Drawing;

namespace Barcodes
{
	/// <summary>
	/// Universal Product Code (UPC) 5 digit code
	/// </summary>
	public class UPC5 : EAN
	{
		private readonly int[] _digitGrouping;

		protected override int[] DigitGrouping
		{
			get { return _digitGrouping; }
		}

		public UPC5()
			: base()
		{
			_digitGrouping = new int[] { 0, 5, 0 };
		}

		protected override void Init()
		{
			base.Init();

			DefaultSettings.TextPadding = 2;

			PatternSet.Add(33, Pattern.Parse("nb nw nb nb"));
			PatternSet.Add(34, Pattern.Parse("nw nb"));

			Parity.Clear();
			Parity.Add(new bool[] { true, true, false, false, false });
			Parity.Add(new bool[] { true, false, true, false, false });
			Parity.Add(new bool[] { true, false, false, true, false });
			Parity.Add(new bool[] { true, false, false, false, true });
			Parity.Add(new bool[] { false, true, true, false, false });
			Parity.Add(new bool[] { false, false, true, true, false });
			Parity.Add(new bool[] { false, false, false, true, true });
			Parity.Add(new bool[] { false, true, false, true, false });
			Parity.Add(new bool[] { false, true, false, false, true });
			Parity.Add(new bool[] { false, false, true, false, true });

			AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d{5}$");
		}

		protected override void CalculateParity(CodedValueCollection codes)
		{
			int total = (codes[0] + codes[2] + codes[4]) * 3;
			total += (codes[1] + codes[3]) * 9;
			total = (total % 10);

			bool[] parity = Parity[total];

			for (int i = 0; i < codes.Count; i++)
			{
				if (parity[i])
					codes[i] += 10;
			}
		}

		protected override void OnBeforeDrawModule(State state, int index)
		{
			if (index == 1)
				state.Left -= 3 * state.Settings.NarrowWidth;

			if (index > 1 && index % 2 == 1)
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
			width += ((-23) * settings.NarrowWidth);

			return base.OnCalculateWidth(width, settings, codes);
		}

		protected override string ParseText(string value, CodedValueCollection codes)
		{
			value = base.ParseText(value, codes);

			codes.Insert(4, 34);
			codes.Insert(3, 34);
			codes.Insert(2, 34);
			codes.Insert(1, 34);
			codes.Insert(0, 33);

			return value;
		}

	}
}
