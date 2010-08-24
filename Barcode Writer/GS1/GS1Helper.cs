using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class GS1Helper
    {
        public int CheckDigitCalculate(AddChecksumEventArgs e)
        {
            int total = 0;
            bool flip = true;
            for (int i = e.Codes.Count-1; i > 0; i--)
            {
                if (flip)
                    total += e.Codes[i] * 3;
                else
                    total += e.Codes[i];

                flip = !flip;
            }

            total = total % 10;
            return total == 0 ? 0 : 10 - total;
        }

        public string Normalise(string value)
        {
            if (value.Length < 13)
                value = value.PadLeft(13, '0');

            return value;
        }
    }
}
