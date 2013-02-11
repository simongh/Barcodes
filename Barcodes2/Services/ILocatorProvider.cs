using System;

namespace Barcodes2.Services
{
	public interface ILocatorProvider
	{
		T Get<T>(params object[] arguments);
		object Get(Type type, params object[] arguments);
	}
}
