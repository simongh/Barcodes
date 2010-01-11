#define MEASURE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public abstract class EAN : BarcodeBase
    {
        protected enum GuardType
        {
            Limit = 31,
            Split = 32
        }
        protected const int TEXTPADDING = 2;

        protected List<bool[]> Parity;

        //protected abstract bool ShowCountryCode
        //{
        //    get;
        //}

        protected abstract int[] DigitGrouping
        {
            get;
        }

        //protected override string AllowedChars
        //{
        //    get { return "0123456789"; }
        //}

        //public override bool IsCaseSensitive
        //{
        //    get { return false; }
        //}

//        protected override System.Drawing.Bitmap Paint(BarcodeSettings settings, string text)
//        {
//            if (!IsValidText(text))
//                throw new ApplicationException("The text provided is not valid.");

//            List<int> values = ParseText(text);
//            bool[] theParity = Parity[values[0]];

//            int width = settings.LeftMargin + settings.RightMargin + (Length * 7 * settings.NarrowWidth) + (11 * settings.NarrowWidth);
//            int height = settings.TopMargin + settings.BottomMargin + settings.BarHeight + Convert.ToInt32(settings.Font.GetHeight()) + TEXTPADDING;
//            if (ShowCountryCode)
//                width += TEXTPADDING;

//            Bitmap b = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
//            Graphics g = Graphics.FromImage(b);
//            g.FillRectangle(Brushes.White, 0, 0, width, height);

//#if MEASURE
//            AddMeasure(settings, width, g);
//#endif
//            int[] TextPoints = new int[3];

//            int left = settings.LeftMargin;
//            TextPoints[0] = left;

//            if (ShowCountryCode)
//                left += (7 * settings.NarrowWidth) + TEXTPADDING;
//            left += DrawGuardBar(settings, left, g, LIMITGUARDBAR);
//            TextPoints[1] = left;

//            int barindex;
//            for (int i = 1; i < values.Count; i++)
//            {
//                barindex = values[i];
//                if (i < 7)
//                {
//                    if (theParity[i - 1])
//                        barindex += 10;

//                }
//                else
//                {
//                    if (i == 7)
//                    {
//                        left += DrawGuardBar(settings, left, g, SPLITGUARDBAR);
//                        TextPoints[2] = left;
//                    }
//                    barindex += 20;
//                }

//                foreach (Rectangle rect in PatternSet[(char)barindex].Paint(settings))
//                {
//                    rect.Offset(left, settings.TopMargin);
//                    g.FillRectangle(Brushes.Black, rect);
//                }

//                left += settings.NarrowWidth * 7;
//            }
//            DrawGuardBar(settings, left, g, LIMITGUARDBAR);

//            PaintText(g, settings, text, TextPoints);

//            return b;
//        }

        protected void DrawGuardBar(State state, GuardType type)
        {
            Rectangle[] guardbar = PatternSet[(int)type].Paint(state.Settings);
            foreach (Rectangle bar in guardbar)
            {
                bar.Inflate(0, 5);
                bar.Offset(state.Left, state.Settings.TopMargin + 5);

                state.Canvas.FillRectangle(Brushes.Black, bar);
            }

            state.Left += PatternSet[(int)type].NarrowCount * state.Settings.NarrowWidth;
        }

        protected override void Init()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add(0, Pattern.Parse("0001101".ToCharArray()));
            PatternSet.Add(1, Pattern.Parse("0011001".ToCharArray()));
            PatternSet.Add(2, Pattern.Parse("0010011".ToCharArray()));
            PatternSet.Add(3, Pattern.Parse("0111101".ToCharArray()));
            PatternSet.Add(4, Pattern.Parse("0100011".ToCharArray()));
            PatternSet.Add(5, Pattern.Parse("0110001".ToCharArray()));
            PatternSet.Add(6, Pattern.Parse("0101111".ToCharArray()));
            PatternSet.Add(7, Pattern.Parse("0111011".ToCharArray()));
            PatternSet.Add(8, Pattern.Parse("0110111".ToCharArray()));
            PatternSet.Add(9, Pattern.Parse("0001011".ToCharArray()));

            PatternSet.Add(10, Pattern.Parse("0100111".ToCharArray()));
            PatternSet.Add(11, Pattern.Parse("0110011".ToCharArray()));
            PatternSet.Add(12, Pattern.Parse("0011011".ToCharArray()));
            PatternSet.Add(13, Pattern.Parse("0100001".ToCharArray()));
            PatternSet.Add(14, Pattern.Parse("0011101".ToCharArray()));
            PatternSet.Add(15, Pattern.Parse("0111001".ToCharArray()));
            PatternSet.Add(16, Pattern.Parse("0000101".ToCharArray()));
            PatternSet.Add(17, Pattern.Parse("0010001".ToCharArray()));
            PatternSet.Add(18, Pattern.Parse("0001001".ToCharArray()));
            PatternSet.Add(19, Pattern.Parse("0010111".ToCharArray()));

            PatternSet.Add(20, Pattern.Parse("1110010".ToCharArray()));
            PatternSet.Add(21, Pattern.Parse("1100110".ToCharArray()));
            PatternSet.Add(22, Pattern.Parse("1101100".ToCharArray()));
            PatternSet.Add(23, Pattern.Parse("1000010".ToCharArray()));
            PatternSet.Add(24, Pattern.Parse("1011100".ToCharArray()));
            PatternSet.Add(25, Pattern.Parse("1001110".ToCharArray()));
            PatternSet.Add(26, Pattern.Parse("1010000".ToCharArray()));
            PatternSet.Add(27, Pattern.Parse("1000100".ToCharArray()));
            PatternSet.Add(28, Pattern.Parse("1001000".ToCharArray()));
            PatternSet.Add(29, Pattern.Parse("1110100".ToCharArray()));

            PatternSet.Add((int)GuardType.Limit, Pattern.Parse("101".ToCharArray()));
            PatternSet.Add((int)GuardType.Split, Pattern.Parse("01010".ToCharArray()));

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

        //protected void CalculateChecksum(List<int> codes)
        //{
        //    int oddCount = 0, evenCount = 0;

        //    for (int i = 0; i < codes.Count; i++)
        //    {
        //        if (i % 2 == 0)
        //            evenCount += codes[i];
        //        else
        //            oddCount += codes[i];
        //    }

        //    int result = 10 - ((evenCount + (oddCount * 3)) % 10);
        //    codes.Add(result > 9 ? 0 : result);
        //}

        //public override bool IsValidText(string value)
        //{
        //    if (value.Length != Length)
        //        return false;

        //    return base.IsValidText(value);
        //}

        //protected virtual List<int> ParseText(string value)
        //{
        //    if (!IsValidText(value))
        //        throw new ApplicationException("The data was not valid for this barcode.");

        //    List<int> result = new List<int>();

        //    for (int i = 0; i < value.Length; i++)
        //    {
        //        result.Add(int.Parse(value.Substring(i, 1)));
        //    }

        //    return result;
        //}

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

        protected override int GetQuietSpace(BarcodeSettings settings, int length)
        {
            return (11 * settings.NarrowWidth) + (DigitGrouping[0] * GetModuleWidth(settings));
        }

        protected override void OnStartCode(State state)
        {
            state.Left += DigitGrouping[0] * GetModuleWidth(state.Settings);
            DrawGuardBar(state, GuardType.Limit);
        }

        protected override void OnDrawModule(State state, int index)
        {
            if (index == DigitGrouping[1])
                DrawGuardBar(state, GuardType.Split);
        }

        protected override void OnEndCode(State state)
        {
            DrawGuardBar(state, GuardType.Limit);
        }
    }
}
