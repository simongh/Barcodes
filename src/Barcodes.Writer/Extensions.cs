using System.Collections.Generic;
using System.Linq;

namespace Barcodes.Writer
{
    internal static class Extensions
    {
        public static (Pattern Pattern, bool Found) Find(this IEnumerable<Pattern> values, char value)
        {
            var result = values.FirstOrDefault(v => v.Value == value);

            return (result, result.Value == value);
        }
    }
}