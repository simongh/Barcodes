using System.Drawing;

namespace Barcodes2
{
	public class RenderState
	{
		public Point Location
		{
			get;
			set;
		}

		public Pattern CurrentPattern
		{
			get;
			set;
		}

		public int Index
		{
			get;
			set;
		}

		public RenderState()
		{ }

		public RenderState(Point location)
		{
			Location = location;
		}
	}
}
