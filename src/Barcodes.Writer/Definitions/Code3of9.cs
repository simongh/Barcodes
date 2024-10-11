using Barcodes2;
using System.Collections.Generic;
using System.Linq;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Code3of9 : BaseDefinition
    {
        public override IEnumerable<(char, Pattern)> PatternSet
        {
            get
            {
                yield return ('0', new(NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return ('1', new(WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('2', new(NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('3', new(WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('4', new(NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('5', new(WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('6', new(NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('7', new(NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack));
                yield return ('8', new(WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return ('9', new(NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return ('A', new(WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('B', new(NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('C', new(WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('D', new(NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('E', new(WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('F', new(NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('G', new(NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack));
                yield return ('H', new(WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return ('I', new(NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return ('J', new(NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return ('K', new(WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack));
                yield return ('L', new(NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack));
                yield return ('M', new(WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack));
                yield return ('N', new(NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, WideBlack));
                yield return ('O', new(WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack));
                yield return ('P', new(NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack));
                yield return ('Q', new(NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, WideBlack));
                yield return ('R', new(WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack));
                yield return ('S', new(NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack));
                yield return ('T', new(NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, WideWhite, NarrowBlack));
                yield return ('U', new(WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('V', new(NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('W', new(WideBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('X', new(NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack));
                yield return ('Y', new(WideBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('Z', new(NarrowBlack, WideWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('-', new(NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack));
                yield return ('.', new(WideBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return (' ', new(NarrowBlack, WideWhite, WideBlack, NarrowWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return ('*', new(NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, WideBlack, NarrowWhite, WideBlack, NarrowWhite, NarrowBlack));
                yield return ('$', new(NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack));
                yield return ('/', new(NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack));
                yield return ('+', new(NarrowBlack, WideWhite, NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack));
                yield return ('%', new(NarrowBlack, NarrowWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack, WideWhite, NarrowBlack));
            }
        }

        public override bool IsTextShown => true;

        public override int CalculateWidth(BarcodeSettings settings, CodedCollection codes)
        {
            return codes.Count * ((3 * settings.WideWidth) + (6 * settings.NarrowWidth) + settings.ModulePadding);
        }

        public override string GetDisplayText(string value)
        {
            if (!value.StartsWith("*"))
                value = '*' + value;

            if (!value.EndsWith("*"))
                value = value + '*';

            return value.ToUpper();
        }

        protected override void Transform(CodedCollection codes)
        {
            var guard = PatternSet.First(p => p.Item1 == '*').Item2;

            codes.Insert(0, guard);
            codes.Add(guard);
        }
    }
}