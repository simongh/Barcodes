using Barcodes2;
using System.Collections.Generic;
using System.Linq;

namespace Barcodes.Writer
{
    public abstract class BaseDefinition
    {
        public abstract IEnumerable<Pattern> PatternSet { get; }

        public virtual bool IsCheckSumRequired { get; } = false;

        public virtual bool IsTextShown { get; } = false;

        public virtual bool UseModulePadding { get; } = false;

        public virtual int CalculateWidth(BarcodeSettings settings, CodedCollection value)
        {
            return value.Sum(p =>
                (p.NarrowCount * settings.NarrowWidth)
                + (p.WideCount * settings.WideWidth));
        }

        public virtual string GetDisplayText(string value) => value;

        public bool TryParse(string value, out CodedCollection? codes)
        {
            codes = Parse(value);

            if (codes == null)
                return false;
            else
            {
                if (IsTextShown)
                    codes.Value = GetDisplayText(value);

                return true;
            }
        }

        protected virtual CodedCollection? Parse(string value)
        {
            var result = new CodedCollection();

            foreach (var item in value)
            {
                var p = PatternSet.FirstOrDefault(e => e.Value == item);
                if (p.Value != item)
                {
                    return null;
                }

                result.Add(p);
            }

            return result;
        }
    }
}