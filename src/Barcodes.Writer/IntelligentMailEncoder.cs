using System;
using System.Numerics;
using static Barcodes.Writer.Element;

namespace Barcodes.Writer
{
    public static class IntelligentMailEncoder
    {
        private static readonly short[] _t2 = InitialiseNof13Table(2, 78);
        private static readonly short[] _t5 = InitialiseNof13Table(5, 1287);

        public static CodedCollection Parse(string value)
        {
            var data = value.Convert();

            var fcs = data.ToDataArray().CRC11();

            return data
                .BaseShift(fcs)
                .ConvertToCharacters(fcs)
                .ConvertToBars();
        }

        private static BigInteger Convert(this string value)
        {
            var data = ConvertRoutingCode(value.Substring(20));

            for (int i = 0; i < 20; i++)
            {
                data = (data * (i == 1 ? 5 : 10)) + value[i] - '0';
            }

            return data;
        }

        private static BigInteger ConvertRoutingCode(string value)
        {
            BigInteger result;
            if (value.Length == 0)
                result = BigInteger.Zero;
            else
                result = BigInteger.Parse(value);

            if (value.Length > 4)
                result += 1;

            if (value.Length > 8)
                result += 100_000;

            if (value.Length > 10)
                result += 1_000_000_000;
            return result;
        }

        private static byte[] ToDataArray(this BigInteger data)
        {
            var result = new byte[13];
            var tmp = data.ToByteArray();
            for (int i = 0; i < tmp.Length; i++)
            {
                result[12 - i] = tmp[i];
            }

            return result;
        }

        private static int CRC11(this byte[] dataArray)
        {
            if (dataArray.Length != 13)
                throw new ArgumentException("data must be 13 bytes in length");

            const int GeneratorPolynomial = 0x0F35;
            int FrameCheckSequence = 0x7FF;

            int data = dataArray[0] << 5;
            for (int i = 2; i < 8; i++)
            {
                if (((FrameCheckSequence ^ data) & 0x400) != 0)
                    FrameCheckSequence = (FrameCheckSequence << 1) ^ GeneratorPolynomial;
                else
                    FrameCheckSequence = FrameCheckSequence << 1;

                FrameCheckSequence &= 0x7FF;
                data <<= 1;
            }

            for (int i = 1; i < dataArray.Length; i++)
            {
                data = dataArray[i] << 3;
                for (int j = 0; j < 8; j++)
                {
                    if (((FrameCheckSequence ^ data) & 0x400) != 0)
                        FrameCheckSequence = (FrameCheckSequence << 1) ^ GeneratorPolynomial;
                    else
                        FrameCheckSequence = FrameCheckSequence << 1;

                    FrameCheckSequence &= 0x7FF;
                    data <<= 1;
                }
            }

            return FrameCheckSequence;
        }

        private static int[] BaseShift(this BigInteger data, int fcs)
        {
            var result = new int[10];

            BigInteger q = data;
            for (int i = 9; i > 0; i--)
            {
                q = BigInteger.DivRem(q, (i == 9 ? 636 : 1365), out var r);
                result[i] = (int)r;
            }

            result[0] = (int)q + ((fcs & 0x400) != 0 ? 659 : 0);
            result[9] = result[9] * 2;

            return result;
        }

        private static int Reverse(int input)
        {
            int result = 0;
            for (int i = 0; i < 16; i++)
            {
                result <<= 1;
                result |= (input & 1);
                input >>= 1;
            }

            return result;
        }

        private static short[] InitialiseNof13Table(int n, int length)
        {
            short[] result = new short[length];
            int lowerIndex = 0;
            int upperIndex = length - 1;
            int bitCount;
            int r;

            for (int i = 0; i < 8192; i++)
            {
                bitCount = 0;
                for (int j = 0; j < 13; j++)
                {
                    if ((i & (1 << j)) != 0)
                        bitCount++;
                }

                if (bitCount != n)
                    continue;

                r = (Reverse(i) >> 3);
                if (r < i)
                    continue;

                if (i == r)
                {
                    result[upperIndex] = (short)i;
                    upperIndex--;
                }
                else
                {
                    result[lowerIndex] = (short)i;
                    lowerIndex++;
                    result[lowerIndex] = (short)r;
                    lowerIndex++;
                }
            }

            if (lowerIndex != upperIndex + 1)
                throw new ApplicationException("Bounds did not meet");

            return result;
        }

        private static int[] ConvertToCharacters(this int[] data, int fcs)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > 1364)
                    throw new ApplicationException("Invalid value found during conversion.");
                else if (data[i] > 1286)
                    data[i] = _t2[data[i] - 1287];
                else
                    data[i] = _t5[data[i]];

