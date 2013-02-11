
namespace Barcodes2.Definitions
{
	public interface IDefinition
	{
		bool IsChecksumRequired { get; set; }
		bool IsDataValid(string value);
		CodedValueCollection GetCodes(string value);
		string GetDisplayText(string value);
		Pattern GetPattern(int value);
		int CalculateWidth(BarcodeSettings settings, CodedValueCollection codes);
		string AddChecksum(string value, CodedValueCollection codes);
	}
}
