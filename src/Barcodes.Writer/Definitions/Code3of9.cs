using System.Collections.Generic;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Code3of9 : BaseDefinition
    {
        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new('0', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('1', WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('2', NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('3', WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('4', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('5', WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('6', NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('7', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack);
                yield return new('8', WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('9', NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('A', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('B', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('C', WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('D', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('E', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('F', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('G', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack);
                yield return new('H', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('I', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('J', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('K', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack);
                yield return new('L', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack);
                yield return new('M', WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('N', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack);
                yield return new('O', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('P', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('Q', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack);
                yield return new('R', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack);
                yield return new('S', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack);
                yield return new('T', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack);
                yield return new('U', WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('V', NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('W', WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('X', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('Y', WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('Z', NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('-', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack);
                yield return new('.', WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new(' ', NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('*', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('$', NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('/', NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('+', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('%', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack);
            }
        }

        public override bool IsTextShown => true;

        public override bool UseModulePadding => true;

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
            var guard = PatternSet.Find('*').Pattern;
            var space = new Pattern(' ', NarrowWhite, NarrowWhite);

            var result = new CodedCollection
            {
                guard,
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
                result.Add(space);
            }

            result.Add(guard);

            return result;
        }
    }
}