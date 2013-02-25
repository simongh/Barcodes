using System;
using System.IO;
using System.Text;

namespace Barcodes2.DataMatrix
{
	public class Encoder
	{
		/// <summary>
		/// Switch to C40 encoding
		/// </summary>
		public const char SWITCHC40 = (char)0xE6;
		/// <summary>
		/// Switch to BYTE encoding
		/// </summary>
		public const char SWITCHBYTE = (char)0xE7;
		/// <summary>
		/// ASCII FNC1 value
		/// </summary>
		public const char FNC1 = (char)0xE8;
		/// <summary>
		/// Switch to ANSI X12 encoding
		/// </summary>
		public const char SWITCHX12 = (char)0xEE;
		/// <summary>
		/// Switch to TEXT encoding
		/// </summary>
		public const char SWITCHTEXT = (char)0xEF;
		/// <summary>
		/// Switch to EDIFACT encoding
		/// </summary>
		public const char SWITCHEDIFACT = (char)0xF0;
		public const char EXTENDEDCHANNEL = (char)0xF1;
		/// <summary>
		/// Switch to ASCII. Also indicates end of data
		/// </summary>
		public const char SWITCHASCII = (char)0xFE;
		/// <summary>
		/// Toggle to extended ASCII
		/// </summary>
		public const char TOGGLEEXTASCI = (char)0xEB;
		/// <summary>
		/// Padding value
		/// </summary>
		public const char PADDING = (char)0x81;

		private string _valueToEncode;
		private int _currentIndex;
		private EncoderFormat _currentFormat;
		private MemoryStream _stream;

		/// <summary>
		/// Gets the current char from the value to encode
		/// </summary>
		private char Current
		{
			get { return _valueToEncode[_currentIndex]; }
		}

		/// <summary>
		/// Encodes a value using auto formatting
		/// </summary>
		/// <param name="value">string value to encode</param>
		/// <returns>encoded data as a byte array</returns>
		public byte[] Encode(string value)
		{
			return Encode(value, EncoderFormat.Auto);
		}

		/// <summary>
		/// Encodes a value using the selcted format.
		/// </summary>
		/// <param name="value">string value to encode</param>
		/// <param name="format">encoding format to use</param>
		/// <returns>encoded data as a byte array</returns>
		public byte[] Encode(string value, EncoderFormat format)
		{
			_stream = new MemoryStream();
			_valueToEncode = value;
			_currentIndex = 0;
			_currentFormat = format;

			if (format == EncoderFormat.Auto)
				_currentFormat = EncoderFormat.AsciiLower;

			while (_currentIndex < _valueToEncode.Length)
			{
				switch (_currentFormat)
				{
					case EncoderFormat.Auto:
					case EncoderFormat.AsciiLower:
					case EncoderFormat.AsciiExtended:
						AsciiEncode();
						break;
					case EncoderFormat.AsciiNumber:
						break;
					case EncoderFormat.C40:
						C40Encoder();
						break;
					case EncoderFormat.TEXT:
						TEXTEncoder();
						break;
					case EncoderFormat.X12:
						AnsiX12();
						break;
					case EncoderFormat.EDIFACT:
						EdifactEncoder();
						break;
					case EncoderFormat.Byte:
						ByteEncoder(Encoding.ASCII.GetBytes(_valueToEncode.Substring(_currentIndex)));
						break;
					default:
						break;
				}
			}

			return _stream.ToArray();
		}

		/// <summary>
		/// Encodes binary data using the binary format
		/// </summary>
		/// <param name="data">binary data to encode</param>
		/// <returns>encoded data as a byte array</returns>
		public byte[] Encode(byte[] data)
		{
			_stream = new MemoryStream();

			ByteEncoder(data);

			return _stream.ToArray();
		}

		/// <summary>
		/// Starts encoding data using ASCII rules.
		/// </summary>
		private void AsciiEncode()
		{
			while (CanRead() && !IsFormatSwitch(Current))
			{
				if (CanRead(2))
					NumberEncode();
				else
				{
					if (Current >= 0 && Current <= 127)
						_stream.WriteByte((byte)(Current + 1));
					else if (Current >= 128 && Current <= 255)
					{
						_stream.WriteByte(0xEB);
						_stream.WriteByte((byte)(Current - 127));
					}
					else
						throw new ApplicationException("Character is out of ASCII range.");

					_currentIndex++;
				}
			}
		}

