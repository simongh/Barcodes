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

		public EncodedData Parse(string value)
		{
			if (!ValidateData(value))
				return null;

			var data = value.Select(c => _definition.PatternSet.Find(c)).ToList();
			var text = _definition.GetDisplayText(value);

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
	}
}