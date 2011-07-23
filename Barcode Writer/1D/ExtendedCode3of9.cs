using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcodes
{
    /// <summary>
    /// Extended Code 3 of 9 to support full 128 ASCII chars.
    /// </summary>
    public class ExtendedCode3of9 : Code3of9
    {
        protected override string ParseText(string value, CodedValueCollection codes)
        {
            StringBuilder v = new StringBuilder();
            foreach (char item in value.ToCharArray())
            {
                v.Append(AsciiEncoder.Lookup(item));
            }

            base.ParseText(v.ToString(), codes);

            return value;
        }

    }
}
