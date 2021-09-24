﻿using System.Linq;

namespace BarcodeReader
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

		public Types.EncodedData Parse(string value)
		{
			if (!ValidateData(value))
				return null;

			var data = value.Select(c => _definition.PatternSet.First(p => p.Value == c)).ToList();
			var text = _definition.GetDisplayText(value);

			var result = new Types.EncodedData(data)
			{
				DisplayText = text,
			};

			var checksum = _definition as Types.IChecksum;
			if (checksum != null)
			{
				if (checksum.IsChecksumRequired)
					checksum.AddChecksum(result);
			}

			return result;
		}
	}
}