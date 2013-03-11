using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

		private struct DataItem
		{
			public EncoderFormat Format;
			public byte[] BinaryData;
			public string Data;

			public DataItem(EncoderFormat format, string data)
			{
				Format = format;
				Data = data;
				BinaryData = null;
			}

			public DataItem(EncoderFormat format, byte[] data)
			{
				Format = format;
				BinaryData = data;
				Data = null;
			}
		}

		private IList<DataItem> _data;
		private MemoryStream _output;
		private EncoderFormat _currentFormat;

		public int Length
		{
			get { return _data.Sum(x => x.Data == null ? x.BinaryData.Length : x.Data.Length); }
		}

		public DataMatrixWriter()
		{
			_data = new List<DataItem>();
		}

		public void Write(string value)
		{
			Write(value, EncoderFormat.Auto);
		}

		public void Write(string value, EncoderFormat format)
		{
			if (format == EncoderFormat.Auto)
				format = EncoderFormat.AsciiLower;

			_data.Add(new DataItem(format, value));
		}

		public void Write(byte[] data)
		{
			_data.Add(new DataItem(EncoderFormat.Byte, data));
		}

		public byte[] ToArray(int capacity)
		{
			_currentFormat = EncoderFormat.AsciiLower;
			_output = new MemoryStream();

			for (int i = 0; i < _data.Count; i++)
			{
				var item = _data[i];

				SwitchFormat(item);

				if (i + 1 == _data.Count)
					CheckEnd(capacity);

				switch (item.Format)
				{
					case EncoderFormat.AsciiLower:
						ParseAscii(item.Data);
						break;
					case EncoderFormat.C40:
					case EncoderFormat.TEXT:
					case EncoderFormat.X12:
						ParseC40(item.Data);
						break;
					case EncoderFormat.EDIFACT:
						ParseEdifact(item.Data);
						break;
					case EncoderFormat.Byte:
						ParseBinary(item.BinaryData);
						break;
				}
			}

			if (capacity > -1 && _output.Length > capacity)
				throw new BarcodeException("Data exceeds barcode capacity");

			return _output.ToArray();
		}

		private void CheckEnd(int capacity)
		{
			var item = _data.Last();

			if (item.Format == EncoderFormat.AsciiLower || item.Format == EncoderFormat.Byte)
				return;

			if (item.Format == EncoderFormat.EDIFACT)
			{
				var tmp = item.Data.TrimEnd(SWITCHASCII);
				var remaining = (tmp.Length) % 4;
				if (remaining == 1 || remaining == 2)
				{
					_data.Add(new DataItem(EncoderFormat.AsciiLower, tmp.Substring(tmp.Length - remaining)));

					item.Data = tmp.Remove(tmp.Length - remaining);
					_currentFormat = EncoderFormat.AsciiLower;
				}
				else if (remaining == 0)
					item.Data = tmp;

				return;
			}

			//handle C40 end rules
		}

		private void SwitchFormat(DataItem item)
		{
			if (_currentFormat == item.Format)
				return;

			if (_currentFormat == EncoderFormat.AsciiLower)
			{
				switch (item.Format)
				{
					case EncoderFormat.C40:
						_output.WriteInt(SWITCHC40);
						break;
					case EncoderFormat.TEXT:
						_output.WriteInt(SWITCHTEXT);
						break;
					case EncoderFormat.X12:
						_output.WriteInt(SWITCHX12);
						break;
					case EncoderFormat.EDIFACT:
						_output.WriteInt(SWITCHEDIFACT);
						break;
					case EncoderFormat.Byte:
						_currentFormat = EncoderFormat.AsciiLower;
						break;
					default:
						throw new BarcodeException("Unknown or unsupported encoding format");
				}

				return;
			}

			if (item.Data.Last() != SWITCHASCII)
				item.Data += SWITCHASCII;
			_currentFormat = EncoderFormat.AsciiLower;
			SwitchFormat(item);
		}

		//public DataMatrixWriter()
		//{
		//	_stream = new MemoryStream();
		//	_currentFormat = EncoderFormat.AsciiLower;
		//}

		//public void Write(string value)
		//{
		//	Write(value, EncoderFormat.Auto);
		//}

		//public void Write(string value, EncoderFormat format)
		//{
		//	if (format == EncoderFormat.Auto)
		//		format = EncoderFormat.AsciiLower;

		//	SwitchFormat(format);

		//	switch (format)
		//	{
		//		case EncoderFormat.AsciiLower:
		//			ParseAscii(value);
		//			break;
		//		case EncoderFormat.C40:
		//		case EncoderFormat.TEXT:
		//		case EncoderFormat.X12:
		//			ParseC40(value);
		//			break;
		//		case EncoderFormat.EDIFACT:
		//			ParseEdifact(value);
		//			break;
		//		case EncoderFormat.Byte:
		//		default:
		//			throw new ArgumentException("Unknown or invalid encoding specified");
		//	}
		//}

		public void ParseBinary(byte[] data)
		{
			if (data.Length <= 249)
				_output.WriteInt(data.Length);
			else if (data.Length >= 250 && data.Length <= 1555)
			{
				_output.WriteInt((data.Length / 250) + 249);
				_output.WriteInt(data.Length % 250);
			}

			_output.Write(data, 0, data.Length);
		}

		private void ParseAscii(string value)
		{
			var index = 0;
			while (index < value.Length)
			{
				if (index + 1 < value.Length && char.IsDigit(value[index]) && char.IsDigit(value[index + 1]))
				{
					var tmp = int.Parse(value.Substring(index, 2));
					_output.WriteInt(130 + tmp);
					index += 2;
				}
				else
				{
					var c = value[index];
					if (c >= 0 && c <= 0x7f)
						_output.WriteInt(c + 1);
					else if (c >= 0x80 && c <= 0xff)
					{
						_output.WriteByte(0xeb);
						_output.WriteInt(c - 0x7f);
					}
					else
						throw new ApplicationException("Value not in ASCII character range");

					index++;
				}
			}
		}

		private void ParseEdifact(string value)
		{
			var targetlength = (((value.Length - 1) / 4) + 1) * 3;
			var tmp = 0;
			int sourceIndex = 0, targetIndex = 0;

			while(targetIndex < targetlength)
			{
				tmp = tmp << 6;
				if (sourceIndex < value.Length)
				{
					if (value[sourceIndex] > 62 && value[sourceIndex] < 95)
						tmp += (value[sourceIndex] & 0x3f);
					else
						throw new ApplicationException("Value encountered not supported by EDIFACT encoding");
				}

				if (sourceIndex % 4 == 3)
				{
					_output.WriteInt(tmp / 0x10000);
					_output.WriteInt(tmp / 0x100);
					_output.WriteInt(tmp % 0x100);

					targetIndex += 3;
				}

				sourceIndex++;
			}
		}

		private void ParseC40(string value)
		{
			Action<int, Queue<byte>> lookupvalue = null;
			if (_currentFormat == EncoderFormat.TEXT)
				lookupvalue = LookupText;
			else
				lookupvalue = LookupC40;

			int sourceIndex = 0;
			var buffer = new Queue<byte>();

			while (buffer.Count > 0 || sourceIndex < value.Length)
			{
				if (sourceIndex < value.Length)
				{
					var c = value[sourceIndex];
					if (c >= 0 && c <= 127)
						lookupvalue(c, buffer);
					else if (c >= 128 && c <= 254)
					{
						buffer.Enqueue(1);
						buffer.Enqueue(30);
						lookupvalue(c - 128, buffer);
					}
					else
						throw new ApplicationException("Invalid character cannot be encoded using C40");
				}
				else
					buffer.Enqueue(0);

				if (buffer.Count >= 3)
				{
					var tmp = (buffer.Dequeue() * 1600) + (buffer.Dequeue() * 40) + buffer.Dequeue() + 1;
					_output.WriteInt(tmp / 0x100);
					_output.WriteInt(tmp % 0x100);
				}

				sourceIndex++;
			}
		}

		private void LookupC40(int value, Queue<byte> buffer)
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
				buffer.Enqueue(0);
				offset = 0;
			}
			else if (value >= '!' && value <= '/')
			{
				buffer.Enqueue(1);
				offset = 33;
			}
			else if (value >= ':' && value <= '_')
			{
				buffer.Enqueue(1);
				offset = 43;
			}
			else if (value >= '\'' && value <= 127)
			{
				buffer.Enqueue(2);
				offset = 96;
			}

			buffer.Enqueue((byte)(value - offset));
		}

		private void LookupText(int value, Queue<byte> buffer)
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
				buffer.Enqueue(0);
				offset = 0;
			}
			else if (value >= '!' && value <= '/')
			{
				buffer.Enqueue(1);
				offset = 33;
			}
			else if (value >= ':' && value <= '@')
			{
				buffer.Enqueue(1);
				offset = 43;
			}
			else if (value >= '[' && value <= '_')
			{
				buffer.Enqueue(1);
				offset = 69;
			}
			else if (value == '\'')
			{
				buffer.Enqueue(2);
				offset = 96;
			}
			else if (value >= 'A' && value <= 127)
			{
				buffer.Enqueue(2);
				offset = 64;
			}

			buffer.Enqueue((byte)(value - offset));
		}

		public void Clear()
		{
			_output = null;
			_data.Clear();
		}

		//public byte[] ToBytes(int capacity)
		//{
		//	var output = new MemoryStream();
		//	_stream.Position = 0;
		//	var encoding = EncoderFormat.AsciiLower;
		//	_Latches.Add((int)_stream.Length);

		//	foreach (var item in _Latches)
		//	{
		//		switch (encoding)
		//		{
		//			case EncoderFormat.AsciiLower:

		//				break;
		//			case EncoderFormat.C40:
		//				break;
		//			case EncoderFormat.TEXT:
		//				break;
		//			case EncoderFormat.X12:
		//				break;
		//			case EncoderFormat.EDIFACT:
		//				break;
		//			case EncoderFormat.Byte:
		//				break;
		//			default:
		//				break;
		//		}
		//	}

		//	if (capacity > -1 && output.Length > capacity)
		//		throw new BarcodeException("Data exceeds barcode capacity");

		//	return output.ToArray();
		//}

		//private byte[] Compact(int position, EncoderFormat format)
		//{
		//	byte[] result = null;
		//	var count = _stream.Position - _stream.Length;

		//	if (format == EncoderFormat.AsciiLower || format == EncoderFormat.Byte)
		//	{
		//		result = new byte[count];
		//		_stream.Read(result, 0, result.Length);
		//	}
		//	else if (format == EncoderFormat.EDIFACT)
		//	{
		//	}
		//	return result;
		//}


		//private void WriteBytes(int count, EncoderFormat encoding, Stream stream)
		//{
		//	if (encoding == EncoderFormat.AsciiLower || encoding == EncoderFormat.Byte)
		//	{
		//		_stream.CopyTo(stream, count);
		//	}
		//	else if (encoding == EncoderFormat.EDIFACT)
		//	{
		//		var index = 0;
		//		while (index + 4 < count)
		//		{
		//			int tmp = (_stream.ReadByte() * 0x40000) + (_stream.ReadByte() * 0x1000) + (_stream.ReadByte() * 0x40) + _stream.ReadByte();
		//			stream.WriteByte((byte)(tmp / 0x10000));
		//			stream.WriteByte((byte)(tmp / 0x100));
		//			stream.WriteByte((byte)(tmp % 0x100));

		//			index += 4;
		//		}
		//	}
		//	else
		//	{
		//		var index = 0;
		//		while (index + 3 < count)
		//		{
		//			var tmp = (1600 * _stream.ReadByte()) + (40 * _stream.ReadByte()) + _stream.ReadByte() + 1;
		//			stream.WriteByte((byte)(tmp / 256));
		//			stream.WriteByte((byte)(tmp % 256));
		//			index += 3;
		//		}
		//	}
		//}

		//private void WriteEndBytes(int count, EncoderFormat encoding, Stream stream)
		//{
		//	switch (encoding)
		//	{
		//		case EncoderFormat.AsciiLower:
		//			WriteBytes(count, encoding, stream);
		//			break;
		//		case EncoderFormat.C40:
		//		case EncoderFormat.TEXT:
		//			if ((count % 3) == 2)
		//			{
		//				_stream.WriteByte(0);
		//				WriteBytes(count + 1, encoding, stream);
		//			}
		//			else if ((count % 3) == 1)
		//			{
		//				WriteBytes(count - 1, encoding, stream);
		//				//unlatch if space
		//				WriteBytes(1, EncoderFormat.AsciiLower, stream);
		//			}
		//			else
		//				WriteBytes(count, encoding, stream);
		//			break;
		//		case EncoderFormat.X12:
		//			if ((count % 3) == 2)
		//			{
		//				WriteBytes(count - 2, encoding, stream);
		//				//unlatch
		//				WriteBytes(2, EncoderFormat.AsciiLower, stream);
		//			}
		//			else if ((count % 3) == 1)
		//			{
		//				WriteBytes(count - 1, encoding, stream);
		//				WriteBytes(1, EncoderFormat.AsciiLower, stream);
		//			}
		//			else
		//				WriteBytes(count, encoding, stream);
		//			break;
		//		case EncoderFormat.EDIFACT:
		//			if ((count % 4) == 1 || (count % 4) == 2)
		//			{
		//				WriteBytes(count - (count % 4), encoding, stream);
		//				WriteBytes(count % 4, EncoderFormat.AsciiLower, stream);
		//			}
		//			else
		//				WriteBytes(count, encoding, stream);
		//			break;
		//		case EncoderFormat.Byte:
		//			break;
		//		default:
		//			break;
		//	}
		//}

		//private void WriteLatch(EncoderFormat encoding, Stream stream)
		//{
		//	switch (encoding)
		//	{
		//		case EncoderFormat.Auto:
		//			break;
		//		case EncoderFormat.AsciiLower:
		//			break;
		//		case EncoderFormat.AsciiExtended:
		//			break;
		//		case EncoderFormat.AsciiNumber:
		//			break;
		//		case EncoderFormat.C40:
		//			break;
		//		case EncoderFormat.TEXT:
		//			break;
		//		case EncoderFormat.X12:
		//			break;
		//		case EncoderFormat.EDIFACT:
		//			break;
		//		case EncoderFormat.Byte:
		//			break;
		//		default:
		//			break;
		//	}
		//}
	}
}
