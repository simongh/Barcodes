namespace Barcodes.Writer
{
    public static class Code128Helper
    {
        /// <summary>
        /// Start value for a type A code
        /// </summary>
        public const char StartVariantA = (char)103;

        /// <summary>
        /// Start value for a type B code
        /// </summary>
        public const char StartVariantB = (char)104;

        /// <summary>
        /// Start value for a type C code
        /// </summary>
        public const char StartVariantC = (char)105;

        /// <summary>
        /// Indicate following code in a Uniform Code Council code
        /// </summary>
        public const char FNC1 = (char)102;

        /// <summary>
        /// Indicate following code in a Uniform Code Council code
        /// </summary>
        public const char FNC2 = (char)97;

        /// <summary>
        /// Indicate following code in a Uniform Code Council code
        /// </summary>
        public const char FNC3 = (char)96;

        /// <summary>
        /// Indicate following code in a Uniform Code Council code
        /// </summary>
        public const char FNC4 = (char)100;

        /// <summary>
        /// Shift the code into variant A
        /// </summary>
        public const char CODEA = (char)101;

        /// <summary>
        /// Shift the code into variant B
        /// </summary>
        public const char CODEB = (char)100;

        /// <summary>
        /// Shift the code into variant C
        /// </summary>
        public const char CODEC = (char)99;

        /// <summary>
        /// Shifts the next value between A & B
        /// </summary>
        public const char SHIFT = (char)98;

        public const char AiMarker = (char)106;
    }
}