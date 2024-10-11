using Barcodes2;
using System.Collections.Generic;
using System.Linq;

namespace Barcodes.Writer
{
    public abstract class BaseDefinition
    {
        public abstract IEnumerable<(char Value, Pattern Pattern)> PatternSet { get; }

        public virtual bool IsCheckSumRequired { get; } = false;

        public virtual bool IsTextShown { get; } = false;

        public virtual int CalculateWidth(BarcodeSettings settings, CodedCollection value)
        {
            return value.Sum(p =>
                (p.NarrowCount * settings.NarrowWidth)
                + (p.WideCount * settings.WideWidth))
            + (value.Count * settings.ModulePadding);
        }

        public virtual string GetDisplayText(string value) => value;

        public virtual bool TryParse(string value, out CodedCollection? codes)
        {
            var result = new CodedCollection();

            foreach (var item in PreParse(value))
            {
                var p = PatternSet.FirstOrDefault(e => e.Value == item);
                if (p.Value != item)
                {
                    codes = null;
                    return false;
                }

                result.Add(p.Pattern);
            }

            Transform(result);
            result.Value = GetDisplayText(value);

            codes = result;
            return true;
        }

        protected virtual string PreParse(string value) => value;

        protected virtual void Transform(CodedCollection codes)
        { }
    }
}