using System.Collections.Generic;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Code93 : BaseDefinition
    {
        private const char Shift1 = (char)43;
        private const char Shift2 = (char)44;
        private const char Shift3 = (char)45;
        private const char Shift4 = (char)46;
        private readonly Pattern _limit = new((char)47, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
        private readonly Pattern _terminator = new((char)48, NarrowBlack);

        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new((char)0, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)1, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new((char)2, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)3, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)4, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new((char)5, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)6, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)7, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new((char)8, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)9, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)10, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new((char)11, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)12, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)13, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)14, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)15, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)16, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new((char)17, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)18, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)19, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)20, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)21, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new((char)22, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)23, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)24, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)25, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)26, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)27, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)28, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)29, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)30, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)31, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)32, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)33, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)34, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)35, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)36, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)37, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new((char)38, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)39, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new((char)40, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)41, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new((char)42, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);

                yield return new(Shift1, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(Shift2, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(Shift3, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(Shift4, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
            }
        }

        protected override CodedCollection? Parse(string value)
        {
            var result = new CodedCollection
            {
                _limit
            };

            foreach (var item in value)
            {
                int lookup;
                if (item == ' ')
                    lookup = 38;
                else if (item == '$')
                    lookup = 39;
                else if (item == '/')
                    lookup = 40;
                else if (item == '+')
                    lookup = 41;
                else if (item == '%')
                    lookup = 42;
                else if (item >= '0' && item <= '9')
                    lookup = item - 48;
                else
                {
                    var t = AsciiEncoder.Lookup(item);

                    switch (t[0])
                    {
                        case '$':
                            result.Add(PatternSet.Find(Shift1).Pattern);
                            lookup = t[1] - 55;
                            break;

                        case '%':
                            result.Add(PatternSet.Find(Shift2).Pattern);
                            lookup = t[1] - 55;
                            break;

                        case '/':
                            result.Add(PatternSet.Find(Shift3).Pattern);
                            lookup = t[1] - 55;
                            break;

                        case '+':
                            result.Add(PatternSet.Find(Shift4).Pattern);
                            lookup = t[1] - 55;
                            break;

                        default:
                            lookup = t[0] - 55;
                            break;
                    }
                }
                result.Add(PatternSet.Find((char)lookup).Pattern);
            }

            AddCheckDigit(20, result);
            AddCheckDigit(15, result);

            result.Add(_limit);
            result.Add(_terminator);

            return result;
        }

        private void AddCheckDigit(int weight, CodedCollection codes)
        {
            int total = 0;
            int w = 1;
            for (int i = codes.Count - 1; i > 0; i--)
            {
                total += (w * codes[i].Value);
                w++;
                if (w > weight)
                    w = 1;
            }

            total = total % 47;
            var cs = PatternSet.Find((char)total).Pattern;
            codes.Add(cs);
        }
    }
}