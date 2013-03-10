using System;
using System.Collections.Generic;
using System.IO;

namespace Barcodes2.DataMatrix
{
	public class DataMatrixWriter
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

		private EncoderFormat _currentFormat;
		private MemoryStream _stream;
		private IDictionary<int, EncoderFormat> _Latches;

		public int Length
		{
			get { return (int)_stream.Position; }
		}

		public DataMatrixWriter()
		{
			_stream = new MemoryStream();
			_currentFormat = EncoderFormat.AsciiLower;
		}

		public void Write(string value)
		{
			Write(value, EncoderFormat.Auto);
		}

		public void Write(string value, EncoderFormat format)
		{
			if (format == EncoderFormat.Auto)
				format = EncoderFormat.AsciiLower;

			if (format != _currentFormat)
			{
				_Latches.Add((int)_stream.Position, format);
				_currentFormat = format;
			}

			switch (format)
			{
				case EncoderFormat.AsciiLower:
					ParseAscii(value);
					break;
				case EncoderFormat.C40:
				case EncoderFormat.TEXT:
				case EncoderFormat.X12:
					ParseC40(value);
					break;
				case EncoderFormat.EDIFACT:
					ParseEdifact(value);
					break;
				case EncoderFormat.Byte:
				default:
					throw new ArgumentException("Unknown or invalid encoding specified");
			}
		}

		public void Write(byte[] data)
		{
			_Latches.Add((int)_stream.Position, EncoderFormat.Byte);
			_currentFormat = EncoderFormat.AsciiLower;

			if (data.Length <= 249)
				_stream.WriteInt(data.Length);
			else if (data.Length >= 250 && data.Length <= 1555)
			{
				_stream.WriteInt((data.Length / 250) + 249);
				_stream.WriteInt(data.Length % 250);
			}

			_stream.Write(data, 0, data.Length);
		}

		private void ParseAscii(string value)
		{
			var index = 0;
			while (index < value.Length)
			{
				if (index + 1 < value.Length && char.IsDigit(value[index]) && char.IsDigit(value[index + 1]))
				{
					var tmp = int.Parse(value.Substring(index, 2));
					_stream.WriteInt(130 + tmp);
					index += 2;
				}
				else 
				{
					var c = value[index];
					if (c >= 0 && c <= 0x7f)
						_stream.WriteInt(c + 1);
					else if (c >= 0x80 && c <= 0xff)
					{
						_stream.WriteByte(0xeb);
						_stream.WriteInt(c - 0x7f);
					}
					else
						throw new ApplicationException("Value not in ASCII character range");

					index++;
				}
			}
		}

		private void ParseC40(string value)
		{
			Func<int,int> getbyte = null;
			if (_currentFormat == EncoderFormat.TEXT)
				getbyte = GetTextByte;
			else  
				getbyte = GetC40Byte;

			for (int i = 0; i < value.Length; i++)
			{
				var c = value[i];
				if (c >=0 && c<= 127)
					_stream.WriteInt(getbyte(c));
				else if (c >= 128 && c <= 254)
				{
					_stream.WriteByte(1);
					_stream.WriteByte(30);
					_stream.WriteInt(getbyte(c - 128));
				}
				else
					throw new ApplicationException("Invalid character cannot be encoded using C40");
			}
		}

