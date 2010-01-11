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
        public readonly static Code128 Instance;

        private Code128()
            : base()
        { }

        static Code128()
        {
            Instance = new Code128();
        }

        protected override void Init()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add(0, ParseCode("212222"));
            PatternSet.Add(1, ParseCode("222122"));
            PatternSet.Add(2, ParseCode("222221"));
            PatternSet.Add(3, ParseCode("121223"));
            PatternSet.Add(4, ParseCode("121322"));
            PatternSet.Add(5, ParseCode("131222"));
            PatternSet.Add(6, ParseCode("122213"));
            PatternSet.Add(7, ParseCode("122312"));
            PatternSet.Add(8, ParseCode("132212"));
            PatternSet.Add(9, ParseCode("221213"));

            PatternSet.Add(10, ParseCode("221312"));
            PatternSet.Add(11, ParseCode("231212"));
            PatternSet.Add(12, ParseCode("112232"));
            PatternSet.Add(13, ParseCode("122132"));
            PatternSet.Add(14, ParseCode("122231"));
            PatternSet.Add(15, ParseCode("113222"));
            PatternSet.Add(16, ParseCode("123122"));
            PatternSet.Add(17, ParseCode("123221"));
            PatternSet.Add(18, ParseCode("223211"));
            PatternSet.Add(19, ParseCode("221132"));
            
            PatternSet.Add(20, ParseCode("221231"));
            PatternSet.Add(21, ParseCode("213212"));
            PatternSet.Add(22, ParseCode("223112"));
            PatternSet.Add(23, ParseCode("312131"));
            PatternSet.Add(24, ParseCode("311222"));
            PatternSet.Add(25, ParseCode("321122"));
            PatternSet.Add(26, ParseCode("321221"));
            PatternSet.Add(27, ParseCode("312212"));
            PatternSet.Add(28, ParseCode("322112"));
            PatternSet.Add(29, ParseCode("322211"));
            
            PatternSet.Add(30, ParseCode("212123"));
            PatternSet.Add(31, ParseCode("212321"));
            PatternSet.Add(32, ParseCode("232121"));
            PatternSet.Add(33, ParseCode("111323"));
            PatternSet.Add(34, ParseCode("131123"));
            PatternSet.Add(35, ParseCode("131321"));
            PatternSet.Add(36, ParseCode("112313"));
            PatternSet.Add(37, ParseCode("132113"));
            PatternSet.Add(38, ParseCode("132311"));
            PatternSet.Add(39, ParseCode("211313"));
            
            PatternSet.Add(40, ParseCode("231113"));
            PatternSet.Add(41, ParseCode("231311"));
            PatternSet.Add(42, ParseCode("112133"));
            PatternSet.Add(43, ParseCode("112331"));
            PatternSet.Add(44, ParseCode("132131"));
            PatternSet.Add(45, ParseCode("113123"));
            PatternSet.Add(46, ParseCode("113321"));
            PatternSet.Add(47, ParseCode("133121"));
            PatternSet.Add(48, ParseCode("313121"));
            PatternSet.Add(49, ParseCode("211331"));
            
            PatternSet.Add(50, ParseCode("231131"));
            PatternSet.Add(51, ParseCode("213113"));
            PatternSet.Add(52, ParseCode("213311"));
            PatternSet.Add(53, ParseCode("213131"));
            PatternSet.Add(54, ParseCode("311123"));
            PatternSet.Add(55, ParseCode("311321"));
            PatternSet.Add(56, ParseCode("331121"));
            PatternSet.Add(57, ParseCode("312113"));
            PatternSet.Add(58, ParseCode("312311"));
            PatternSet.Add(59, ParseCode("332111"));
            
            PatternSet.Add(60, ParseCode("314111"));
            PatternSet.Add(61, ParseCode("221411"));
            PatternSet.Add(62, ParseCode("431111"));
            PatternSet.Add(63, ParseCode("111224"));
            PatternSet.Add(64, ParseCode("111422"));
            PatternSet.Add(65, ParseCode("121124"));
            PatternSet.Add(66, ParseCode("121421"));
            PatternSet.Add(67, ParseCode("141122"));
            PatternSet.Add(68, ParseCode("141221"));
            PatternSet.Add(69, ParseCode("112214"));
            
            PatternSet.Add(70, ParseCode("112412"));
            PatternSet.Add(71, ParseCode("122114"));
            PatternSet.Add(72, ParseCode("122411"));
            PatternSet.Add(73, ParseCode("142112"));
            PatternSet.Add(74, ParseCode("142211"));
            PatternSet.Add(75, ParseCode("241211"));
            PatternSet.Add(76, ParseCode("221114"));
            PatternSet.Add(77, ParseCode("413111"));
            PatternSet.Add(78, ParseCode("241112"));
            PatternSet.Add(79, ParseCode("134111"));
            
            PatternSet.Add(80, ParseCode("111242"));
            PatternSet.Add(81, ParseCode("121142"));
            PatternSet.Add(82, ParseCode("121241"));
            PatternSet.Add(83, ParseCode("114212"));
            PatternSet.Add(84, ParseCode("124112"));
            PatternSet.Add(85, ParseCode("124211"));
            PatternSet.Add(86, ParseCode("411212"));
            PatternSet.Add(87, ParseCode("421112"));
            PatternSet.Add(88, ParseCode("421211"));
            PatternSet.Add(89, ParseCode("212141"));
            
            PatternSet.Add(90, ParseCode("214121"));
            PatternSet.Add(91, ParseCode("412121"));
            PatternSet.Add(92, ParseCode("111143"));
            PatternSet.Add(93, ParseCode("111341"));
            PatternSet.Add(94, ParseCode("131141"));
            PatternSet.Add(95, ParseCode("114113"));
            PatternSet.Add(96, ParseCode("114311"));
            PatternSet.Add(97, ParseCode("411113"));
            PatternSet.Add(98, ParseCode("411311"));
            PatternSet.Add(99, ParseCode("113141"));
            
            PatternSet.Add(100, ParseCode("114131"));
            PatternSet.Add(101, ParseCode("311141"));
            PatternSet.Add(102, ParseCode("411131"));
            PatternSet.Add(103, ParseCode("211412"));
            PatternSet.Add(104, ParseCode("211214"));
            PatternSet.Add(105, ParseCode("211232"));
            
            //STOP pattern
            PatternSet.Add(106, ParseCode("2331112"));
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

        protected override string ParseText(string value, List<int> codes)
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

            AddCheckDigit(codes);
            codes.Add(106);

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

        protected override int GetModuleWidth(BarcodeSettings settings)
        {
            return settings.NarrowWidth * 11;
        }

        protected override int GetQuietSpace(BarcodeSettings settings, int length)
        {
            //10 narrow space queit zone + 2 narrow bars for the STOP
            return (2 * 10 * settings.NarrowWidth) + (2 * settings.NarrowWidth);
        }

        protected override void OnStartCode(State state)
        {
            state.Left += 10 * state.Settings.NarrowWidth;
            base.OnStartCode(state);
        }

        public override bool IsValidData(string value)
        {
            foreach (char item in value.ToCharArray())
            {
                if (item > 127)
                    return false;
            }

            return true;
        }
    }
}
