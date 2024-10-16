using System.Collections.Generic;
using System.Linq;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Codabar : BaseDefinition
    {
        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new('0', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack);
                yield return new('1', NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack);
                yield return new('2', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('3', WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('4', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('5', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('6', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('7', NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('8', NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new('9', WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack);

                yield return new('-', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('$', NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack);
                yield return new(':', WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack);
                yield return new('/', WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('.', WideBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack);
                yield return new('+', NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack);

                yield return new('A', NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('B', NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('C', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, WideBlack);
                yield return new('D', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, WideWhite, NarrowBlack);
                yield return new('T', NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack);
                yield return new('N', NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack);
                yield return new('*', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, WideBlack);
                yield return new('E', NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, WideWhite, NarrowBlack);
            }
        }

        public override bool IsTextShown => true;

        protected override CodedCollection? Parse(string value)
        {
            var result = new CodedCollection();

            var space = new Pattern(' ', NarrowWhite);
            var ended = false;

            foreach (var item in value)
            {
                if (item == ' ')
                    continue;

                var v = PatternSet.Find(char.ToUpper(item));
                if (!v.Found)
                    return null;

                if (result.Count == 0 && !IsLimit(item))
                    return null;
                else if (ended)
                    return null;
                else if (result.Count > 0 && IsLimit(item))
                    ended = true;

                result.AddSpace(space);
                result.Add(v.Pattern);
            }

            if (IsCheckSumRequired && result.All(p => char.IsDigit(p.Value) || IsLimit(p.Value)))
            {
                result.AddSpace(space);
                result.Add(AddChecksum(result));
            }
            return result;
        }

        private bool IsLimit(char value) => char.IsLetter(value) || value == '*';

        private Pattern AddChecksum(CodedCollection codes)
        {
            var total = 0;
            var i = 0;

            foreach (var item in codes)
            {
                if (item.Value == ' ')
                    continue;

                if (i % 2 == 0)
                    total += item.Value - 48;
                else
                {
                    total += ((item.Value - 48) * 2) % 9;
                }
                i++;
            }

            total = total % 10;

            return PatternSet.Find((char)(total + 48)).Pattern;
        }

        public override string GetDisplayText(string value) => value.ToUpper();
    }
}