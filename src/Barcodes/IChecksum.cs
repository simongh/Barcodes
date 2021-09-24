namespace Barcodes
{
	public interface IChecksum
	{
		bool IsChecksumRequired { get; }

		void AddChecksum(EncodedData data);
	}
}