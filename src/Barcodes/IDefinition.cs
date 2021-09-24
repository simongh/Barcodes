using System.Collections.Generic;

namespace Barcodes
{
	public interface IDefinition
	{
		IEnumerable<Pattern> PatternSet { get; }

		string GetDisplayText(string value);

		bool ValidateInput(string value);
	}
}