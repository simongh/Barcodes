
using System.Drawing;
namespace Barcodes2.Services
{
	public class DebugRenderer : BitmapRenderer
	{
		public bool ShowMeasure
		{
			get;
			set;
		}

		public bool ShowModuleShading
		{
			get;
			set;
		}

		protected bool IsGrey
		{
			get;
			set;
		}

		public DebugRenderer()
		{
			ShowMeasure = true;
			ShowModuleShading = true;
		}

		protected virtual void AddMeasure()
		{
			int left = 0;
			bool alt = true;

			while (left < Canvas.VisibleClipBounds.Width)
			{
				if (alt)
					Canvas.FillRectangle(Brushes.Gainsboro, left, 0, 1, Settings.TopMargin);
				left++;
				alt = !alt;
			}
		}

		protected override void PreRenderCode(Point location)
		{
			if (ShowMeasure)
				AddMeasure();
			IsGrey = true;

			base.PreRenderCode(location);
		}

		protected override void PreRenderModule(int index, Point location)
		{
			base.PreRenderModule(index, location);

			if (!ShowModuleShading)
				return;

			if (IsGrey)
			{
				var p = Definition.GetPattern(Codes[index]);
				Canvas.FillRectangle(Brushes.Gray, location.X - Settings.ModulePadding, 0, (p.WideCount * Settings.WideWidth) + (p.NarrowCount * Settings.NarrowWidth), Canvas.VisibleClipBounds.Height);
			}
			IsGrey = !IsGrey;

		}
	}
}
