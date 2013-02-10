using System;
using System.Collections.Generic;
using System.Drawing;

namespace Barcodes.Datamatrix
{
    public class DataMatrix
    {
        private Encoder _Encoder;
        private bool _IsSquare;
        private System.Drawing.Size _Size;
        private byte[] _Data;
        private Bitmap _Canvas;
        private Barcodes.DataMatrix.OutputSettings _Settings;

        public System.Drawing.Size Size
        {
            get { return _Size; }
            set
            {
                if (!value.IsEmpty && !DataMatrixHelper.Instance.ValidateSize(value))
                    throw new ArgumentException("the size was not valid.");

                _Size = value;
            }
        }

        public bool IsInverted
        {
            get;
            set;
        }

        public EncoderFormat EncodingFormat
        {
            get;
            set;
        }

        public bool IsSquare
        {
            get { return _IsSquare; }
            set { _IsSquare = value; }
        }

        public bool IsRectangle
        {
            get { return !_IsSquare; }
            set { _IsSquare = !value; }
        }

        public DataMatrix()
        {
            IsSquare = true;
            _Encoder = new Encoder();
            Size = System.Drawing.Size.Empty;
            EncodingFormat = EncoderFormat.Auto;
        }

        public DataMatrix(System.Drawing.Size size)
            : this()
        {
            Size = size;
        }

        public System.Drawing.Bitmap Generate(string value)
        {
            _Data = _Encoder.Encode(value, EncodingFormat);

            return Draw();
        }

        public System.Drawing.Bitmap Generate(byte[] data)
        {
            _Data = _Encoder.Encode(data);

            return Draw();
        }

        private System.Drawing.Bitmap Draw()
        {
            DataMatrixDefinition def = ComputeSize();

            ReedSolomon rs = new ReedSolomon(new DataMatrixInitialiser(def));
            _Data = rs.Encode(_Data, 1);

            int row = 0, col = 4;
            bool up = true;
            foreach (byte item in _Data)
            {
                Rectangle[] r = DrawCodeword(item);

                if (row < _Size.Height & col < _Size.Width)
                    Transform(r, row, col);

                if (up)
                {
                    row -= 2;
                    col += 2;

                    up = row >= 0 && col < _Size.Width;
                    if (!up)
                    {
                        row += 3;
                        col += 1;
                    }
                }
                else
                {
                    row += 2;
                    col -= 2;

                    up = row >= _Size.Height && col < 0;

                    if (up)
                    {
                        row += 3;
                        col += 1;
                    }
                }

            }

            throw new NotImplementedException();
        }

        private DataMatrixDefinition ComputeSize()
        {
            int size = _Data.Length;
            if (_Data[_Data.Length - 1] == Encoder.SWITCHASCII)
                size--;

            DataMatrixDefinition def = DataMatrixHelper.Instance.GetDefinitionForSize(size);

            if (def.DataWords < _Data.Length)
            {
                Array.Resize(ref _Data, size);
            }

            return def;
        }

        private Rectangle[] DrawCodeword(byte value)
        {
            int dx = 2, dy = 2;
            byte mask = 1;
            List<Rectangle> result = new List<Rectangle>();

            for (int i = 0; i < 8; i++)
            {
                if ((value & mask) > 0)
                {
                    result.Add(new Rectangle(dx * _Settings.BitSize, dy * _Settings.BitSize, _Settings.BitSize, _Settings.BitSize));
                }
                dx--;
                if (dx < 0)
                {
                    dy--;
                    if (dy == 0)
                        dx = 1;
                    else
                        dx = 2;
                }
                mask <<= 1;
            }

            return result.ToArray();
        }

        private void Transform(Rectangle[] codeword, int row, int col)
        {
            int dx = (row - 3) * _Settings.BitSize;
            int dy = (col - 3) * _Settings.BitSize;

            foreach (Rectangle item in codeword)
            {
                if (item.X + dx < 0)
                    dx += (_Size.Width * _Settings.BitSize);

                if (item.Y + dy < 0)
                    dy += (_Size.Height * _Settings.BitSize);

                item.Offset(dx, dy);
            }
        }
    }
}
