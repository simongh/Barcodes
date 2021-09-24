namespace Barcodes
{
	public interface IParser
	{
		Pattern Convert(char value);
	}
}