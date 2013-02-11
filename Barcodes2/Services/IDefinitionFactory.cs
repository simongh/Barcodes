
namespace Barcodes2.Services
{
	public interface IDefinitionFactory
	{
		Definitions.IDefinition GetDefinition(BarcodeFormats format);
	}
}
