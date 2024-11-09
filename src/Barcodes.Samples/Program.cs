namespace Barcodes.Samples
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            ReadOnlySpan<byte> s;
            s = Writer.BarcodeWriter.Cpc("A1B 2C3");
            File.WriteAllBytes("c:\\temp\\test.bmp", s.ToArray());
        }
    }
}