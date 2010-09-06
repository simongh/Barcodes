using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer.Datamatrix
{
    public class DataMatrix
    {
        private Encoder _Encoder;
        private bool _IsSquare;
        private System.Drawing.Size _Size;
        private byte[] _Data;

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
        }

        private DataMatrixDefinition ComputeSize()
        {
            throw new NotImplementedException();
        }
    }
}
