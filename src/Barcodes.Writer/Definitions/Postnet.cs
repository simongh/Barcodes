using System.Collections.Generic;
using System.Linq;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Postnet : BaseDefinition
    {
        private readonly Pattern _startStop = new('*', Ascender);

        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new('0', NarrowWhite, Ascender, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Tracker);
                yield return new('1', NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Ascender);
                yield return new('2', NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Ascender);
                yield return new('3', NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Ascender, NarrowWhite, Tracker);
                yield return new('4', NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Ascender);
                yield return new('5', NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Tracker);
                yield return new('6', NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Tracker);
                yield return new('7', NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Ascender);
                yield return new('8', NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Tracker);
                yield return new('9', NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Ascender, NarrowWhite, Tracker, NarrowWhite, Tracker);
            }
        }

        public override bool IsCheckSumRequired { get; set; } = true;

        public override bool IsTextShown => false;

        protected override CodedCollection? Parse(string value)
        {
            var codes = new CodedCollection()
            {
                _startStop
            };

            foreach (var item in value)
            {
                if (item == ' ' || item == '-')
                    continue;

                var c = PatternSet.Find(item);
                if (!c.Found)
                    return null;

                codes.Add(c.Pattern);
            }

            if (IsCheckSumRequired)
                AddChecksum(codes);

            codes.Add(new(' ', NarrowWhite));
            codes.Add(_startStop);

            return codes;
        }

        private void AddChecksum(CodedCollection codes)
        {
            var total = 0;
            foreach (var c in codes.Skip(1))
            {
                total += c.Value - '0';
            }

            total = total % 10;

            codes.Add(PatternSet.Find((char)((total == 0 ? 0 : 10 - total) + '0')).Pattern);
        }
    }
}