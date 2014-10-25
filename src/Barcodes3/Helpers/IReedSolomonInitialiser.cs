
namespace Barcodes.Helpers
{
	/// <summary>
	/// Defines the initialiser for the Reed Solomon functions
	/// </summary>
	public interface IReedSolomonInitialiser
	{
		int G { get; }
		int GetECCCount(int dataCount, int level);
	}
}
