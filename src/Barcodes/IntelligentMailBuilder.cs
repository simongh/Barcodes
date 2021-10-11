using System;
using System.Text.RegularExpressions;

namespace Barcodes
{
	public class IntelligentMailBuilder
	{
		public const int ASCENDER = 'A';
		public const int DESCENDER = 'D';
		public const int FULLBAR = 'F';
		public const int TRACKER = 'T';

		private static readonly short[] t5;
		private static readonly short[] t2;

		static IntelligentMailBuilder()
		{
			t5 = InitialiseNof13Table(5, 1287);
			t2 = InitialiseNof13Table(2, 78);
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

		public byte[] Build(string value)
		{
			value = Regex.Replace(value, "[-\\s]", "");

			var data1 = ConvertToBytes(ConvertRoutingCode(value), value);
			var fcs = Crc11(data1);

			var data2 = ConvertToCodewords(data1);

			if ((fcs & 0x400) != 0)
				data2[0] += 659;

			ConvertToCharacters(data2);
			IncludeFcs(data2, fcs);

			return ConvertToBars(data2);
		}

		private long ConvertRoutingCode(string value)
		{
			var code = value.Substring(20);

			var tmp = 0L;
			if (value.Length == 5)
				tmp = long.Parse(code) + 1;
			if (value.Length == 9)
				tmp = long.Parse(code) + 100001;
			if (value.Length == 11)
				tmp = long.Parse(code) + 1000100001;

			return tmp;
		}

		private byte[] ConvertToBytes(long routingcode, string value)
		{
			routingcode = (routingcode * 10) + value[0] - '0';
			routingcode = (routingcode * 5) + value[1] - '0';

			byte[] result = new byte[13];
			int length = 0;
			void loop(int digit)
			{
				if (length == 0)
				{
					if (digit != 0)
					{
						result[0] = (byte)digit;
						length = 1;
					}
				}
				else
				{
					for (int j = 0; j < length; j++)
					{
						digit += result[j] * 10;
						result[j] = (byte)digit;
						digit >>= 8;
					}

					if (digit != 0)
					{
						result[length++] = (byte)digit;
					}
				}
			}

			foreach (var digit in routingcode.ToString("d11"))
			{
				loop(digit - '0');
			}

			foreach (var digit in value.Substring(2, 18))
			{
				loop(digit - '0');
			}

			return result;
		}

		private int Crc11(byte[] dataArray)
		{
			const int GeneratorPolynomial = 0x0F35;
			int FrameCheckSequence = 0x7FF;

			int data = dataArray[12] << 5;
			for (int i = 2; i < 8; i++)
			{
				if (((FrameCheckSequence ^ data) & 0x400) != 0)
					FrameCheckSequence = (FrameCheckSequence << 1) ^ GeneratorPolynomial;
				else
					FrameCheckSequence = FrameCheckSequence << 1;

				FrameCheckSequence &= 0x7FF;
				data <<= 1;
			}

			for (int i = 11; i >= 0; i--)
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

		private short[] ConvertToCodewords(byte[] data)
		{
			ushort[] pairdata = new ushort[7];
			for (int i = 0; i < data.Length - 1; i += 2)
			{
				pairdata[i / 2] = BitConverter.ToUInt16(data, i);
			}
			pairdata[6] = (ushort)(data[12] & 0x3f);

			short[] result = new short[10];
			result[9] = (short)(2 * BaseShift(pairdata, 636));

			for (int i = 8; i > 0; i--)
			{
				result[i] = BaseShift(pairdata, 1365);
			}

			result[0] = (short)pairdata[0];

			return result;
		}

		private short BaseShift(ushort[] data, int basediv)
		{
			int c = 0;
			ushort res;

			for (int i = data.Length - 1; i >= 0; i--)
			{
				c = (c << 16) + data[i];
				res = (ushort)(c / basediv);
				c -= (ushort)res * basediv;

				data[i] = res;
			}

			return (short)c;
		}

		private void ConvertToCharacters(short[] data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] > 1364)
					throw new ApplicationException("Invalid value found during conversion.");
				else if (data[i] > 1286)
					data[i] = t2[data[i] - 1287];
				else
					data[i] = t5[data[i]];
			}
		}

