using static Barcodes.Writer.Element;

namespace Barcodes.Writer
{
    public struct Pattern
    {
        public Element[] Elements { get; private set; }

        public int WhiteCount { get; private set; }

        public int BlackCount { get; private set; }

        public int WideCount { get; private set; }

        public int NarrowCount { get; private set; }

        public Pattern(params Element[] elements)
        {
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