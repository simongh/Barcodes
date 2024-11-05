using System.Collections.Generic;
using System.Linq;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class RM4SCC : BaseDefinition
    {
        private readonly Pattern _stop = new(')', NarrowBlack);
        private readonly Pattern _start = new('(', Ascender, NarrowWhite);

        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new('0', Tracker, NarrowWhite, Tracker, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('1', Tracker, NarrowWhite, Descender, NarrowWhite, Ascender, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('2', Tracker, NarrowWhite, Descender, NarrowWhite, NarrowBlack, NarrowWhite, Ascender, NarrowWhite);
                yield return new('3', Descender, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('4', Descender, NarrowWhite, Tracker, NarrowWhite, NarrowBlack, NarrowWhite, Ascender, NarrowWhite);
                yield return new('5', Descender, NarrowWhite, Descender, NarrowWhite, Ascender, NarrowWhite, Ascender, NarrowWhite);
                yield return new('6', Tracker, NarrowWhite, Ascender, NarrowWhite, Descender, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('7', Tracker, NarrowWhite, NarrowBlack, NarrowWhite, Tracker, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('8', Tracker, NarrowWhite, NarrowBlack, NarrowWhite, Descender, NarrowWhite, Ascender, NarrowWhite);
                yield return new('9', Descender, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, NarrowBlack, NarrowWhite);

                yield return new('A', Descender, NarrowWhite, Ascender, NarrowWhite, Descender, NarrowWhite, Ascender, NarrowWhite);
                yield return new('B', Descender, NarrowWhite, NarrowBlack, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite);
                yield return new('C', Tracker, NarrowWhite, Ascender, NarrowWhite, NarrowBlack, NarrowWhite, Descender, NarrowWhite);
                yield return new('D', Tracker, NarrowWhite, NarrowBlack, NarrowWhite, Ascender, NarrowWhite, Descender, NarrowWhite);
                yield return new('E', Tracker, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, Tracker, NarrowWhite);
                yield return new('F', Descender, NarrowWhite, Ascender, NarrowWhite, Ascender, NarrowWhite, Descender, NarrowWhite);
                yield return new('G', Descender, NarrowWhite, Ascender, NarrowWhite, NarrowBlack, NarrowWhite, Tracker, NarrowWhite);
                yield return new('H', Descender, NarrowWhite, NarrowBlack, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite);
                yield return new('I', Ascender, NarrowWhite, Tracker, NarrowWhite, Descender, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('J', Ascender, NarrowWhite, Descender, NarrowWhite, Tracker, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('K', Ascender, NarrowWhite, Descender, NarrowWhite, Descender, NarrowWhite, Ascender, NarrowWhite);
                yield return new('L', NarrowBlack, NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('M', NarrowBlack, NarrowWhite, Tracker, NarrowWhite, Descender, NarrowWhite, Ascender, NarrowWhite);
                yield return new('N', NarrowBlack, NarrowWhite, Descender, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite);
                yield return new('O', Ascender, NarrowWhite, Tracker, NarrowWhite, NarrowBlack, NarrowWhite, Descender, NarrowWhite);
                yield return new('P', Ascender, NarrowWhite, Descender, NarrowWhite, Ascender, NarrowWhite, Descender, NarrowWhite);
                yield return new('Q', Ascender, NarrowWhite, Descender, NarrowWhite, NarrowBlack, NarrowWhite, Tracker, NarrowWhite);
                yield return new('R', NarrowBlack, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Descender, NarrowWhite);
                yield return new('S', NarrowBlack, NarrowWhite, Tracker, NarrowWhite, NarrowBlack, NarrowWhite, Tracker, NarrowWhite);
                yield return new('T', NarrowBlack, NarrowWhite, Descender, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite);
                yield return new('U', Ascender, NarrowWhite, Ascender, NarrowWhite, Descender, NarrowWhite, Descender, NarrowWhite);
                yield return new('V', Ascender, NarrowWhite, NarrowBlack, NarrowWhite, Tracker, NarrowWhite, Descender, NarrowWhite);
                yield return new('W', Ascender, NarrowWhite, NarrowBlack, NarrowWhite, Descender, NarrowWhite, Tracker, NarrowWhite);
                yield return new('X', NarrowBlack, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Descender, NarrowWhite);
                yield return new('Y', NarrowBlack, NarrowWhite, Ascender, NarrowWhite, Descender, NarrowWhite, Tracker, NarrowWhite);
                yield return new('Z', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite);
            }
        }

        public override bool IsTextShown => false;

        public override bool IsCheckSumRequired { get; set; } = true;

        protected override CodedCollection? Parse(string value)
        {
            var result = new CodedCollection()
            {
                _start
            };

            foreach (var item in value)
            {
                var c = PatternSet.Find(item);
                if (!c.Found)
                    return null;

                result.Add(c.Pattern);
            }

            if (IsCheckSumRequired)
                AddChecksum(result);

            result.Add(_stop);

            return result;
        }

        private void AddChecksum(CodedCollection codes)
        {
            var values = codes.Skip(1);

            int topScore = 0, btmScore = 0;
            foreach (var item in values)
            {
                var power = 4;
                foreach (var c in item.Elements.Where(e => e != NarrowWhite))
                {
                    if (c == NarrowBlack || c == Ascender)
                    {
                        topScore += power;
                    }

                    if (c == NarrowBlack || c == Descender)
                    {
                        btmScore += power;
                    }
                    power >>= 1;
                }
            }

            var chk = ((topScore - 1) % 6 * 6) + ((btmScore - 1) % 6);
            chk = chk <= 9 ? chk + '0' : chk - 10 + 'A';
            codes.Add(PatternSet.First(c => c.Value == chk));
        }
    }
}