
namespace Barcodes.DataMatrix
{
    public class OutputSettings
    {
        public float Resolution
        {
            get;
            set;
        }

        public int BitSize
        {
            get;
            set;
        }

        public OutputSettings()
        {
            Resolution = 72F;
            BitSize = 2;
        }

    }
}
