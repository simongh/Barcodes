using System;

namespace Barcodes.Writer
{
    [Flags]
    public enum Element
    {
        White = 0,
        Black = 1,
        Wide = 2,
        Narrow = 4,
        Tracker = 9,
        Ascender = 17,
        Descender = 33,
        Guard = 64,
        WideBlack = 3,
        WideWhite = 2,
        NarrowBlack = 5,
        NarrowWhite = 4,
    }
}