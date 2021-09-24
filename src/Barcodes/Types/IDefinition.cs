using System.Collections.Generic;

namespace BarcodeReader
{
	public interface IDefinition
	{
		IEnumerable<Types.Pattern> PatternSet { get; }

		string GetDisplayText(string value);

		bool ValidateInput(string value);
	}
}