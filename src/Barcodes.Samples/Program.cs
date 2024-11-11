namespace Barcodes.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ImTest("01234567094987654321");
            ImTest("0123456709498765432101234");
            ImTest("01234567094987654321012345678");
            ImTest("0123456709498765432101234567891");
            Console.WriteLine();
            return;

            ReadOnlySpan<byte> s;
            s = Writer.BarcodeWriter.Cpc("A1B 2C3");
            File.WriteAllBytes("c:\\temp\\test.bmp", s.ToArray());
        }

        private static void ImTest(string value)
        {
            var e = new Writer.Definitions.IntelligentMail.Encoder();
            e.Parse(value);
            var data = e.ToByteArray();
            foreach (var item in data)
            {
                Console.Write("{0:X2} ", item);
            }
            Console.WriteLine();
            Console.WriteLine("{0:X3}", e.CRC11(data));

            var words = e.BaseShift();
            foreach (var item in words)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();

            e.ConvertToCharacters(words);
            foreach (var item in words)
            {
                Console.Write("{0:X4} ", item);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}