		/// <summary>
		/// Encodes pairs of digits 00-99
		/// </summary>
		private void NumberEncode()
		{
			while (CanRead(2) && char.IsDigit(Current) && char.IsDigit(_valueToEncode[_currentIndex + 1]))
			{
				var tmp = int.Parse(_valueToEncode.Substring(_currentIndex, 2));
				_stream.WriteByte((byte)(tmp + 130));

				_currentIndex += 2;
			}
		}

		/// <summary>
		/// Encodes using C40 rules.
		/// </summary>
		private void C40Encoder()
		{
			_stream.WriteByte((byte)SWITCHC40);
			DoubleByteEncoder(EncoderFormat.C40);
		}

		/// <summary>
		/// Encodes using TEXT rules
		/// </summary>
		private void TEXTEncoder()
		{
			_stream.WriteByte((byte)SWITCHTEXT);
			DoubleByteEncoder(EncoderFormat.TEXT);
		}

		/// <summary>
		/// Encodes using ANSI X12 rules
		/// </summary>
		private void AnsiX12()
		{
			_stream.WriteByte((byte)SWITCHX12);

			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			while (CanRead() && !IsFormatSwitch(Current))
			{
				if (Current == 13)
					ms.WriteByte(0);
				else if (Current == 42)
					ms.WriteByte(1);
				else if (Current == 62)
					ms.WriteByte(2);
				else if (Current == 32)
					ms.WriteByte(3);
				else if (Current >= 48 && Current <= 57)
					ms.WriteByte((byte)(Current - 44));
				else if (Current >= 65 && Current <= 90)
					ms.WriteByte((byte)(Current - 51));
				else
				{
					_currentFormat = EncoderFormat.AsciiLower;
					break;
				}

				_currentIndex++;
			}

			Compact(ms);
		}

		/// <summary>
		/// Encodes 3 bytes into 2 bytes. Used by C40 and TEXT
		/// </summary>
		/// <param name="format">format to use</param>
		private void DoubleByteEncoder(EncoderFormat format)
		{
			MemoryStream buffer = new MemoryStream();

			while (CanRead() && !IsFormatSwitch(Current))
			{
				CharToInt(Current, buffer, format);
				_currentIndex++;
			}

			Compact(buffer);
		}

		/// <summary>
		/// Encodes using EDIFACT rules
		/// </summary>
		private void EdifactEncoder()
		{
			MemoryStream buffer = new MemoryStream();

			while (CanRead() && !IsFormatSwitch(Current))
			{
				if (Current >= 64 && Current <= 94)
					buffer.WriteByte((byte)(Current - 64));
				else if (Current >= 32 && Current <= 63)
					buffer.WriteByte((byte)Current);
				else
				{
					buffer.WriteByte(31);
					_currentFormat = EncoderFormat.AsciiLower;
					break;
				}
			}

			while (buffer.Length % 4 != 0)
			{
				buffer.WriteByte(0);
			}

			_stream.WriteByte((byte)SWITCHEDIFACT);

			buffer.Seek(0, SeekOrigin.Begin);
			int tmp = 0;
			while (buffer.Position + 4 < buffer.Length)
			{
				tmp = (tmp * 64) + buffer.ReadByte();

				if (buffer.Position > 1 && buffer.Position % 4 == 1)
				{
					_stream.WriteByte((byte)(tmp / 65536));
					_stream.WriteByte((byte)((tmp % 65536) / 256));
					_stream.WriteByte((byte)(tmp % 256));

					tmp = 0;
				}
			}
		}

		/// <summary>
		/// Encodes binary data
		/// </summary>
		/// <param name="data"></param>
		private void ByteEncoder(byte[] data)
		{
			_stream.WriteByte((byte)SWITCHBYTE);

			if (data.Length < 250)
				_stream.WriteByte((byte)data.Length);
			else
			{
				_stream.WriteByte((byte)((data.Length / 250) + 249));
				_stream.WriteByte((byte)(data.Length % 250));
			}

			int tmp;
			for (int i = 0; i < data.Length; i++)
			{
				tmp = ((149 * i) % 250) + 1;
				_stream.WriteByte((byte)((data[i] + tmp) % 256));
			}
		}

