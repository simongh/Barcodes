using System.Collections.Generic;
using System.Linq;
using static Barcodes.Writer.Code128Helper;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Code128 : BaseDefinition
    {
        private readonly Pattern _stop = new(106, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack);

        public Code128()
        {
            IsCheckSumRequired = true;
        }

        public override IEnumerable<Pattern> PatternSet
        {
            get
            {
                yield return new(0, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(1, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(2, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(3, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(4, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(5, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(6, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(7, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(8, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(9, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);

                yield return new(10, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(11, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(12, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(13, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(14, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(15, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(16, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(17, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(18, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(19, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);

                yield return new(20, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(21, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(22, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(23, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(24, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(25, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(26, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(27, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(28, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(29, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);

                yield return new(30, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(31, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(32, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(33, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(34, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(35, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(36, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(37, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(38, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(39, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);

                yield return new(40, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(41, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(42, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(43, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(44, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(45, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(46, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(47, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(48, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(49, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);

                yield return new(50, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(51, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(52, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(53, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(54, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(55, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(56, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(57, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(58, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(59, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);

                yield return new(60, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(61, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(62, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(63, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(64, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(65, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(66, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(67, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(68, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(69, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite);

                yield return new(70, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(71, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(72, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(73, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(74, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(75, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(76, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(77, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(78, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(79, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite);

                yield return new(80, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(81, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(82, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(83, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(84, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(85, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(86, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(87, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(88, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(89, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);

                yield return new(90, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(91, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(92, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(93, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(94, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(95, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(96, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(97, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(98, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite);
                yield return new(99, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);

                yield return new(100, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(101, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(102, NarrowBlack, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite);
                yield return new(103, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite);
                yield return new(104, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowWhite, NarrowWhite);
                yield return new(105, NarrowBlack, NarrowBlack, NarrowWhite, NarrowBlack, NarrowWhite, NarrowWhite, NarrowBlack, NarrowBlack, NarrowBlack, NarrowWhite, NarrowWhite);
            }
        }

        public override bool IsTextShown => true;

        public CodedCollection? Parse(IEnumerable<byte> value)
        {
            return Parse(value.Cast<char>());
        }

        protected override CodedCollection? Parse(string value)
        {
            return Parse(value.ToCharArray());
        }

        private CodedCollection? Parse(IEnumerable<char> value)
        {
            var codes = new CodedCollection();
            var (variant, start) = AddStart(value, codes);

            var shifted = false;
            for (var i = start; i < value.Count(); i++)
            {
                var c = value.ElementAt(i);

                if (c == AiMarker)
                    continue;

                variant = ShiftVariant(variant, shifted);

                Pattern? p;
                if (variant == CODEA)
                {
                    p = FindVariantA(c);
                    if (c == CODEA || c == CODEC)
                        variant = c;
                }
                else if (variant == CODEB)
                {
                    p = FindVariantB(c);
                    if (c == CODEA || c == CODEC)
                        variant = c;
                }
                else
                {
                    if (c == CODEA || c == CODEB)
                    {
                        variant = c;
                        p = Find(c);
                    }
                    else if (c == FNC1)
                        p = Find(c);
                    else if (value.Count() - 1 <= i)
                        return null;
                    else
                    {
                        p = FindVariantC(c, value.ElementAt(i + 1));
                        i++;
                    }
                }

                if (p == null)
                    return null;
                else
                    codes.Add(p.Value);

                variant = ShiftVariant(variant, shifted);
                shifted = variant != CODEC && c == SHIFT;
            }

            AddCheckSum(codes);
            codes.Add(_stop);
            return codes;
        }

        private (char Variant, int Start) AddStart(IEnumerable<char> value, CodedCollection codes)
        {
            char c = value.First();
            char variant = StartVariantB;
            int start = 1;
            if (c != StartVariantA && c != StartVariantB && c != StartVariantC)
            {
                if (c < ' ')
                    c = StartVariantA;
                else if (value.All(char.IsDigit))
                    c = StartVariantC;

                start = 0;
            }

            if (c == StartVariantA)
            {
                variant = CODEA;
                codes.Add(Find(StartVariantA));
            }
            else if (c == StartVariantB)
            {
                variant = CODEB;
                codes.Add(Find(StartVariantB));
            }
            else if (c == StartVariantC)
            {
                codes.Add(Find(StartVariantC));
                variant = CODEC;
            }

            return (variant, start);
        }

        private char ShiftVariant(char variant, bool shifted)
        {
            if (!shifted)
                return variant;

            if (variant == CODEA)
                return CODEB;
            else
                return CODEA;
        }

        private Pattern? FindVariantA(char value)
        {
            if (value > 31 && value < 96)
                return Find(value - 32);

            if (value >= 0 && value < 32)
                return Find(value + 64);

            if (value >= FNC3 && value <= FNC1)
                return Find(value);

            return null;
        }

        private Pattern? FindVariantB(char value)
        {
            if (value > 31 && value <= 127)
                return Find(value - 32);

            if (value >= FNC3 && value <= FNC1)
                return Find(value);

            return null;
        }

        private Pattern? FindVariantC(char a, char b)
        {
            var i = ((a - '0') * 10) + b - '0';
            return Find(i);
        }

        private Pattern Find(int value) => PatternSet.First(p => p.Value == (char)(value));

        private void AddCheckSum(CodedCollection codes)
        {
            if (!IsCheckSumRequired)
                return;

            var total = 0;

            for (var i = 0; i < codes.Count; i++)
            {
                total += codes[i].Value * (i == 0 ? 1 : i);
            }

            codes.Add(Find(total % 103));
        }
    }
}