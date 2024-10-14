using static Barcodes.Writer.Element;

namespace Barcodes.Writer
{
    public struct Pattern
    {
        public char Value { get; private set; }

        public Element[] Elements { get; private set; }

        public int WhiteCount { get; private set; }

        public int BlackCount { get; private set; }

        public int WideCount { get; private set; }

        public int NarrowCount { get; private set; }

        public Pattern(char value, params Element[] elements)
        {
            Value = value;
            Elements = elements;

            foreach (var item in elements)
            {
                if ((item & White) == White)
                    WhiteCount++;
                else
                    BlackCount++;
                if ((item & Wide) == Wide)
                    WideCount++;
                else
                    NarrowCount++;
            }
        }
    }
}