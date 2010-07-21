#define MEASURE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    /// <summary>
    /// Base class for EAN codes
    /// </summary>
    public abstract class EAN : BarcodeBase
    {
        
        /// <summary>
        /// The type of guard bar to use
        /// </summary>
        protected enum GuardType
        {
            Limit = 31,
            Split = 32
        }
        protected const int TEXTPADDING = 2;

        /// <summary>
        /// List of parity settings used for calculations
        /// </summary>
        protected List<bool[]> Parity;

        /// <summary>
        /// Gets the digits are groupings
        /// </summary>
        protected abstract int[] DigitGrouping
        {
            get;
        }

        /// <summary>
        /// Draws the required guard bar
        /// </summary>
        /// <param name="state">drawing state</param>
        /// <param name="type">type of guard bar to draw</param>
        protected void DrawGuardBar(State state, GuardType type)
        {
            Rectangle[] guardbar = PatternSet[(int)type].Paint(state.Settings);
            int offset = (5 * state.Settings.NarrowWidth) / 2;
            foreach (Rectangle bar in guardbar)
            {
                bar.Inflate(0, offset);
                bar.Offset(state.Left, state.Top + offset);

                state.Canvas.FillRectangle(Brushes.Black, bar);
            }

            state.Left += PatternSet[(int)type].NarrowCount * state.Settings.NarrowWidth;
        }

        protected override void Init()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add(0, Pattern.Parse("0 0 0 1 1 0 1"));
            PatternSet.Add(1, Pattern.Parse("0 0 1 1 0 0 1"));
            PatternSet.Add(2, Pattern.Parse("0 0 1 0 0 1 1"));
            PatternSet.Add(3, Pattern.Parse("0 1 1 1 1 0 1"));
            PatternSet.Add(4, Pattern.Parse("0 1 0 0 0 1 1"));
            PatternSet.Add(5, Pattern.Parse("0 1 1 0 0 0 1"));
            PatternSet.Add(6, Pattern.Parse("0 1 0 1 1 1 1"));
            PatternSet.Add(7, Pattern.Parse("0 1 1 1 0 1 1"));
            PatternSet.Add(8, Pattern.Parse("0 1 1 0 1 1 1"));
            PatternSet.Add(9, Pattern.Parse("0 0 0 1 0 1 1"));

            PatternSet.Add(10, Pattern.Parse("0 1 0 0 1 1 1"));
            PatternSet.Add(11, Pattern.Parse("0 1 1 0 0 1 1"));
            PatternSet.Add(12, Pattern.Parse("0 0 1 1 0 1 1"));
            PatternSet.Add(13, Pattern.Parse("0 1 0 0 0 0 1"));
            PatternSet.Add(14, Pattern.Parse("0 0 1 1 1 0 1"));
            PatternSet.Add(15, Pattern.Parse("0 1 1 1 0 0 1"));
            PatternSet.Add(16, Pattern.Parse("0 0 0 0 1 0 1"));
            PatternSet.Add(17, Pattern.Parse("0 0 1 0 0 0 1"));
            PatternSet.Add(18, Pattern.Parse("0 0 0 1 0 0 1"));
            PatternSet.Add(19, Pattern.Parse("0 0 1 0 1 1 1"));

            PatternSet.Add(20, Pattern.Parse("1 1 1 0 0 1 0"));
            PatternSet.Add(21, Pattern.Parse("1 1 0 0 1 1 0"));
            PatternSet.Add(22, Pattern.Parse("1 1 0 1 1 0 0"));
            PatternSet.Add(23, Pattern.Parse("1 0 0 0 0 1 0"));
            PatternSet.Add(24, Pattern.Parse("1 0 1 1 1 0 0"));
            PatternSet.Add(25, Pattern.Parse("1 0 0 1 1 1 0"));
            PatternSet.Add(26, Pattern.Parse("1 0 1 0 0 0 0"));
            PatternSet.Add(27, Pattern.Parse("1 0 0 0 1 0 0"));
            PatternSet.Add(28, Pattern.Parse("1 0 0 1 0 0 0"));
            PatternSet.Add(29, Pattern.Parse("1 1 1 0 1 0 0"));

            PatternSet.Add((int)GuardType.Limit, Pattern.Parse("1 0 1"));
            PatternSet.Add((int)GuardType.Split, Pattern.Parse("0 1 0 1 0"));

            Parity = new List<bool[]>();
            Parity.Add(new bool[] { false, false, false, false, false, false });
            Parity.Add(new bool[] { false, false, true, false, true, true });
            Parity.Add(new bool[] { false, false, true, true, false, true });
            Parity.Add(new bool[] { false, false, true, true, true, false });
            Parity.Add(new bool[] { false, true, false, false, true, true });
            Parity.Add(new bool[] { false, true, true, false, false, true });
            Parity.Add(new bool[] { false, true, true, true, false, false });
            Parity.Add(new bool[] { false, true, false, true, false, true });
            Parity.Add(new bool[] { false, true, false, true, true, false });
            Parity.Add(new bool[] { false, true, true, false, true, false });
        }

        /// <summary>
        /// Calculate parity for given list of codes
        /// </summary>
        /// <param name="codes">list of values</param>
        protected virtual void CalculateParity(List<int> codes)
        {
            bool[] parity = Parity[codes[0]];

            for (int i = 1; i < codes.Count; i++)
            {
                if (i < 1 + DigitGrouping[1])
                {
                    if (parity[i - 1])
                        codes[i] += 10;
                }
                else
                    codes[i] += 20;
            }
            codes.RemoveAt(0);
        }

        protected override void PaintText(Graphics canvas, BarcodeSettings settings, string text, int width)
        {
            int y = settings.TopMargin + settings.BarHeight + TEXTPADDING;
            canvas.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int x = settings.LeftMargin;

            if (DigitGrouping[0] > 0)
                canvas.DrawString(text.Substring(0, DigitGrouping[0]), settings.Font, Brushes.Black, x, y);
            
            x += DigitGrouping[0] * GetModuleWidth(settings);
            x += (PatternSet[(int)GuardType.Limit].NarrowCount * settings.NarrowWidth) + settings.TextPadding;
            canvas.DrawString(text.Substring(DigitGrouping[0], DigitGrouping[1]), settings.Font, Brushes.Black, x, y);

            x += DigitGrouping[1] * GetModuleWidth(settings);
            x += (PatternSet[(int)GuardType.Split].NarrowCount * settings.NarrowWidth);
            canvas.DrawString(text.Substring(DigitGrouping[0]+DigitGrouping[1], DigitGrouping[2]), settings.Font, Brushes.Black, x, y);
        }

        protected override string ParseText(string value, List<int> codes)
        {
            if (!IsValidData(value))
                throw new ApplicationException("The data was not valid.");

            for (int i = 0; i < value.Length; i++)
            {
                codes.Add(int.Parse(value.Substring(i, 1)));

            }

            CalculateParity(codes);

            return value;
        }

        protected override int GetModuleWidth(BarcodeSettings settings)
        {
            return 7 * settings.NarrowWidth;
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, List<int> codes)
        {
            return width + ((11 * settings.NarrowWidth) + (DigitGrouping[0] * GetModuleWidth(settings)));
        }
        
        protected override void OnBeforeDrawCode(State state)
        {
            state.Left += DigitGrouping[0] * GetModuleWidth(state.Settings);
            DrawGuardBar(state, GuardType.Limit);
        }

        protected override void OnBeforeDrawModule(State state, int index)
        {
            if (index == DigitGrouping[1])
                DrawGuardBar(state, GuardType.Split);
        }

        protected override void OnAfterDrawCode(State state)
        {
            DrawGuardBar(state, GuardType.Limit);
        }
    }
}
