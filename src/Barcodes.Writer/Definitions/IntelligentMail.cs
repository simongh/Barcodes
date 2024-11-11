using System;
using System.Collections.Generic;
using System.Linq;

namespace Barcodes.Writer.Definitions
{
    public class IntelligentMail : BaseDefinition
    {
        public override IEnumerable<Pattern> PatternSet => throw new NotSupportedException();

        public override bool IsCheckSumRequired { get; set; } = false;

        public override bool IsTextShown => false;

        protected override CodedCollection? Parse(string value)
        {
            if (value.Any(C => !char.IsDigit(C)))
                return null;

            return IntelligentMailEncoder.Parse(value);
        }
    }
}