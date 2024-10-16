using Barcodes2;

namespace Barcodes.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            var def = new Writer.Definitions.Interleaved2of5();
            var draw = new Writer.Drawing.Bitmap();

            if (def.TryParse("602003", out var codes))
            {
                var options = new BarcodeSettings
                {
                    TextPadding = 10,
                    BottomMargin = 10
                };

                var s = draw.Create(def, codes, options);

                File.WriteAllBytes("c:\\temp\\test.bmp", s.ToArray());
            }
            else
                Console.WriteLine("invalid data");
        }
    }
}