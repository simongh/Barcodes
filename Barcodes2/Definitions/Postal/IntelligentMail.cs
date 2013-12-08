using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes2.Definitions.Postal
{
	public class IntelligentMail : DefaultDefinition
	{
		public const int ASCENDER = 'A';
		public const int DESCENDER = 'D';
		public const int FULLBAR = 'F';
		public const int TRACKER = 'T';

		private short[] t5;
		private short[] t2;

		public IntelligentMail()
		{
			t5 = InitialiseNof13Table(5, 1287);
			t2 = InitialiseNof13Table(2, 78);
		}

		protected override System.Text.RegularExpressions.Regex GetRegex()
		{
			throw new NotImplementedException();
		}

		protected override void CreatePatternSet()
		{
			PatternSet = new Dictionary<int, Pattern>();

			PatternSet.Add(ASCENDER, Pattern.Parse("a"));
			PatternSet.Add(DESCENDER, Pattern.Parse("d"));
			PatternSet.Add(FULLBAR, Pattern.Parse("f"));
			PatternSet.Add(TRACKER, Pattern.Parse("t"));
		}

		public override bool IsDataValid(string value)
		{
			value = Regex.Replace(value, "[-\\s]", "");

			if (Regex.IsMatch(value, @"^\d[0-4]\d{3}"))
				return true;

			if (Regex.IsMatch(value, @"^\d{5}([0-8]\d{5}\d{9}|9\d{8}\d{6})"))
				return true;

			return Regex.IsMatch(value, @"^\d{20}(\d{5}(\d{4}(\d{2})?)?)?$");
		}

		public override void TransformSettings(BarcodeSettings settings)
		{
			base.TransformSettings(settings);

			settings.IsTextShown = false;
			settings.IsChecksumCalculated = false;
			settings.ModulePadding = settings.NarrowWidth;
			settings.WideWidth = 0;
			settings.ShortHeight = settings.BarHeight / 3;
			settings.MediumHeight = (settings.BarHeight / 3) * 2;
		}

		public override CodedValueCollection GetCodes(string value)
		{
			var codes = new CodedValueCollection();

			value = Regex.Replace(value, "[-\\s]", "");
			byte[] data1 = ConvertToBytes(ConvertRoutingCode(value.Substring(20)), value.Substring(0, 20));
			int fcs = CRC11(data1);

			short[] data2 = ConvertToCodewords(data1);

			if (CheckFcs(fcs))
				data2[0] += 659;

			ConvertToCharacters(data2);

			IncludeFcs(data2, fcs);

			codes.AddRange(ConvertToBars(data2));

			return codes;
		}

		/// <summary>
		/// CRC 11 calculation
		/// </summary>
		/// <param name="dataArray">data values to calculate from</param>
		/// <returns>CRC11 value</returns>
		public int CRC11(byte[] dataArray)
		{
			if (dataArray.Length != 13)
				throw new ArgumentException("data must be 13 bytes in length");

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

		/// <summary>
		/// Reverse the bit order in a 16-bit value
		/// </summary>
		/// <param name="input">input to reverse</param>
		/// <returns>reversed value</returns>
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

		/// <summary>
		/// Initialise the tables for lookup
		/// </summary>
		/// <param name="n">the bit count to use in the table</param>
		/// <param name="length">the length of the table</param>
		/// <returns>lookup table of values</returns>
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

		/// <summary>
		/// Incorporate the Frame Check Sequence into the data
		/// </summary>
		/// <param name="fcs">Frame Check Sequence</param>
		/// <param name="data">data to incorporate</param>
		public void IncorporateFCS(int fcs, short[] data)
		{
			if ((fcs & 0x400) != 0)
				data[0] += 659;
			if ((fcs & 0x200) != 0)
				data[9] = (short)(~data[9] & 0x7ff);
			if ((fcs & 0x100) != 0)
				data[9] = (short)(~data[8] & 0x7ff);
			if ((fcs & 0x80) != 0)
				data[9] = (short)(~data[7] & 0x7ff);
			if ((fcs & 0x40) != 0)
				data[9] = (short)(~data[6] & 0x7ff);
			if ((fcs & 0x20) != 0)
				data[9] = (short)(~data[5] & 0x7ff);
			if ((fcs & 0x10) != 0)
				data[9] = (short)(~data[4] & 0x7ff);
			if ((fcs & 0x8) != 0)
				data[9] = (short)(~data[3] & 0x7ff);
			if ((fcs & 0x4) != 0)
				data[9] = (short)(~data[2] & 0x7ff);
			if ((fcs & 0x2) != 0)
				data[9] = (short)(~data[1] & 0x7ff);
			if ((fcs & 0x1) != 0)
				data[9] = (short)(~data[0] & 0x7ff);

		}

		/// <summary>
		/// Encode the routing code
		/// </summary>
		/// <param name="value">routing code value, either 0, 5, 9 or 11 digits long</param>
		/// <returns>encoded number containing the routing code</returns>
		public long ConvertRoutingCode(string value)
		{
			long tmp = 0;
			if (value.Length == 5)
				tmp = long.Parse(value) + 1;
			if (value.Length == 9)
				tmp = long.Parse(value) + 100001;
			if (value.Length == 11)
				tmp = long.Parse(value) + 1000100001;

			return tmp;
		}

		/// <summary>
		/// Encode the routing code and destination code into a byte array
		/// </summary>
		/// <param name="routingcode">Encoded routing code</param>
		/// <param name="value">destination code</param>
		/// <returns>byte array</returns>
		public byte[] ConvertToBytes(long routingcode, string value)
		{
			routingcode = (routingcode * 10) + int.Parse(value.Substring(0, 1));
			routingcode = (routingcode * 5) + int.Parse(value.Substring(1, 1));

			value = routingcode.ToString("d11") + value.Substring(2);

			byte[] result = new byte[13];

			int length = 0, digit = 0;
			for (int i = 0; i < value.Length; i++)
			{
				digit = byte.Parse(value.Substring(i, 1));

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

			return result;
		}

		/// <summary>
		/// Convert the encoded data into code words
		/// </summary>
		/// <param name="data">data to encode</param>
		/// <returns>13 bit code words</returns>
		public short[] ConvertToCodewords(byte[] data)
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

		/// <summary>
		/// Shift the codeword base
		/// </summary>
		/// <param name="data">codewords to convert</param>
		/// <param name="basediv">base to shift to</param>
		/// <returns>shifted code words</returns>
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

		/// <summary>
		/// Check Frame Check Sequence is valid?
		/// </summary>
		/// <param name="fcs">FCS to check</param>
		/// <returns>true if the FCS is greater than 0x400</returns>
		public bool CheckFcs(int fcs)
		{
			return (fcs & 0x400) != 0;
		}

		/// <summary>
		/// Lookup codewords in the tables
		/// </summary>
		/// <param name="data">data to encode</param>
		public void ConvertToCharacters(short[] data)
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

		/// <summary>
		/// Include the FCS in the data
		/// </summary>
		/// <param name="data">data</param>
		/// <param name="fcs">Frame Check Sequence</param>
		public void IncludeFcs(short[] data, int fcs)
		{

			for (int i = 0; i < data.Length; i++)
			{
				if ((fcs & (1 << i)) != 0)
					data[i] ^= 0x1fff;
			}
		}

		//ABCDE FGHIJ
		//01234 56789

		//12   11 10 9 8   7 6 5 4   3 2 1 0
		// 1    8  4 2 1   8 4 2 1   8 4 2 1

		/// <summary>
		/// Convert the encoded data into bar patterns
		/// </summary>
		/// <param name="data">data to encode</param>
		/// <returns>bar pattern collection</returns>
		public CodedValueCollection ConvertToBars(short[] data)
		{
			CodedValueCollection result = new CodedValueCollection();

			result.Add(ChooseBar(data[7] & 0x0004, data[4] & 0x0008));
			result.Add(ChooseBar(data[1] & 0x0400, data[0] & 0x0001));
			result.Add(ChooseBar(data[9] & 0x1000, data[2] & 0x0100));
			result.Add(ChooseBar(data[5] & 0x0020, data[6] & 0x0800));
			result.Add(ChooseBar(data[8] & 0x0200, data[3] & 0x0002));
			result.Add(ChooseBar(data[0] & 0x0002, data[5] & 0x1000));
			result.Add(ChooseBar(data[2] & 0x0020, data[1] & 0x0100));
			result.Add(ChooseBar(data[4] & 0x0010, data[9] & 0x0800));
			result.Add(ChooseBar(data[6] & 0x0008, data[8] & 0x0400));
			result.Add(ChooseBar(data[3] & 0x0200, data[7] & 0x0040));

			result.Add(ChooseBar(data[5] & 0x0800, data[1] & 0x0010));
			result.Add(ChooseBar(data[8] & 0x0020, data[2] & 0x1000));
			result.Add(ChooseBar(data[9] & 0x0400, data[0] & 0x0004));
			result.Add(ChooseBar(data[7] & 0x0002, data[6] & 0x0080));
			result.Add(ChooseBar(data[3] & 0x0040, data[4] & 0x0200));
			result.Add(ChooseBar(data[0] & 0x0008, data[8] & 0x0040));
			result.Add(ChooseBar(data[6] & 0x0010, data[2] & 0x0080));
			result.Add(ChooseBar(data[1] & 0x0002, data[9] & 0x0200));
			result.Add(ChooseBar(data[7] & 0x0400, data[5] & 0x0004));
			result.Add(ChooseBar(data[4] & 0x0001, data[3] & 0x0100));

			result.Add(ChooseBar(data[6] & 0x0004, data[0] & 0x0010));
			result.Add(ChooseBar(data[8] & 0x0800, data[1] & 0x0001));
			result.Add(ChooseBar(data[9] & 0x0100, data[3] & 0x1000));
			result.Add(ChooseBar(data[2] & 0x0040, data[7] & 0x0080));
			result.Add(ChooseBar(data[5] & 0x0002, data[4] & 0x0400));
			result.Add(ChooseBar(data[1] & 0x1000, data[6] & 0x0200));
			result.Add(ChooseBar(data[7] & 0x0008, data[8] & 0x0001));
			result.Add(ChooseBar(data[5] & 0x0100, data[9] & 0x0080));
			result.Add(ChooseBar(data[4] & 0x0040, data[2] & 0x0400));
			result.Add(ChooseBar(data[3] & 0x0010, data[0] & 0x0020));

			result.Add(ChooseBar(data[8] & 0x0010, data[5] & 0x0080));
			result.Add(ChooseBar(data[7] & 0x0800, data[1] & 0x0200));
			result.Add(ChooseBar(data[6] & 0x0001, data[9] & 0x0040));
			result.Add(ChooseBar(data[0] & 0x0040, data[4] & 0x0100));
			result.Add(ChooseBar(data[2] & 0x0002, data[3] & 0x0004));
			result.Add(ChooseBar(data[5] & 0x0200, data[8] & 0x1000));
			result.Add(ChooseBar(data[4] & 0x0800, data[6] & 0x0002));
			result.Add(ChooseBar(data[9] & 0x0020, data[7] & 0x0010));
			result.Add(ChooseBar(data[3] & 0x0008, data[1] & 0x0004));
			result.Add(ChooseBar(data[0] & 0x0080, data[2] & 0x0001));

			result.Add(ChooseBar(data[1] & 0x0008, data[4] & 0x0002));
			result.Add(ChooseBar(data[6] & 0x0400, data[3] & 0x0020));
			result.Add(ChooseBar(data[8] & 0x0080, data[9] & 0x0010));
			result.Add(ChooseBar(data[2] & 0x0800, data[5] & 0x0040));
			result.Add(ChooseBar(data[0] & 0x0100, data[7] & 0x1000));
			result.Add(ChooseBar(data[4] & 0x0004, data[8] & 0x0002));
			result.Add(ChooseBar(data[5] & 0x0400, data[3] & 0x0001));
			result.Add(ChooseBar(data[9] & 0x0008, data[0] & 0x0200));
			result.Add(ChooseBar(data[6] & 0x0020, data[2] & 0x0010));
			result.Add(ChooseBar(data[7] & 0x0100, data[1] & 0x0080));

			result.Add(ChooseBar(data[5] & 0x0001, data[4] & 0x0020));
			result.Add(ChooseBar(data[2] & 0x0008, data[0] & 0x0400));
			result.Add(ChooseBar(data[6] & 0x1000, data[9] & 0x0004));
			result.Add(ChooseBar(data[3] & 0x0800, data[1] & 0x0040));
			result.Add(ChooseBar(data[8] & 0x0100, data[7] & 0x0200));
			result.Add(ChooseBar(data[5] & 0x0010, data[0] & 0x0800));
			result.Add(ChooseBar(data[1] & 0x0020, data[2] & 0x0004));
			result.Add(ChooseBar(data[9] & 0x0002, data[4] & 0x1000));
			result.Add(ChooseBar(data[8] & 0x0008, data[6] & 0x0040));
			result.Add(ChooseBar(data[7] & 0x0001, data[3] & 0x0080));

			result.Add(ChooseBar(data[4] & 0x0080, data[7] & 0x0020));
			result.Add(ChooseBar(data[0] & 0x1000, data[1] & 0x0800));
			result.Add(ChooseBar(data[2] & 0x0200, data[9] & 0x0001));
			result.Add(ChooseBar(data[6] & 0x0100, data[5] & 0x0008));
			result.Add(ChooseBar(data[3] & 0x0400, data[8] & 0x0004));

			return result;
		}

		/// <summary>
		/// Converts a pair of values into a 4-state bar
		/// </summary>
		/// <param name="descender">ascender value</param>
		/// <param name="ascender">descender value</param>
		/// <returns>4-state bar value</returns>
		private int ChooseBar(int descender, int ascender)
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
