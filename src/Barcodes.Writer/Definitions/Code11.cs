using System.Collections.Generic;
using System.Linq;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Code11 : BaseDefinition
    {
        private readonly Pattern _limit = new('s', NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack);

        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new('0', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('1', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('2', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('3', WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('4', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack);
                yield return new('5', WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('6', NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('7', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack);
                yield return new('8', WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('9', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('-', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
            }
        }

        public override bool IsTextShown => true;

        protected override CodedCollection? Parse(string value)
        {
            var space = new Pattern(' ', NarrowWhite);

            var result = new CodedCollection()
            {
                _limit,
            };

            foreach (var c in value)
            {
                if (c == '*')
                    continue;

                var v = PatternSet.Find(c);

                if (!v.Found)
                    return null;

                result.AddSpace(space);
                result.Add(v.Pattern);
            }

            result.Value = value;
            result.AddSpace(space);

            if (IsCheckSumRequired)
            {
                DoChecksumCalculation(10, result);

                if (value.Length >= 10)
                {
                    result.AddSpace(space);
                    DoChecksumCalculation(9, result);
                }
            }

            result.AddSpace(space);
            result.Add(_limit);

            return result;
        }

        private void DoChecksumCalculation(int factor, CodedCollection codes)
        {
            var values = codes.Where(p => p.Value != ' ' && p.Value != _limit.Value).ToArray();

            int tmp = 0, length = values.Length;

            for (int i = 0; i < length; i++)
            {
                int weight = (length - i) % factor;
                if (weight == 0)
                    weight = factor;

                var v = values[i].Value;
                tmp += (v == '-' ? 10 : v - '0') * weight;
            }

            tmp = tmp % 11;
            var cs = PatternSet.Find(tmp > 9 ? '-' : (char)(tmp + '0')).Pattern;

            codes.Add(cs);
            codes.Value += cs.Value;
        }
    }
}