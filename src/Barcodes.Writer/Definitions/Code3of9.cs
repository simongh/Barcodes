using System.Collections.Generic;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Code3of9 : BaseDefinition
    {
        private readonly Pattern _guard = new('*', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);

        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new('0', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('1', WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('2', NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('3', WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('4', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('5', WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('6', NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('7', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('8', WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('9', NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('A', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('B', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('C', WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('D', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('E', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('F', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('G', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('H', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('I', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('J', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('K', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite);
                yield return new('L', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite);
                yield return new('M', WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite);
                yield return new('N', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite);
                yield return new('O', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite);
                yield return new('P', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite);
                yield return new('Q', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack, NarrowWhite);
                yield return new('R', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite);
                yield return new('S', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite);
                yield return new('T', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite);
                yield return new('U', WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('V', NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('W', WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('X', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('Y', WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('Z', NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('-', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite);
                yield return new('.', WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(' ', NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('$', NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new('/', NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite);
                yield return new('+', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite);
                yield return new('%', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite);
            }
        }

        public override bool IsTextShown => true;

        public override string GetDisplayText(string value)
        {
            if (!value.StartsWith("*"))
                value = '*' + value;

            if (!value.EndsWith("*"))
                value = value + '*';

            return value.ToUpper();
        }

        protected override CodedCollection? Parse(string value)
        {
            var space = new Pattern(' ', NarrowWhite);

            var result = new CodedCollection
            {
                _guard,
                space,
            };

            foreach (var item in value)
            {
                var p = PatternSet.Find(item);
                if (!p.Found)
                {
                    return null;
                }

                result.Add(p.Pattern);
            }

            result.Add(_guard);

            return result;
        }
    }
}