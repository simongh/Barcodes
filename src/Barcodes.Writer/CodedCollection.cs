using System.Collections.Generic;

namespace Barcodes.Writer
{
    public class CodedCollection : List<Pattern>
    {
        public string Value { get; set; }
    }
}