		/// <summary>
		/// Encodes the char using the specified format and writes the result to the stream
		/// </summary>
		/// <param name="value">value to encode</param>
		/// <param name="stream">stream to write to</param>
		/// <param name="format">Encoding format</param>
		private void CharToInt(char value, System.IO.MemoryStream stream, EncoderFormat format)
		{
			//Extended ASCII
			if (value > 127)
			{
				stream.WriteByte(1);
				stream.WriteByte(30);
				value = (char)(value - 128);
			}

			if (value == 32)
			{
				stream.WriteByte(3);
				return;
			}

			if (value >= 48 && value <= 57)
			{
				stream.WriteByte((byte)(value - 44));
				return;
			}
			//Shift set 1
			if (value >= 0 && value <= 31)
			{
				stream.WriteByte(0);
				stream.WriteByte((byte)value);
				return;
			}

			//Shift set 2
			if (value >= 33 && value <= 47)
			{
				stream.WriteByte(1);
				stream.WriteByte((byte)(value - 33));
				return;
			}
			if (value >= 58 && value <= 64)
			{
				stream.WriteByte(1);
				stream.WriteByte((byte)(value - 58 + 15));
				return;
			}
			if (value >= 91 && value <= 95)
			{

				stream.WriteByte(1);
				stream.WriteByte((byte)(value - 91 + 22));
				return;
			}

			if (format == EncoderFormat.C40)
			{
				//Shift 3
				if (value >= 96 && value <= 127)
				{
					stream.WriteByte(2);
					stream.WriteByte((byte)(value - 96));
					return;
				}
			}

			if (format == EncoderFormat.TEXT)
			{
				//Shift 3
				if (value == 96)
				{
					stream.WriteByte(2);
					stream.WriteByte((byte)(value - 96));
					return;
				}
				if (value >= 65 && value <= 90)
				{
					stream.WriteByte(2);
					stream.WriteByte((byte)(value - 65 + 1));
					return;
				}
				if (value >= 123 && value <= 127)
				{
					stream.WriteByte(2);
					stream.WriteByte((byte)(value - 123 + 27));
					return;
				}
			}
		}

		/// <summary>
		/// Compacts 3 bytes into 2.
		/// </summary>
		/// <param name="stream">stream to compact from</param>
		private void Compact(System.IO.MemoryStream stream)
		{
			stream.Seek(0, System.IO.SeekOrigin.Begin);

			int tmp = 0;
			while (stream.Position + 3 < stream.Length)
			{
				tmp = (tmp * 40) + stream.ReadByte();

				if (stream.Position > 1 && stream.Position % 3 == 1)
				{
					tmp++;
					_stream.WriteByte((byte)(tmp / 256));
					_stream.WriteByte((byte)(tmp % 256));
					tmp = 0;
				}
			}

			if (stream.Position != stream.Length)
			{
				_currentFormat = EncoderFormat.AsciiLower;
				_stream.WriteByte((byte)SWITCHASCII);
				_currentIndex -= (int)(stream.Length - stream.Position - 1);
			}
			else if (CanRead())
				_stream.WriteByte((byte)SWITCHASCII);

		}

		/// <summary>
		/// Checks the supplied value to see if a switch in encoding rules is needed
		/// </summary>
		/// <param name="value">value to check</param>
		/// <returns>true if a switch is needed. the target format is set as the current format</returns>
		private bool IsFormatSwitch(int value)
		{
			if (_currentFormat != EncoderFormat.AsciiLower && value == SWITCHASCII)
			{
				_currentFormat = EncoderFormat.AsciiLower;
				return true;
			}

			switch (value)
			{
				case SWITCHASCII:
					_currentFormat = EncoderFormat.AsciiLower;
					return true;
				case SWITCHBYTE:
					_currentFormat = EncoderFormat.Byte;
					return true;
				case SWITCHC40:
					_currentFormat = EncoderFormat.C40;
					return true;
				case SWITCHEDIFACT:
					_currentFormat = EncoderFormat.EDIFACT;
					return true;
				case SWITCHTEXT:
					_currentFormat = EncoderFormat.TEXT;
					return true;
				case SWITCHX12:
					_currentFormat = EncoderFormat.X12;
					return true;
				default:
					return false;
			}
		}

		/// <summary>
		/// Returns true if there is a char to read from the input value
		/// </summary>
		/// <returns>true if the index is in range</returns>
		private bool CanRead()
		{
			return CanRead(1);
		}

		/// <summary>
		/// Returns true if there are the specified number of chars to read from the input value
		/// </summary>
		/// <param name="ahead">chars to read ahead</param>
		/// <returns>true if the values are available</returns>
		private bool CanRead(int ahead)
		{
			return _currentIndex + ahead - 1 < _valueToEncode.Length;
		}
	}
}
