
namespace Barcodes2.Services
{
	public interface IRenderer
	{
		string Name { get; }
		BarcodeSettings Settings { get; set; }
		Definitions.IDefinition Definition { get; set; }
		object Render(CodedValueCollection codes, string displayText);
	}
}
