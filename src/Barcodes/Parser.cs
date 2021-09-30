using System.Collections.Generic;
using System.Linq;

namespace Barcodes
{
	public class Parser
	{
		private IDefinition _definition;

		public Parser(IDefinition definition)
		{
			_definition = definition;
		}

		public bool ValidateData(string value)
		{
			return _definition.ValidateInput(value);
		}

		public EncodedData Parse(byte[] value, string text)
		{
			var data = new List<Pattern>();

			if (_definition is IParser parser)
			{
				data.AddRange(parser.Parse(value));
			}
			else
			{
				data.AddRange(value
					.Select(c =>
					{
						if (_definition is IConvert convert)
							return convert.Convert(c);
						else
							return _definition.PatternSet.Find(c);
					})
					.Where(c => c != null));
			}

			var result = new EncodedData(data)
			{
				DisplayText = text,
			};

			if (_definition is IChecksum checksum)
			{
				if (checksum.IsChecksumRequired)
					checksum.AddChecksum(result);
			}

			if (_definition is ILimits limits)
			{
				limits.AddLimits(result);
			}

			return result;
		}

		public EncodedData Parse(string value) => Parse(value.Select(c => (byte)c).ToArray(), value);

		//{
		//	if (!ValidateData(value))
		//		return null;

		//	var data = new List<Pattern>();

		//	if (_definition is IParser parser)
		//	{
		//		data.AddRange(parser.Parse(value));
		//	}
		//	else
		//	{
		//		data.AddRange(value
		//			.Select(c =>
		//			{
		//				if (_definition is IConvert convert)
		//					return convert.Convert(c);
		//				else
		//					return _definition.PatternSet.Find(c);
		//			})
		//			.Where(c => c != null));
		//	}
		//	var text = _definition.GetDisplayText(value);

		//	var result = new EncodedData(data)
		//	{
		//		DisplayText = text,
		//	};

		//	if (_definition is IChecksum checksum)
		//	{
		//		if (checksum.IsChecksumRequired)
		//			checksum.AddChecksum(result);
		//	}

		//	if (_definition is ILimits limits)
		//	{
		//		limits.AddLimits(result);
		//	}

		//	return result;
		//}
	}
}