                if ((fcs & (1 << i)) != 0)
                    data[i] ^= 0x1fff;
            }

            return data;
        }

        private static CodedCollection ConvertToBars(this int[] values)
        {
            return
            [
                ChooseBar(values[7] & 0x0004, values[4] & 0x0008),
                ChooseBar(values[1] & 0x0400, values[0] & 0x0001),
                ChooseBar(values[9] & 0x1000, values[2] & 0x0100),
                ChooseBar(values[5] & 0x0020, values[6] & 0x0800),
                ChooseBar(values[8] & 0x0200, values[3] & 0x0002),
                ChooseBar(values[0] & 0x0002, values[5] & 0x1000),
                ChooseBar(values[2] & 0x0020, values[1] & 0x0100),
                ChooseBar(values[4] & 0x0010, values[9] & 0x0800),
                ChooseBar(values[6] & 0x0008, values[8] & 0x0400),
                ChooseBar(values[3] & 0x0200, values[7] & 0x0040),

                ChooseBar(values[5] & 0x0800, values[1] & 0x0010),
                ChooseBar(values[8] & 0x0020, values[2] & 0x1000),
                ChooseBar(values[9] & 0x0400, values[0] & 0x0004),
                ChooseBar(values[7] & 0x0002, values[6] & 0x0080),
                ChooseBar(values[3] & 0x0040, values[4] & 0x0200),
                ChooseBar(values[0] & 0x0008, values[8] & 0x0040),
                ChooseBar(values[6] & 0x0010, values[2] & 0x0080),
                ChooseBar(values[1] & 0x0002, values[9] & 0x0200),
                ChooseBar(values[7] & 0x0400, values[5] & 0x0004),
                ChooseBar(values[4] & 0x0001, values[3] & 0x0100),

                ChooseBar(values[6] & 0x0004, values[0] & 0x0010),
                ChooseBar(values[8] & 0x0800, values[1] & 0x0001),
                ChooseBar(values[9] & 0x0100, values[3] & 0x1000),
                ChooseBar(values[2] & 0x0040, values[7] & 0x0080),
                ChooseBar(values[5] & 0x0002, values[4] & 0x0400),
                ChooseBar(values[1] & 0x1000, values[6] & 0x0200),
                ChooseBar(values[7] & 0x0008, values[8] & 0x0001),
                ChooseBar(values[5] & 0x0100, values[9] & 0x0080),
                ChooseBar(values[4] & 0x0040, values[2] & 0x0400),
                ChooseBar(values[3] & 0x0010, values[0] & 0x0020),

                ChooseBar(values[8] & 0x0010, values[5] & 0x0080),
                ChooseBar(values[7] & 0x0800, values[1] & 0x0200),
                ChooseBar(values[6] & 0x0001, values[9] & 0x0040),
                ChooseBar(values[0] & 0x0040, values[4] & 0x0100),
                ChooseBar(values[2] & 0x0002, values[3] & 0x0004),
                ChooseBar(values[5] & 0x0200, values[8] & 0x1000),
                ChooseBar(values[4] & 0x0800, values[6] & 0x0002),
                ChooseBar(values[9] & 0x0020, values[7] & 0x0010),
                ChooseBar(values[3] & 0x0008, values[1] & 0x0004),
                ChooseBar(values[0] & 0x0080, values[2] & 0x0001),

                ChooseBar(values[1] & 0x0008, values[4] & 0x0002),
                ChooseBar(values[6] & 0x0400, values[3] & 0x0020),
                ChooseBar(values[8] & 0x0080, values[9] & 0x0010),
                ChooseBar(values[2] & 0x0800, values[5] & 0x0040),
                ChooseBar(values[0] & 0x0100, values[7] & 0x1000),
                ChooseBar(values[4] & 0x0004, values[8] & 0x0002),
                ChooseBar(values[5] & 0x0400, values[3] & 0x0001),
                ChooseBar(values[9] & 0x0008, values[0] & 0x0200),
                ChooseBar(values[6] & 0x0020, values[2] & 0x0010),
                ChooseBar(values[7] & 0x0100, values[1] & 0x0080),

                ChooseBar(values[5] & 0x0001, values[4] & 0x0020),
                ChooseBar(values[2] & 0x0008, values[0] & 0x0400),
                ChooseBar(values[6] & 0x1000, values[9] & 0x0004),
                ChooseBar(values[3] & 0x0800, values[1] & 0x0040),
                ChooseBar(values[8] & 0x0100, values[7] & 0x0200),
                ChooseBar(values[5] & 0x0010, values[0] & 0x0800),
                ChooseBar(values[1] & 0x0020, values[2] & 0x0004),
                ChooseBar(values[9] & 0x0002, values[4] & 0x1000),
                ChooseBar(values[8] & 0x0008, values[6] & 0x0040),
                ChooseBar(values[7] & 0x0001, values[3] & 0x0080),

                ChooseBar(values[4] & 0x0080, values[7] & 0x0020),
                ChooseBar(values[0] & 0x1000, values[1] & 0x0800),
                ChooseBar(values[2] & 0x0200, values[9] & 0x0001),
                ChooseBar(values[6] & 0x0100, values[5] & 0x0008),
                ChooseBar(values[3] & 0x0400, values[8] & 0x0004),
            ];
        }

        private static Pattern ChooseBar(int descender, int ascender)
        {
            if (ascender == 0 && descender == 0)
                return new Pattern('T', Tracker, NarrowWhite);

            if (ascender != 0 && descender != 0)
                return new Pattern('F', NarrowBlack, NarrowWhite);

            if (ascender != 0)
                return new Pattern('A', Ascender, NarrowWhite);

            return new Pattern('D', Descender, NarrowWhite);
        }
    }
}