		private void IncludeFcs(short[] data, int fcs)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if ((fcs & (1 << i)) != 0)
					data[i] ^= 0x1fff;
			}
		}

		public byte[] ConvertToBars(short[] data)
		{
			return new byte[]
			{
				ChooseBar(data[7] & 0x0004, data[4] & 0x0008),
				ChooseBar(data[1] & 0x0400, data[0] & 0x0001),
				ChooseBar(data[9] & 0x1000, data[2] & 0x0100),
				ChooseBar(data[5] & 0x0020, data[6] & 0x0800),
				ChooseBar(data[8] & 0x0200, data[3] & 0x0002),
				ChooseBar(data[0] & 0x0002, data[5] & 0x1000),
				ChooseBar(data[2] & 0x0020, data[1] & 0x0100),
				ChooseBar(data[4] & 0x0010, data[9] & 0x0800),
				ChooseBar(data[6] & 0x0008, data[8] & 0x0400),
				ChooseBar(data[3] & 0x0200, data[7] & 0x0040),

				ChooseBar(data[5] & 0x0800, data[1] & 0x0010),
				ChooseBar(data[8] & 0x0020, data[2] & 0x1000),
				ChooseBar(data[9] & 0x0400, data[0] & 0x0004),
				ChooseBar(data[7] & 0x0002, data[6] & 0x0080),
				ChooseBar(data[3] & 0x0040, data[4] & 0x0200),
				ChooseBar(data[0] & 0x0008, data[8] & 0x0040),
				ChooseBar(data[6] & 0x0010, data[2] & 0x0080),
				ChooseBar(data[1] & 0x0002, data[9] & 0x0200),
				ChooseBar(data[7] & 0x0400, data[5] & 0x0004),
				ChooseBar(data[4] & 0x0001, data[3] & 0x0100),

				ChooseBar(data[6] & 0x0004, data[0] & 0x0010),
				ChooseBar(data[8] & 0x0800, data[1] & 0x0001),
				ChooseBar(data[9] & 0x0100, data[3] & 0x1000),
				ChooseBar(data[2] & 0x0040, data[7] & 0x0080),
				ChooseBar(data[5] & 0x0002, data[4] & 0x0400),
				ChooseBar(data[1] & 0x1000, data[6] & 0x0200),
				ChooseBar(data[7] & 0x0008, data[8] & 0x0001),
				ChooseBar(data[5] & 0x0100, data[9] & 0x0080),
				ChooseBar(data[4] & 0x0040, data[2] & 0x0400),
				ChooseBar(data[3] & 0x0010, data[0] & 0x0020),

				ChooseBar(data[8] & 0x0010, data[5] & 0x0080),
				ChooseBar(data[7] & 0x0800, data[1] & 0x0200),
				ChooseBar(data[6] & 0x0001, data[9] & 0x0040),
				ChooseBar(data[0] & 0x0040, data[4] & 0x0100),
				ChooseBar(data[2] & 0x0002, data[3] & 0x0004),
				ChooseBar(data[5] & 0x0200, data[8] & 0x1000),
				ChooseBar(data[4] & 0x0800, data[6] & 0x0002),
				ChooseBar(data[9] & 0x0020, data[7] & 0x0010),
				ChooseBar(data[3] & 0x0008, data[1] & 0x0004),
				ChooseBar(data[0] & 0x0080, data[2] & 0x0001),

				ChooseBar(data[1] & 0x0008, data[4] & 0x0002),
				ChooseBar(data[6] & 0x0400, data[3] & 0x0020),
				ChooseBar(data[8] & 0x0080, data[9] & 0x0010),
				ChooseBar(data[2] & 0x0800, data[5] & 0x0040),
				ChooseBar(data[0] & 0x0100, data[7] & 0x1000),
				ChooseBar(data[4] & 0x0004, data[8] & 0x0002),
				ChooseBar(data[5] & 0x0400, data[3] & 0x0001),
				ChooseBar(data[9] & 0x0008, data[0] & 0x0200),
				ChooseBar(data[6] & 0x0020, data[2] & 0x0010),
				ChooseBar(data[7] & 0x0100, data[1] & 0x0080),

				ChooseBar(data[5] & 0x0001, data[4] & 0x0020),
				ChooseBar(data[2] & 0x0008, data[0] & 0x0400),
				ChooseBar(data[6] & 0x1000, data[9] & 0x0004),
				ChooseBar(data[3] & 0x0800, data[1] & 0x0040),
				ChooseBar(data[8] & 0x0100, data[7] & 0x0200),
				ChooseBar(data[5] & 0x0010, data[0] & 0x0800),
				ChooseBar(data[1] & 0x0020, data[2] & 0x0004),
				ChooseBar(data[9] & 0x0002, data[4] & 0x1000),
				ChooseBar(data[8] & 0x0008, data[6] & 0x0040),
				ChooseBar(data[7] & 0x0001, data[3] & 0x0080),

				ChooseBar(data[4] & 0x0080, data[7] & 0x0020),
				ChooseBar(data[0] & 0x1000, data[1] & 0x0800),
				ChooseBar(data[2] & 0x0200, data[9] & 0x0001),
				ChooseBar(data[6] & 0x0100, data[5] & 0x0008),
				ChooseBar(data[3] & 0x0400, data[8] & 0x0004),
			};
		}

		private byte ChooseBar(int descender, int ascender)
		{
			if (ascender == 0 && descender == 0)
				return TRACKER;

			if (ascender != 0 && descender != 0)
				return FULLBAR;

			if (ascender != 0)
				return ASCENDER;

			return DESCENDER;
		}
	}
}