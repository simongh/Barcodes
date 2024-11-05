using Barcodes2;

namespace Barcodes.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var def = new Writer.Definitions.RM4SCC();
            var draw = new Writer.Drawing.Bitmap();

            if (def.TryParse("BX11LT1A", out var codes))
            {
                var options = new BarcodeSettings
                {
                    TextPadding = 10,
                    BottomMargin = 10,
                    BarHeight = 12,
                    MediumHeight = 8,
                    ShortHeight = 4,
                };

                var s = draw.Create(def, codes, options);

                File.WriteAllBytes("c:\\temp\\test.bmp", s.ToArray());
            }
            else
                Console.WriteLine("invalid data");
        }
    }
}