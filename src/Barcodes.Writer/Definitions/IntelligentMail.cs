using System;
using System.Collections.Generic;
using System.Numerics;

namespace Barcodes.Writer.Definitions
{
    public class IntelligentMail : BaseDefinition
    {
        public override IEnumerable<Pattern> PatternSet => throw new NotImplementedException();

        public class Encoder
        {
            private BigInteger _data;
            private int _fcs;

            public void Parse(string value)
            {
                _data = Convert(value);

                var raw = ToByteArray();
                var fcs = CRC11(raw);
            }

            private BigInteger Convert(string value)
            {
                var data = ConvertRoutingCode(value.Substring(20));

                for (int i = 0; i < 20; i++)
                {
                    data = (data * (i == 1 ? 5 : 10)) + value[i] - '0';
                }

                return data;
            }

            public byte[] ToByteArray()
            {
                var result = new byte[13];
                var tmp = _data.ToByteArray();
                for (int i = 0; i < tmp.Length; i++)
                {
                    result[12 - i] = tmp[i];
                }

                return result;
            }

            private BigInteger ConvertRoutingCode(string value)
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

            public int CRC11(byte[] dataArray)
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

                _fcs = FrameCheckSequence;
                return FrameCheckSequence;
            }

            public int[] BaseShift()
            {
                var result = new int[10];

                BigInteger q = _data;
                for (int i = 9; i > 0; i--)
                {
                    q = BigInteger.DivRem(q, (i == 9 ? 636 : 1365), out var r);
                    result[i] = (int)r;
                }

                result[0] = (int)q + ((_fcs & 0x400) != 0 ? 659 : 0);
                result[9] = result[9] * 2;

                return result;
            }

            private int Reverse(int input)
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

            private short[] InitialiseNof13Table(int n, int length)
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

            public void ConvertToCharacters(int[] data)
            {
                var t5 = InitialiseNof13Table(5, 1287);
                var t2 = InitialiseNof13Table(2, 78);

                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] > 1364)
                        throw new ApplicationException("Invalid value found during conversion.");
                    else if (data[i] > 1286)
                        data[i] = t2[data[i] - 1287];
                    else
                        data[i] = t5[data[i]];

                    if ((_fcs & (1 << i)) != 0)
                        data[i] ^= 0x1fff;
                }
            }
        }
    }
}