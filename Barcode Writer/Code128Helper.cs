using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public static class Code128Helper
    {
        public const char StartVariantA = (char)153;
        public const char StartVariantB = (char)154;
        public const char StartVariantC = (char)155;

        public const char FNC1 = (char)152;
        public const char FNC2 = (char)147;
        public const char FNC3 = (char)146;
        public const char FNC4 = (char)150;

        public const char CODEA = (char)151;
        public const char CODEB = (char)150;
        public const char CODEC = (char)149;

        public const char SHIFT = (char)148;
    }
}
