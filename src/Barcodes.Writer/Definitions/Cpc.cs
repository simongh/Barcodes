using System;
using System.Collections.Generic;
using System.Linq;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer.Definitions
{
    public class Cpc : BaseDefinition
    {
        public override IEnumerable<Pattern> PatternSet => throw new NotSupportedException("This symbology does use lookups");

        protected override CodedCollection? Parse(string value)
        {
            var items = value.Where(c => c != ' ').ToArray();
            if (items.Length != 6)
                return null;

            var codes = new CodedCollection
            {
                CreatePattern(Pair(items[0], items[1])),
                CreatePattern(Lookup.FirstOrDefault(v => v.Item1 == items[2]).Item2),
                CreatePattern(Lookup.FirstOrDefault(v => v.Item1 == items[3]).Item2),
                CreatePattern(Pair(items[4], items[5]))
            };

            if (codes.Any(c => c.Value == 0x0))
                return null;

            codes.Add(new(0xff, NarrowBlack));

            if (codes.Sum(c => c.BlackCount) % 2 == 0)
            {
                codes.Insert(0, new(0xfe, NarrowBlack, NarrowWhite));
            }

            return codes;
        }

        private Pattern CreatePattern(byte value)
        {
            var pattern = new Element[16];
            var mask = 0x80;

            for (int j = 0; j < 8; j++)
            {
                pattern[j * 2] = (value & mask) == 0 ? NarrowWhite : NarrowBlack;
                pattern[j * 2 + 1] = NarrowWhite;
                mask >>= 1;
            }

            return new(value, pattern);
        }

        private IEnumerable<(char, byte)> Lookup
        {
            get
            {
                yield return ('L', 0x02);
                yield return ('K', 0x03);
                yield return ('M', 0x04);
                yield return ('R', 0x05);
                yield return ('J', 0x06);
                yield return ('A', 0x07);
                yield return ('H', 0x08);
                yield return ('G', 0x09);
                yield return ('S', 0x0A);
                yield return ('C', 0x0B);
                yield return ('B', 0x0C);
                yield return ('E', 0x0D);
                yield return ('Y', 0x0E);
                yield return ('V', 0x11);
                yield return ('X', 0x13);
                yield return ('T', 0x14);
                yield return ('N', 0x16);
                yield return ('W', 0x18);
                yield return ('Z', 0x1A);
                yield return ('P', 0x1C);

                yield return ('1', 0x2);
                yield return ('3', 0x3);
                yield return ('5', 0x5);
                yield return ('6', 0x6);
                yield return ('7', 0x7);
                yield return ('2', 0x9);
                yield return ('0', 0xA);
                yield return ('4', 0xB);
                yield return ('8', 0xD);
                yield return ('9', 0xE);
            }
        }

        private byte Pair(char first, char second)
        {
            if (first == 'X')
            {
                return second switch
                {
                    '0' => 0x11,
                    '1' => 0x14,
                    '2' => 0x1c,
                    '3' => 0x41,
                    '4' => 0x44,
                    '5' => 0x4c,
                    '6' => 0xc1,
                    '7' => 0xc4,
                    '8' => 0xcc,
                    '9' => 0x84,
                };
            }

            var ho = Lookup.FirstOrDefault(v => v.Item1 == first).Item2;
            var lo = Lookup.FirstOrDefault(v => v.Item1 == second).Item2;

            if (ho > 0x0 && ho < 0x10)
                return (byte)((ho << 4) | lo);
            else if (ho == 0x11)
                return (byte)(0x10 | lo);
            else if ((ho & 0x2) == 0x2)
            {
                return (byte)((lo << 4) | ((ho & 0x4) >> 2));
            }
            else if ((ho | 0xC) != 0)
                return (byte)((lo << 4) | ho & 0xC);
            else
                return 0x0;
        }
    }
}