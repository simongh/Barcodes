namespace Barcodes
{
	public interface IDefinition
	{
		PatternSet PatternSet { get; }

		string GetDisplayText(string value);

		bool ValidateInput(string value);
	}
}