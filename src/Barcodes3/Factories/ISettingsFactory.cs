namespace Barcodes.Factories
{
	public interface ISettingsFactory
	{
		Settings Default { get; }

		Settings Copy(Settings source);
	}
}