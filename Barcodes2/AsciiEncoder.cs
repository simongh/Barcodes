using System;

namespace Barcodes2
{
    /// <summary>
    /// ASCII encoder used by 1D barcodes, such as Extended 3of9
    /// </summary>
    internal static class AsciiEncoder
    {
        /// <summary>
        /// Lookup the value string for a given ASCII value
        /// </summary>
        /// <param name="value">ASCII value less than 0x80</param>
        /// <returns>the encoded value string</returns>
        public static string Lookup(char value)
        {
            if (value > 127)
                throw new ArgumentException("The value to encode contained characters not supported by this barcode.");

            if (System.Text.RegularExpressions.Regex.IsMatch(value.ToString(), "[A-Z0-9 -\\.]"))
                return value.ToString();

            if (value == 0)
                return "%U";
            if (value == 64)
                return "%V";
            if (value == 96)
                return "%W";

            if (value > 0 && value < 27)
                return string.Format("${0}", (char)(value + 64));
            if (value > 32 && value < 59)
                return string.Format("/{0}", (char)(value + 32));
            if (value > 96 && value < 123)
                return string.Format("+{0}", (char)(value - 32));

            if (value > 26 && value < 32)
                return string.Format("%{0}", (char)(value + 38));
            if (value > 58 && value < 64)
                return string.Format("%{0}", (char)(value + 11));
            if (value > 90 && value < 96)
                return string.Format("%{0}", (char)(value - 16));
            if (value > 122)
                return string.Format("%{0}", (char)(value - 43));

            throw new ApplicationException("The character could not encoded.");
        }
    }
}
