using System;

namespace Barcodes.Factories
{
	public class SettingsFactory : ISettingsFactory
	{
		public Settings Default { get { return Create(); } }

		public Settings Copy(Settings source)
		{
			return source.Copy();
		}

		private Settings Create()
		{
			var height = 80;

			return new Settings
			{
				BarHeight = height,
				ShortHeight = height / 3,
				MediumHeight = (height / 3) * 2,
				LeftMargin = 10,
				RightMargin = 10,
				TopMargin = 10,
				BottomMargin = 10,
				WideWidth = 6,
				NarrowWidth = 2,
				ModulePadding = 2,
				IsTextShown = true,
				TextPadding = 10,
				IsTextPadded = true,
				Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericMonospace, 12),
				IsChecksumCalculated = false,
				Scale = 1.0F
			};
		}
	}
}