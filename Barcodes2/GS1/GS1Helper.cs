
namespace Barcodes2.GS1
{
    public class GS1Helper
    {
        public int CheckDigitCalculate(CodedValueCollection codes)
        {
            int total = 0;
            bool flip = true;
            for (int i = codes.Count-1; i > 0; i--)
            {
                if (flip)
                    total += codes[i] * 3;
                else
                    total += codes[i];

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
