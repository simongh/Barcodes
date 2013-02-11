using System;

namespace Barcodes2.Services
{
	public static class Locator
	{
		private static ILocatorProvider _provider;
		private static object _lock;

		public static bool IsLocatorSet
		{
			get { return _provider == null; }
		}

		static Locator()
		{
			_lock = new object();
		}

		public static void SetLocator(ILocatorProvider provider)
		{
			if (IsLocatorSet)
				throw new ApplicationException("The provider is already set");

			lock (_lock)
			{
				if (IsLocatorSet)
					throw new ApplicationException("The provider is already set");

				_provider = provider;
			}
		}

		public static T Get<T>(params object[] arguments)
		{
			if (IsLocatorSet)
				return _provider.Get<T>(arguments);

			if (arguments != null)
				throw new ArgumentException("arguments are not supported without a locator");

			return Activator.CreateInstance<T>();
		}

		public static object Get(Type type, params object[] arguments)
		{
			if (IsLocatorSet)
				return _provider.Get(type, arguments);

			return Activator.CreateInstance(type, arguments);
		}
	}
}
