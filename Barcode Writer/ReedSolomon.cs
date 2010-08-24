using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class ReedSolomon
    {
        public const short DATAMATRIX_INITIALISER = 0x012D;
        public const short QRCODE_INITIALISER = 0x11D;
        private const int SYMBOLSIZE = 0xff;

        private byte[] _LogTable;
        private byte[] _AntiLogTable;
        private byte[] _Coefficients;
        private short _G;

        public int CorrectionCodewords
        {
            get { return _Coefficients.Length - 1; }
            set
            {
                InitialiseCoefficients(value, 1);
            }
        }

        public ReedSolomon(short g, int correctionCodewords)
        {
            if (g != DATAMATRIX_INITIALISER && g != QRCODE_INITIALISER)
                throw new ArgumentException("Unsupported polynomial initialiser defined.");

            _G = (short)g;
            InitaliseLogTables();
            InitialiseCoefficients(correctionCodewords, 1);
        }

        /// <summary>
        /// Initialise the log & anti-log tables
        /// </summary>
        private void InitaliseLogTables()
        {
            _LogTable = new byte[SYMBOLSIZE + 1];
            _AntiLogTable = new byte[SYMBOLSIZE];

            for (short p = 1, v = 0; v < SYMBOLSIZE; v++)
            {
                _AntiLogTable[v] = (byte)p;
                _LogTable[p] = (byte)v;
                p <<= 1;
                if ((p > SYMBOLSIZE))
                    p ^= _G;
            }
        }

        /// <summary>
        /// Initialise the coefficients table according to the desired number of codewords.
        /// </summary>
        /// <param name="numberofSymbols">number of error correction codewords desired</param>
        /// <param name="firstTerm">intilises the polynomial, usually 1</param>
        private void InitialiseCoefficients(int numberofSymbols, int firstTerm)
        {
            _Coefficients = new byte[numberofSymbols + 1];

            _Coefficients[0] = 1;
            for (int i = 1; i <= numberofSymbols; i++)
            {
                _Coefficients[i] = 1;

                for (int k = i - 1; k > 0; k--)
                {
                    if (_Coefficients[k] != 0)
                        _Coefficients[k] = _AntiLogTable[(_LogTable[_Coefficients[k]] + firstTerm) % SYMBOLSIZE];

                    _Coefficients[k] ^= _Coefficients[k - 1];
                }

                _Coefficients[0] = _AntiLogTable[(_LogTable[_Coefficients[0]] + firstTerm) % SYMBOLSIZE];
                firstTerm++;
            }
        }

        /// <summary>
        /// Calculates the error correction codewords from the data
        /// </summary>
        /// <param name="data">data to encode</param>
        /// <returns>array of codewords</returns>
        private byte[] GetCodewords(byte[] data)
        {
            //int CorrectionCWLength = _Coefficients.Length - 1;
            byte[] result = new byte[_Coefficients.Length - 1];
            int t = 0;

            for (int i = 0; i < data.Length; i++)
            {
                t = data[i] ^ result[result.Length - 1];

                for (int j = result.Length - 1; j > 0; j--)
                {
                    if (j == 0)
                        result[j] = result[j - 1];
                    else
                        result[j] = (byte)(result[j - 1] ^ _AntiLogTable[(_LogTable[t] + _LogTable[_Coefficients[j]]) % SYMBOLSIZE]);
                }

                if ((t & _Coefficients[0]) != 0)
                    result[0] = _AntiLogTable[(_LogTable[t] + _LogTable[_Coefficients[0]]) % SYMBOLSIZE];
                else
                    result[0] = 0;
            }

            return result;

        }

        /// <summary>
        /// Take a data array of bytes and adds the error codes to the end.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encode(byte[] data)
        {
            return Encode(data, data.Length + _Coefficients.Length - 1);
        }

        public byte[] Encode(byte[] data, int length)
        {
            byte[] result = new byte[length];
            data.CopyTo(result, 0);

            byte[] tmp = GetCodewords(data);

            for (int i = 0; i < tmp.Length; i++)
            {
                result[data.Length + tmp.Length - i - 1] = tmp[i];
            }

            return result;

        }
    }

    public interface ICodewordInitialiser
    {
        short G
        {
            get;
        }
        int CorrectionCodewords
        {
            get;
        }
        bool ValidateCodewordCount(int dataCount);
    }
}
