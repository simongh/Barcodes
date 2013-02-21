
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

		protected override void PreRenderCode(RenderState state)
		{
			if (ShowMeasure)
				AddMeasure();
			IsGrey = true;

			base.PreRenderCode(state);
		}

		protected override void PreRenderModule(RenderState state)
		{
			base.PreRenderModule(state);

			if (!ShowModuleShading)
				return;

			if (IsGrey)
			{
				Canvas.FillRectangle(Brushes.Gray, state.Location.X, Settings.TopMargin, (state.CurrentPattern.WideCount * Settings.WideWidth) + (state.CurrentPattern.NarrowCount * Settings.NarrowWidth), Canvas.VisibleClipBounds.Height);
			}
			IsGrey = !IsGrey;

		}
	}
}
