namespace Barcodes
{
	public interface IDefinition
	{
		PatternSet PatternSet { get; }

		bool ValidateInput(string value);
	}
}