		private void ParseEdifact(string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] > 62 && value[i] < 95)
					_stream.WriteInt(value[i] & 0x3f);
				else
					throw new ApplicationException("Value encountered not supported by EDIFACT encoding");
			}
		}

		private int GetC40Byte(int value)
		{
			var offset = 0;
			if (value == ' ')
				offset = 29;
			else if (value >= '0' && value <= '9')
				offset = 44;
			else if (value >= 'A' && value <= 'Z')
				offset = 51;
			else if (value >= 0 && value <= 31)
			{
				_stream.WriteByte(0);
				offset = 0;
			}
			else if (value >= '!' && value <= '/')
			{
				_stream.WriteByte(1);
				offset = 33;
			}
			else if (value >= ':' && value <= '_')
			{
				_stream.WriteByte(1);
				offset = 43;
			}
			else if (value >= '\'' && value <= 127)
			{
				_stream.WriteByte(2);
				offset = 96;
			}

			return value - offset;
		}

		private int GetTextByte(int value)
		{
			var offset = 0;
			if (value == 32)
				offset = 29;
			else if (value >= '0' && value <= '9')
				offset = 44;
			else if (value >= 'a' && value <= 'z')
				offset = 83;
			else if (value >= 0 && value <= 31)
			{
				_stream.WriteByte(0);
				offset = 0;
			}
			else if (value >= '!' && value <= '/')
			{
				_stream.WriteByte(1);
				offset = 33;
			}
			else if (value >= ':' && value <= '@')
			{
				_stream.WriteByte(1);
				offset = 43;
			}
			else if (value >= '[' && value <= '_')
			{
				_stream.WriteByte(1);
				offset = 69;
			}
			else if (value == '\'')
			{
				_stream.WriteByte(2);
				offset = 96;
			}
			else if (value >= 'A' && value <= 127)
			{
				_stream.WriteByte(2);
				offset = 64;
			}

			return value - offset;
		}

		public void Clear()
		{
			_stream = new MemoryStream();
		}

		public byte[] ToBytes(int capacity)
		{
			var output = new MemoryStream();
			_stream.Position = 0;
			var encoding = EncoderFormat.AsciiLower;

			foreach (var item in _Latches)
			{
				WriteBytes((item.Key - (int)_stream.Position), item.Value, output);
				encoding = item.Value;
			}
			WriteBytes((int)(_stream.Length - _stream.Position), encoding, output);

			if (capacity > -1 && output.Length > capacity)
				throw new BarcodeException("Data exceeds barcode capacity");

			return output.ToArray();
		}

		private void WriteBytes(int count, EncoderFormat encoding, Stream stream)
		{
			if (encoding == EncoderFormat.AsciiLower || encoding == EncoderFormat.Byte)
			{
				_stream.CopyTo(stream, count);
			}
			else if (encoding == EncoderFormat.EDIFACT)
			{
				var index = 0;
				while (index + 4 < count)
				{
					int tmp = (_stream.ReadByte() * 0x40000) + (_stream.ReadByte() * 0x1000) + (_stream.ReadByte() * 0x40) + _stream.ReadByte();
					stream.WriteByte((byte)(tmp / 0x10000));
					stream.WriteByte((byte)(tmp / 0x100));
					stream.WriteByte((byte)(tmp % 0x100));

					index += 4;
				}
			}
			else
			{
				var index = 0;
				while (index + 3 < count)
				{
					var tmp = (1600 * _stream.ReadByte()) + (40 * _stream.ReadByte()) + _stream.ReadByte() + 1;
					stream.WriteByte((byte)(tmp / 256));
					stream.WriteByte((byte)(tmp % 256));
					index += 3;
				}
			}
		}

		private void WriteEndBytes(int count, EncoderFormat encoding, Stream stream)
		{
			switch (encoding)
			{
				case EncoderFormat.AsciiLower:
					WriteBytes(count, encoding, stream);
					break;
				case EncoderFormat.C40:
				case EncoderFormat.TEXT:
					if ((count % 3) == 2)
					{
						_stream.WriteByte(0);
						WriteBytes(count + 1, encoding, stream);
					}
					else if ((count % 3) == 1)
					{
						WriteBytes(count - 1, encoding, stream);
						//unlatch if space
						WriteBytes(1, EncoderFormat.AsciiLower, stream);
					}
					else
						WriteBytes(count, encoding, stream);
					break;
				case EncoderFormat.X12:
					if ((count % 3) == 2)
					{
						WriteBytes(count - 2, encoding, stream);
						//unlatch
						WriteBytes(2, EncoderFormat.AsciiLower, stream);
					}
					else if ((count % 3) == 1)
					{
						WriteBytes(count - 1, encoding, stream);
						WriteBytes(1, EncoderFormat.AsciiLower, stream);
					}
					else
						WriteBytes(count, encoding, stream);
					break;
				case EncoderFormat.EDIFACT:
					if ((count % 4) == 1 || (count % 4) == 2)
					{
						WriteBytes(count - (count % 4), encoding, stream);
						WriteBytes(count % 4, EncoderFormat.AsciiLower, stream);
					}
					else
						WriteBytes(count, encoding, stream);
					break;
				case EncoderFormat.Byte:
					break;
				default:
					break;
			}
		}
	}
}
