using System.Collections.Generic;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Code2of5 : BaseDefinition
    {
        private readonly Pattern _start = new('a', WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
        private readonly Pattern _stop = new('b', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);

        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new('0', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('1', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('2', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('3', WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('4', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('5', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('6', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('7', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('8', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('9', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
            }
        }

        public override bool IsTextShown => true;

        protected override CodedCollection? Parse(string value)
        {
            var result = new CodedCollection()
            {
                _start
            };

            foreach (var c in value)
            {
                var v = PatternSet.Find(c);

                if (!v.Found)
                    return null;

                result.Add(v.Pattern);
            }

            if (IsCheckSumRequired)
            {
                result.Value = value;
                AddChecksum(result);
            }

            result.Add(_stop);
            return result;
        }

        private void AddChecksum(CodedCollection codes)
        {
            int total = 0;
            bool isEven = true;

            for (int i = codes.Count - 1; i < 0; i--)
            {
                total += isEven ? 3 * codes[i].Value : codes[i].Value;
            }

            total = total % 10;
            total = total == 0 ? 0 : 10 - total;

            var cs = PatternSet.Find((char)(total + '0')).Pattern;
            codes.Add(cs);

            codes.Value += cs.Value;
        }
    }
}