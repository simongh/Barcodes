using System.Collections.Generic;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Interleaved2of5 : BaseDefinition
    {
        private readonly Pattern _startMarker = new(' ', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
        private readonly Pattern _endMarker = new(' ', WideBlack, NarrowWhite, NarrowBlack);

        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new('0', Narrow, Narrow, Wide, Wide, Narrow);
                yield return new('1', Wide, Narrow, Narrow, Narrow, Wide);
                yield return new('2', Narrow, Wide, Narrow, Narrow, Wide);
                yield return new('3', Wide, Wide, Narrow, Narrow, Narrow);
                yield return new('4', Narrow, Narrow, Wide, Narrow, Wide);
                yield return new('5', Wide, Narrow, Wide, Narrow, Narrow);
                yield return new('6', Narrow, Wide, Wide, Narrow, Narrow);
                yield return new('7', Narrow, Narrow, Narrow, Wide, Wide);
                yield return new('8', Wide, Narrow, Narrow, Wide, Narrow);
                yield return new('9', Narrow, Wide, Narrow, Wide, Narrow);
            }
        }

        protected override CodedCollection? Parse(string value)
        {
            if (value.Length % 2 == 1)
                value = '0' + value;

            var result = new CodedCollection
            {
                _startMarker
            };

            var elements = new List<Element>();
            for (int i = 0; i < value.Length; i += 2)
            {
                elements.Clear();

                var blacks = PatternSet.Find(value[i]);
                var whites = PatternSet.Find(value[i + 1]);
                if (!blacks.Found || !whites.Found)
                    return null;

                for (int j = 0; j < 5; j++)
                {
                    elements.Add(blacks.Pattern.Elements[j] | Black);
                    elements.Add(whites.Pattern.Elements[j] | White);
                }

                result.Add(new(' ', elements.ToArray()));
            }

            result.Add(_endMarker);

            return result;
        }
    }
}