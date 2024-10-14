using System;

namespace Barcodes.Writer
{
    internal static class AsciiEncoder
    {
        public static char[] Lookup(char value)
        {
            if (value > 127)
                throw new ArgumentException("The value to encode contained characters not supported by this barcode.");

            if (char.IsLetter(value) && char.IsUpper(value))
                return [value];

            if (char.IsDigit(value))
                return [value];

            if (value == ' ' || value == '-' || value == '.')
                return [value];

            if (value == 0)
                return ['%', 'U'];

            if (value == 64)
                return ['%', 'V'];

            if (value == 96)
                return ['%', 'W'];

            if (value > 0 && value < 27)
                return ['$', (char)(value + 64)];

            if (value > 32 && value < 59)
                return ['/', (char)(value + 32)];
            if (value > 96 && value < 123)
                return ['+', (char)(value - 32)];

            if (value > 26 && value < 32)
                return ['%', (char)(value + 38)];
            if (value > 58 && value < 64)
                return ['%', (char)(value + 11)];
            if (value > 90 && value < 96)
                return ['%', (char)(value - 16)];
            if (value > 122)
                return ['%', (char)(value - 43)];

            throw new ApplicationException("The character could not encoded.");
        }
    }
}