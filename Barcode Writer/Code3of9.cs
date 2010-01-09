#define MEASURE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public class Code3of9 : BarcodeBase
    {
        //internal readonly static Code3of9 Instance;

        protected override string AllowedChars
        {
            get { return "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.$/+% "; }
        }

        public override bool IsCaseSensitive
        {
            get { return false; }
        }

        private Code3of9()
            : base()
        { }

        static Code3of9()
        {
            Instance = new Code3of9();
        }

        protected override void Init()
        {
            PatternSet = new Dictionary<char, Pattern>();

            PatternSet.Add('0', Pattern.Parse("nb nw nb ww wb nw wb nw nb"));
            PatternSet.Add('1', Pattern.Parse("wb nw nb ww nb nw nb nw wb"));
            PatternSet.Add('2', Pattern.Parse("nb nw wb ww nb nw nb nw wb"));
            PatternSet.Add('3', Pattern.Parse("wb nw wb ww nb nw nb nw nb"));
            PatternSet.Add('4', Pattern.Parse("nb nw nb ww wb nw nb nw wb"));
            PatternSet.Add('5', Pattern.Parse("wb nw nb ww wb nw nb nw nb"));
            PatternSet.Add('6', Pattern.Parse("nb nw wb ww wb nw nb nw nb"));
            PatternSet.Add('7', Pattern.Parse("nb nw nb ww nb nw wb nw wb"));
            PatternSet.Add('8', Pattern.Parse("wb nw nb ww nb nw wb nw nb"));
            PatternSet.Add('9', Pattern.Parse("nb nw wb ww nb nw wb nw nb"));
            PatternSet.Add('A', Pattern.Parse("wb nw nb nw nb ww nb nw wb"));
            PatternSet.Add('B', Pattern.Parse("nb nw wb nw nb ww nb nw wb"));
            PatternSet.Add('C', Pattern.Parse("wb nw wb nw nb ww nb nw nb"));
            PatternSet.Add('D', Pattern.Parse("nb nw nb nw wb ww nb nw wb"));
            PatternSet.Add('E', Pattern.Parse("wb nw nb nw wb ww nb nw nb"));
            PatternSet.Add('F', Pattern.Parse("nb nw wb nw wb ww nb nw nb"));
            PatternSet.Add('G', Pattern.Parse("nb nw nb nw nb ww wb nw wb"));
            PatternSet.Add('H', Pattern.Parse("wb nw nb nw nb ww wb nw nb"));
            PatternSet.Add('I', Pattern.Parse("nb nw wb nw nb ww wb nw nb"));
            PatternSet.Add('J', Pattern.Parse("nb nw nb nw wb ww wb nw nb"));
            PatternSet.Add('K', Pattern.Parse("wb nw nb nw nb nw nb ww wb"));
            PatternSet.Add('L', Pattern.Parse("nb nw wb nw nb nw nb ww wb"));
            PatternSet.Add('M', Pattern.Parse("wb nw wb nw nb nw nb ww nb"));
            PatternSet.Add('N', Pattern.Parse("nb nw nb nw wb nw nb ww wb"));
            PatternSet.Add('O', Pattern.Parse("wb nw nb nw wb nw nb ww nb"));
            PatternSet.Add('P', Pattern.Parse("nb nw wb nw wb nw nb ww nb"));
            PatternSet.Add('Q', Pattern.Parse("nb nw nb nw nb nw wb ww wb"));
            PatternSet.Add('R', Pattern.Parse("wb nw nb nw nb nw wb ww nb"));
            PatternSet.Add('S', Pattern.Parse("nb nw wb nw nb nw wb ww nb"));
            PatternSet.Add('T', Pattern.Parse("nb nw nb nw wb nw wb ww nb"));
            PatternSet.Add('U', Pattern.Parse("wb ww nb nw nb nw nb nw wb"));
            PatternSet.Add('V', Pattern.Parse("nb ww wb nw nb nw nb nw wb"));
            PatternSet.Add('W', Pattern.Parse("wb ww wb nw nb nw nb nw nb"));
            PatternSet.Add('X', Pattern.Parse("nb ww nb nw wb nw nb nw wb"));
            PatternSet.Add('Y', Pattern.Parse("wb ww nb nw wb nw nb nw nb"));
            PatternSet.Add('Z', Pattern.Parse("nb ww wb nw wb nw nb nw nb"));
            PatternSet.Add('-', Pattern.Parse("nb ww nb nw nb nw wb nw wb"));
            PatternSet.Add('.', Pattern.Parse("wb ww nb nw nb nw wb nw nb"));
            PatternSet.Add(' ', Pattern.Parse("nb ww wb nw nb nw wb nw nb"));
            PatternSet.Add('*', Pattern.Parse("nb ww nb nw wb nw wb nw nb"));
            PatternSet.Add('$', Pattern.Parse("nb ww nb ww nb ww nb nw nb"));
            PatternSet.Add('/', Pattern.Parse("nb ww nb ww nb nw nb ww nb"));
            PatternSet.Add('+', Pattern.Parse("nb ww nb nw nb ww nb ww nb"));
            PatternSet.Add('%', Pattern.Parse("nb nw nb ww nb ww nb ww nb"));
        }

        public override System.Drawing.Bitmap Paint(BarcodeSettings settings, string text)
        {
            text = text.Trim('*');
            if (!IsValidText(text))
                throw new ApplicationException("The text for the barcode contained unsupported characters.");
            text = text.ToUpper();

            text = "*" + text + "*";

            int width = settings.LeftMargin + settings.RightMargin + (text.Length * 3 * settings.WideWidth) + (text.Length * 6 * settings.NarrowWidth) + (settings.BarSpacing * (text.Length - 1));
            int height = settings.BarHeight + settings.TopMargin + settings.BottomMargin;
            if (settings.IsTextShown)
                height += Convert.ToInt32(settings.Font.GetHeight()) + settings.TextPadding ;
            
            Bitmap b = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(Brushes.White, 0, 0, width, height);

#if MEASURE
            AddMeasure(settings, width, g);
#endif
            int left = settings.LeftMargin;
            foreach (char item in text.ToCharArray())
            {
                foreach (Rectangle bar in PatternSet[item].Paint(settings))
                {
                    bar.Offset(left, settings.TopMargin);
                    g.FillRectangle(Brushes.Black, bar);
                }

                left += settings.BarSpacing + (3 * settings.WideWidth) + (6 * settings.NarrowWidth);
            }

            PaintText(g, settings, text, width);

            return b;
        }

        public static Bitmap Generate(string text)
        {
            return Code3of9.Instance.Paint(new BarcodeSettings(), text);
        }

    }
}
