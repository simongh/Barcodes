using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public class Code3of9 : BarcodeBase
    {
        internal readonly static Code3of9 Instance;

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

            PatternSet.Add('0', Pattern.Parse("nb nw nb ww wb nw wb nw b"));
            PatternSet.Add('1', Pattern.Parse("wb nw nb ww nb nw nb nw b"));
            PatternSet.Add('2', Pattern.Parse("nb nw wb ww nb nw nb nw b"));
            PatternSet.Add('3', Pattern.Parse("wb nw wb ww nb nw nb nw b"));
            PatternSet.Add('4', Pattern.Parse("nb nw nb ww wb nw nb nw b"));
            PatternSet.Add('5', Pattern.Parse("wb nw nb ww wb nw nb nw b"));
            PatternSet.Add('6', Pattern.Parse("nb nw wb ww wb nw nb nw b"));
            PatternSet.Add('7', Pattern.Parse("nb nw nb ww nb nw wb nw b"));
            PatternSet.Add('8', Pattern.Parse("wb nw nb ww nb nw wb nw b"));
            PatternSet.Add('9', Pattern.Parse("nb nw wb ww nb nw wb nw b"));
            PatternSet.Add('A', Pattern.Parse("wb nw nb nw nb ww nb nw b"));
            PatternSet.Add('B', Pattern.Parse("nb nw wb nw nb ww nb nw b"));
            PatternSet.Add('C', Pattern.Parse("wb nw wb nw nb ww nb nw b"));
            PatternSet.Add('D', Pattern.Parse("nb nw nb nw wb ww nb nw b"));
            PatternSet.Add('E', Pattern.Parse("wb nw nb nw wb ww nb nw b"));
            PatternSet.Add('F', Pattern.Parse("nb nw wb nw wb ww nb nw b"));
            PatternSet.Add('G', Pattern.Parse("nb nw nb nw nb ww wb nw b"));
            PatternSet.Add('H', Pattern.Parse("wb nw nb nw nb ww wb nw b"));
            PatternSet.Add('I', Pattern.Parse("nb nw wb nw nb ww wb nw b"));
            PatternSet.Add('J', Pattern.Parse("nb nw nb nw wb ww wb nw b"));
            PatternSet.Add('K', Pattern.Parse("wb nw nb nw nb nw nb ww b"));
            PatternSet.Add('L', Pattern.Parse("nb nw wb nw nb nw nb ww b"));
            PatternSet.Add('M', Pattern.Parse("wb nw wb nw nb nw nb ww b"));
            PatternSet.Add('N', Pattern.Parse("nb nw nb nw wb nw nb ww b"));
            PatternSet.Add('O', Pattern.Parse("wb nw nb nw wb nw nb ww b"));
            PatternSet.Add('P', Pattern.Parse("nb nw wb nw wb nw nb ww b"));
            PatternSet.Add('Q', Pattern.Parse("nb nw nb nw nb nw wb ww b"));
            PatternSet.Add('R', Pattern.Parse("wb nw nb nw nb nw wb ww b"));
            PatternSet.Add('S', Pattern.Parse("nb nw wb nw nb nw wb ww b"));
            PatternSet.Add('T', Pattern.Parse("nb nw nb nw wb nw wb ww b"));
            PatternSet.Add('U', Pattern.Parse("wb ww nb nw nb nw nb nw b"));
            PatternSet.Add('V', Pattern.Parse("nb ww wb nw nb nw nb nw b"));
            PatternSet.Add('W', Pattern.Parse("wb ww wb nw nb nw nb nw b"));
            PatternSet.Add('X', Pattern.Parse("nb ww nb nw wb nw nb nw b"));
            PatternSet.Add('Y', Pattern.Parse("wb ww nb nw wb nw nb nw b"));
            PatternSet.Add('Z', Pattern.Parse("nb ww wb nw wb nw nb nw b"));
            PatternSet.Add('-', Pattern.Parse("nb ww nb nw nb nw wb nw b"));
            PatternSet.Add('.', Pattern.Parse("wb ww nb nw nb nw wb nw b"));
            PatternSet.Add(' ', Pattern.Parse("nb ww wb nw nb nw wb nw b"));
            PatternSet.Add('*', Pattern.Parse("nb ww nb nw wb nw wb nw b"));
            PatternSet.Add('$', Pattern.Parse("nb ww nb ww nb ww nb nw b"));
            PatternSet.Add('/', Pattern.Parse("nb ww nb ww nb nw nb ww b"));
            PatternSet.Add('+', Pattern.Parse("nb ww nb nw nb ww nb ww b"));
            PatternSet.Add('%', Pattern.Parse("nb nw nb ww nb ww nb ww b"));
        }

        public override System.Drawing.Bitmap Paint(BarcodeSettings settings, string text)
        {
            text = text.Trim('*');
            if (!IsValidText(text))
                throw new ApplicationException("The text for the barcode contained unsupported characters.");
            text = text.ToUpper();

            text = "*" + text + "*";

            List<Rectangle> bars = new List<Rectangle>();

            int width = settings.LeftMargin + settings.RightMargin;
            Rectangle[] charbars;
            foreach (char item in text.ToCharArray())
            {
                charbars = PatternSet[item].Paint(settings);
                width += charbars[4].Left + charbars[4].Width + settings.BarSpacing;
                bars.AddRange(charbars);
            }
            width -= settings.BarSpacing;

            int height = settings.BarHeight + settings.TopMargin + settings.BottomMargin;
            if (settings.IsTextShown)
                height += Convert.ToInt32(settings.Font.GetHeight()) + settings.TextPadding ;

            Bitmap b = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(Brushes.White, 0, 0, width, height);

            int left = settings.LeftMargin;
            Rectangle rect;
            for (int i = 0; i < bars.Count; i++)
            {
                rect = bars[i];
                rect.Offset(left, settings.TopMargin);
                g.FillRectangle(Brushes.Black, rect);

                if (i % 5 == 4)
                    left += bars[i].Left + bars[i].Width + settings.BarSpacing;
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
