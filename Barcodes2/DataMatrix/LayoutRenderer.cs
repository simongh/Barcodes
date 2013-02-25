using System.Collections.Generic;
using System.Drawing;

namespace Barcodes2.DataMatrix
{
	public class LayoutRenderer
	{
		private List<Rectangle> _array;
		private List<int> _locations;
		private int _index;
		private byte[] _data;

		public Size Size
		{
			get;
			set;
		}

		private byte Data
		{
			get { return _data[_index]; }
		}

		private void Module(int row, int col, byte mask)
		{
			if (row < 0)
			{
				row += Size.Height;
				col += 4 - ((Size.Height + 4) % 8);
			}

			if (col < 0)
			{
				col += Size.Width;
				row += 4 - ((Size.Width + 4) % 8);
			}

			_locations.Add(row * Size.Width + col);
			if ((Data & mask) == mask)
				_array.Add(new Rectangle(col, row, 1, 1));
		}
		
		private void Utah(Point location)
		{
			Module(location.Y - 2, location.X - 2, 0x01);
			Module(location.Y - 2, location.X - 1, 0x02);
			Module(location.Y - 1, location.X - 2, 0x04);
			Module(location.Y - 1, location.X - 1, 0x08);
			Module(location.Y - 1, location.X, 0x10);
			Module(location.Y, location.X - 2, 0x20);
			Module(location.Y, location.X - 1, 0x40);
			Module(location.Y, location.X, 0x80);

			_index++;
		}

		private void Corner1()
		{
			Module(Size.Height - 1, 0, 0x01);
			Module(Size.Height - 1, 1, 0x02);
			Module(Size.Height - 1, 2, 0x04);
			Module(0, Size.Width - 2, 0x08);
			Module(0, Size.Width - 1, 0x10);
			Module(1, Size.Width - 1, 0x20);
			Module(2, Size.Width - 1, 0x40);
			Module(3, Size.Width - 1, 0x80);

			_index++;
		}

		private void Corner2()
		{
			Module(Size.Height - 3, 0, 0x01);
			Module(Size.Height - 2, 0, 0x02);
			Module(Size.Height - 1, 0, 0x04);
			Module(0, Size.Width - 4, 0x08);
			Module(0, Size.Width - 3, 0x10);
			Module(0, Size.Width - 2, 0x20);
			Module(0, Size.Width - 1, 0x40);
			Module(1, Size.Width - 1, 0x80);

			_index++;
		}

		private void Corner3()
		{
			Module(Size.Height - 3, 0, 0x01);
			Module(Size.Height - 2, 0, 0x02);
			Module(Size.Height - 1, 0, 0x04);
			Module(0, Size.Width - 2, 0x08);
			Module(0, Size.Width - 1, 0x10);
			Module(1, Size.Width - 1, 0x20);
			Module(2, Size.Width - 1, 0x40);
			Module(3, Size.Width - 1, 0x80);

			_index++;
		}

		private void Corner4()
		{
			Module(Size.Height - 1, 0, 0x01);
			Module(Size.Height - 1, Size.Width - 1, 0x02);
			Module(0, Size.Width - 3, 0x04);
			Module(0, Size.Width - 2, 0x08);
			Module(0, Size.Width - 1, 0x10);
			Module(1, Size.Width - 3, 0x20);
			Module(1, Size.Width - 2, 0x40);
			Module(1, Size.Width - 1, 0x80);

			_index++;
		}

		public Rectangle[] Render(byte[] data)
		{
			if (Size.Height < 6 || (Size.Height & 0x01) == 1 || Size.Width < 6 || (Size.Width & 0x01) == 1)
				throw new BarcodeException("The dimensions were invalid");

			_array = new List<Rectangle>();
			_locations = new List<int>();
			_index = 0;
			_data = data;
			var location = new Point(0, 4);

			do
			{
				if ((location.Y == Size.Height) && (location.X == 0))
					Corner1();
				if ((location.Y == Size.Height - 2) && (location.X == 0) && (Size.Width % 4 > 0))
					Corner2();
				if ((location.Y == Size.Height - 2) && (location.X == 0) && (Size.Width % 8 == 4))
					Corner3();
				if ((location.Y == Size.Height + 4) && (location.X == 2) && (Size.Width % 8 == 0))
					Corner4();

				do
				{
					if ((location.Y < Size.Height) && (location.X >= 0) && !_locations.Contains(location.Y * Size.Width + location.X))
						Utah(location);
					location.Y -= 2;
					location.X += 2;
				} while ((location.Y >= 0) && (location.X < Size.Width));
				location.Y += 1;
				location.X += 3;

				do
				{
					if ((location.Y >= 0) && (location.X < Size.Width) && !_locations.Contains(location.Y * Size.Width + location.X))
						Utah(location);
					location.Y += 2;
					location.X -= 2;
				} while ((location.Y < Size.Height) && (location.X >= 0));
				location.Y += 3;
				location.X += 1;

			} while ((location.Y < Size.Height) || (location.X < Size.Width));

			if (!_locations.Contains(Size.Height * Size.Width - 1))
			{
				_array.Add(new Rectangle(Size.Width - 1, Size.Height, 1, 1));
				_array.Add(new Rectangle(Size.Width - 2, Size.Height - 1, 1, 1));
			}
			return _array.ToArray();
			//	_array[Size.Height * Size.Width - 1] = _array[Size.Height * _cols - Size.Width - 2] = 1;
		}
	}
}
