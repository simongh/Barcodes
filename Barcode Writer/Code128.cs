//#define MEASURE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public class Code128 : BarcodeBase
    {
        internal readonly static Code128 Instance;

        protected override string AllowedChars
        {
            get { throw new NotSupportedException(); }
        }

        public override bool IsCaseSensitive
        {
            get { return true; }
        }

        private Code128()
            : base()
        { }

        static Code128()
        {
            Instance = new Code128();
        }

        public override System.Drawing.Bitmap Paint(BarcodeSettings settings, string text)
        {
            List<int> codes = new List<int>();
            text = ParseText(text, codes);
            AddCheckDigit(codes);
            codes.Add(106);

            int width = settings.LeftMargin + settings.RightMargin + (11 * codes.Count * settings.NarrowWidth) + (2 * 10 * settings.NarrowWidth) + (2 * settings.NarrowWidth);
            int height = settings.TopMargin + settings.BarHeight + settings.BottomMargin;
            if (settings.IsTextShown)
                height += Convert.ToInt32(settings.Font.GetHeight()) + settings.TextPadding;

            Bitmap b = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(Brushes.White, 0, 0, width, height);
            
#if MEASURE
            AddMeasure(settings, width, g);
#endif
            int left = settings.LeftMargin + (10 * settings.NarrowWidth);
            foreach (int item in codes)
            {
                foreach (Rectangle rect in PatternSet[(char)item].Paint(settings))
                {
                    rect.Offset(left, settings.TopMargin);
                    g.FillRectangle(Brushes.Black, rect);
                }

                left += settings.NarrowWidth * 11;
            }

            PaintText(g, settings, text, width);

            return b;
        }

        protected override void Init()
        {
            CreatePatternSet();
        }

        private void CreatePatternSet()
        {
            PatternSet = new Dictionary<char, Pattern>();

            PatternSet.Add((char)0, ParseCode("212222"));
            PatternSet.Add((char)1, ParseCode("222122"));
            PatternSet.Add((char)2, ParseCode("222221"));
            PatternSet.Add((char)3, ParseCode("121223"));
            PatternSet.Add((char)4, ParseCode("121322"));
            PatternSet.Add((char)5, ParseCode("131222"));
            PatternSet.Add((char)6, ParseCode("122213"));
            PatternSet.Add((char)7, ParseCode("122312"));
            PatternSet.Add((char)8, ParseCode("132212"));
            PatternSet.Add((char)9, ParseCode("221213"));

            PatternSet.Add((char)10, ParseCode("221312"));
            PatternSet.Add((char)11, ParseCode("231212"));
            PatternSet.Add((char)12, ParseCode("112232"));
            PatternSet.Add((char)13, ParseCode("122132"));
            PatternSet.Add((char)14, ParseCode("122231"));
            PatternSet.Add((char)15, ParseCode("113222"));
            PatternSet.Add((char)16, ParseCode("123122"));
            PatternSet.Add((char)17, ParseCode("123221"));
            PatternSet.Add((char)18, ParseCode("223211"));
            PatternSet.Add((char)19, ParseCode("221132"));
            
            PatternSet.Add((char)20, ParseCode("221231"));
            PatternSet.Add((char)21, ParseCode("213212"));
            PatternSet.Add((char)22, ParseCode("223112"));
            PatternSet.Add((char)23, ParseCode("312131"));
            PatternSet.Add((char)24, ParseCode("311222"));
            PatternSet.Add((char)25, ParseCode("321122"));
            PatternSet.Add((char)26, ParseCode("321221"));
            PatternSet.Add((char)27, ParseCode("312212"));
            PatternSet.Add((char)28, ParseCode("322112"));
            PatternSet.Add((char)29, ParseCode("322211"));
            
            PatternSet.Add((char)30, ParseCode("212123"));
            PatternSet.Add((char)31, ParseCode("212321"));
            PatternSet.Add((char)32, ParseCode("232121"));
            PatternSet.Add((char)33, ParseCode("111323"));
            PatternSet.Add((char)34, ParseCode("131123"));
            PatternSet.Add((char)35, ParseCode("131321"));
            PatternSet.Add((char)36, ParseCode("112313"));
            PatternSet.Add((char)37, ParseCode("132113"));
            PatternSet.Add((char)38, ParseCode("132311"));
            PatternSet.Add((char)39, ParseCode("211313"));
            
            PatternSet.Add((char)40, ParseCode("231113"));
            PatternSet.Add((char)41, ParseCode("231311"));
            PatternSet.Add((char)42, ParseCode("112133"));
            PatternSet.Add((char)43, ParseCode("112331"));
            PatternSet.Add((char)44, ParseCode("132131"));
            PatternSet.Add((char)45, ParseCode("113123"));
            PatternSet.Add((char)46, ParseCode("113321"));
            PatternSet.Add((char)47, ParseCode("133121"));
            PatternSet.Add((char)48, ParseCode("313121"));
            PatternSet.Add((char)49, ParseCode("211331"));
            
            PatternSet.Add((char)50, ParseCode("231131"));
            PatternSet.Add((char)51, ParseCode("213113"));
            PatternSet.Add((char)52, ParseCode("213311"));
            PatternSet.Add((char)53, ParseCode("213131"));
            PatternSet.Add((char)54, ParseCode("311123"));
            PatternSet.Add((char)55, ParseCode("311321"));
            PatternSet.Add((char)56, ParseCode("331121"));
            PatternSet.Add((char)57, ParseCode("312113"));
            PatternSet.Add((char)58, ParseCode("312311"));
            PatternSet.Add((char)59, ParseCode("332111"));
            
            PatternSet.Add((char)60, ParseCode("314111"));
            PatternSet.Add((char)61, ParseCode("221411"));
            PatternSet.Add((char)62, ParseCode("431111"));
            PatternSet.Add((char)63, ParseCode("111224"));
            PatternSet.Add((char)64, ParseCode("111422"));
            PatternSet.Add((char)65, ParseCode("121124"));
            PatternSet.Add((char)66, ParseCode("121421"));
            PatternSet.Add((char)67, ParseCode("141122"));
            PatternSet.Add((char)68, ParseCode("141221"));
            PatternSet.Add((char)69, ParseCode("112214"));
            
            PatternSet.Add((char)70, ParseCode("112412"));
            PatternSet.Add((char)71, ParseCode("122114"));
            PatternSet.Add((char)72, ParseCode("122411"));
            PatternSet.Add((char)73, ParseCode("142112"));
            PatternSet.Add((char)74, ParseCode("142211"));
            PatternSet.Add((char)75, ParseCode("241211"));
            PatternSet.Add((char)76, ParseCode("221114"));
            PatternSet.Add((char)77, ParseCode("413111"));
            PatternSet.Add((char)78, ParseCode("241112"));
            PatternSet.Add((char)79, ParseCode("134111"));
            
            PatternSet.Add((char)80, ParseCode("111242"));
            PatternSet.Add((char)81, ParseCode("121142"));
            PatternSet.Add((char)82, ParseCode("121241"));
            PatternSet.Add((char)83, ParseCode("114212"));
            PatternSet.Add((char)84, ParseCode("124112"));
            PatternSet.Add((char)85, ParseCode("124211"));
            PatternSet.Add((char)86, ParseCode("411212"));
            PatternSet.Add((char)87, ParseCode("421112"));
            PatternSet.Add((char)88, ParseCode("421211"));
            PatternSet.Add((char)89, ParseCode("212141"));
            
            PatternSet.Add((char)90, ParseCode("214121"));
            PatternSet.Add((char)91, ParseCode("412121"));
            PatternSet.Add((char)92, ParseCode("111143"));
            PatternSet.Add((char)93, ParseCode("111341"));
            PatternSet.Add((char)94, ParseCode("131141"));
            PatternSet.Add((char)95, ParseCode("114113"));
            PatternSet.Add((char)96, ParseCode("114311"));
            PatternSet.Add((char)97, ParseCode("411113"));
            PatternSet.Add((char)98, ParseCode("411311"));
            PatternSet.Add((char)99, ParseCode("113141"));
            
            PatternSet.Add((char)100, ParseCode("114131"));
            PatternSet.Add((char)101, ParseCode("311141"));
            PatternSet.Add((char)102, ParseCode("411131"));
            PatternSet.Add((char)103, ParseCode("211412"));
            PatternSet.Add((char)104, ParseCode("211214"));
            PatternSet.Add((char)105, ParseCode("211232"));
            
            //STOP pattern
            PatternSet.Add((char)106, ParseCode("2331112"));
        }

        private Pattern ParseCode(string value)
        {
            List<Elements> list = new List<Elements>();

            int i = 0;
            int tmp;
            while (i < value.Length)
            {
                tmp = int.Parse(value.Substring(i, 1));

                for (int j = 0; j < tmp; j++)
                {
                    list.Add((Elements)(i % 2) + 2);
                }
                i++;
            }

            return new Pattern(list.ToArray());
        }

        private string ParseText(string value, List<int> codes)
        {
            char variant = Code128Helper.StartVariantB;
            int i = 0;
            bool shifted = false;

            StringBuilder ParsedText = new StringBuilder();

            if (value[0] == Code128Helper.StartVariantA)
            {
                variant = Code128Helper.CODEA;
                codes.Add((int)Code128Helper.StartVariantA - 50);
                i++;
            }
            else if (value[0] == Code128Helper.StartVariantB)
            {
                variant = Code128Helper.CODEB;
                codes.Add((int)Code128Helper.StartVariantB - 50);
                i++;
            }
            else if (value[0] == Code128Helper.StartVariantC)
            {
                variant = Code128Helper.CODEC;
                codes.Add((int)Code128Helper.StartVariantC - 50);
                i++;
            }
            else
            {
                codes.Add((int)Code128Helper.StartVariantB);
            }

            int tmp;
            do
            {
                if (shifted)
                    if (variant == Code128Helper.CODEA)
                        variant = Code128Helper.CODEB;
                    else
                        variant = Code128Helper.CODEA;

                if (variant == Code128Helper.CODEA)
                {
                    tmp = EncodeCodeA(value[i], ParsedText);

                    if (value[i] == Code128Helper.CODEB || value[i] == Code128Helper.CODEC)
                        variant = value[i];

                    i++;
                }
                else if (variant == Code128Helper.CODEB)
                {
                    tmp = EncodeCodeB(value[i], ParsedText);

                    if (value[i] == Code128Helper.CODEA || value[i] == Code128Helper.CODEC)
                        variant = value[i];

                    i++;
                }
                else
                {
                    if (value[i] == Code128Helper.CODEA || value[i] == Code128Helper.CODEB)
                    {
                        tmp = (int)value[i] - 50;
                        variant = value[i];
                        i++;
                    }
                    else if (value[i] == Code128Helper.FNC1)
                    {
                        tmp = (int)value[i] - 50;
                        i++;
                    }
                    else
                    {
                        tmp = EncodeCodeC(value.Substring(i, 2), ParsedText);
                        i += 2;
                    }
                }

                codes.Add(tmp);

                if (shifted)
                {
                    if (variant == Code128Helper.CODEA)
                        variant = Code128Helper.CODEB;
                    else
                        variant = Code128Helper.CODEA;
                }

                shifted = (tmp == 98 && variant != Code128Helper.CODEC);

            } while (i < value.ToCharArray().Length);

            return ParsedText.ToString();
        }

        private int EncodeCodeA(char value, StringBuilder parsedText)
        {
            int tmp =(int)value;

            if (tmp > 31 && tmp < 96)
            {
                parsedText.Append(value);
                return tmp - 32;
            }
            if (tmp >= 0 && tmp < 32)
                return tmp + 64;

            if (tmp >= Code128Helper.FNC3 && tmp <= Code128Helper.FNC1)
                return tmp - 50;

            throw new ApplicationException("Unsupported character encountered");
        }

        private int EncodeCodeB(char value, StringBuilder parsedText)
        {
            int tmp = (int)value;

            if (tmp > 31 && tmp < 127)
            {
                parsedText.Append(value);
                return tmp - 32;
            }
            if (tmp == 127)
                return tmp - 32;

            if (value >= Code128Helper.FNC3 && value <= Code128Helper.FNC1)
                return tmp - 50;

            throw new ApplicationException("Unsupported character encountered");
        }

        private int EncodeCodeC(string value, StringBuilder parsedText)
        {
            int tmp = int.Parse(value);
            if (tmp < 0 || tmp > 99)
                throw new ApplicationException("Unexpected value encountered.");

            parsedText.Append(value);
            return tmp;
        }

        private void AddCheckDigit(List<int> values)
        {
            int total = 0;

            for (int i = 0; i < values.Count; i++)
            {
                if (i == 0)
                    total = values[i];
                else
                    total += values[i] * i;
            }

            values.Add(total % 103);
        }

        public static Bitmap Generate(string text)
        {
            return Instance.Paint(new BarcodeSettings(), text);
        }

        private void AddMeasure(BarcodeSettings settings, int width, Graphics canvas)
        {
            int left = settings.LeftMargin;
            bool alt = true;

            while (left < width)
            {
                if (alt)
                    canvas.FillRectangle(Brushes.Gainsboro, left, 0, settings.NarrowWidth, settings.TopMargin);
                left += settings.NarrowWidth;
                alt = !alt;
            }
        }
    }
}
