using System;

namespace Barcodes2
{
    /// <summary>
    /// Calculates Reed Solomon Error Correction codes
    /// </summary>
    public class ReedSolomon
    {
//        public const short QRCODE_INITIALISER = 0x11D;
        private const int SYMBOLSIZE = 0xff;

        private byte[] _LogTable;
        private byte[] _AntiLogTable;
        private IInitialiser _Initialiser;
        private CoefficientsCollection _Coefficients;

        public ReedSolomon(IInitialiser initialiser)
        {
            if (initialiser == null)
                throw new ArgumentNullException("initialiser", "Polynomial initialiser cannot be null.");

            _Initialiser = initialiser;
            InitaliseLogTables();
            _Coefficients = new CoefficientsCollection();
        }

        /// <summary>
        /// Initialise the log & anti-log tables
        /// </summary>
        private void InitaliseLogTables()
        {
            _LogTable = new byte[SYMBOLSIZE + 1];
            _AntiLogTable = new byte[SYMBOLSIZE];

            for (int p = 1, v = 0; v < SYMBOLSIZE; v++)
            {
                _AntiLogTable[v] = (byte)p;
                _LogTable[p] = (byte)v;
                p <<= 1;
                if ((p > SYMBOLSIZE))
                    p ^= _Initialiser.G;
            }
        }

        /// <summary>
        /// Initialise the coefficients table according to the desired number of codewords.
        /// </summary>
        /// <param name="numberofSymbols">number of error correction codewords desired</param>
        /// <param name="firstTerm">intilises the polynomial, usually 1</param>
        private void InitialiseCoefficients(int numberofSymbols)
        {
            if (_Coefficients.Contains(numberofSymbols))
                return;

            byte[] tmp = new byte[numberofSymbols + 1];
            int firstTerm = 1;

            tmp[0] = 1;
            for (int i = 1; i <= numberofSymbols; i++)
            {
                tmp[i] = 1;

                for (int k = i - 1; k > 0; k--)
                {
                    if (tmp[k] != 0)
                        tmp[k] = _AntiLogTable[(_LogTable[tmp[k]] + firstTerm) % SYMBOLSIZE];

                    tmp[k] ^= tmp[k - 1];
                }

                tmp[0] = _AntiLogTable[(_LogTable[tmp[0]] + firstTerm) % SYMBOLSIZE];
                firstTerm++;
            }

            byte[] result = new byte[tmp.Length - 1];
            Array.Copy(tmp, result, result.Length);
            _Coefficients.Add(result);
        }

        /// <summary>
        /// Calculates the error correction codewords from the data
        /// </summary>
        /// <param name="data">data to encode</param>
        /// <returns>array of codewords</returns>
        private byte[] GetCodewords(byte[] data, int level)
        {
            int EccCount = _Initialiser.GetECCCount(data.Length, level);
            InitialiseCoefficients(EccCount);

            byte[] result = new byte[EccCount];
            byte[] factors = _Coefficients[EccCount];
            int t = 0;

            for (int i = 0; i < data.Length; i++)
            {
                t = data[i] ^ result[result.Length - 1];

                for (int j = result.Length - 1; j > 0; j--)
                {
                    if (j == 0)
                        result[j] = result[j - 1];
                    else
                        result[j] = (byte)(result[j - 1] ^ _AntiLogTable[(_LogTable[t] + _LogTable[factors[j]]) % SYMBOLSIZE]);
                }

                if ((t & factors[0]) != 0)
                    result[0] = _AntiLogTable[(_LogTable[t] + _LogTable[factors[0]]) % SYMBOLSIZE];
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
        public byte[] Encode(byte[] data, int level)
        {
            byte[] tmp = GetCodewords(data, level);

            byte[] result = new byte[data.Length + tmp.Length];
            data.CopyTo(result, 0);

            for (int i = 0; i < tmp.Length; i++)
            {
                result[data.Length + tmp.Length - i - 1] = tmp[i];
            }

            return result;
        }
    }

    /// <summary>
    /// Defines the initialiser for the Reed Solomon functions
    /// </summary>
    public interface IInitialiser
    {
        int G { get; }
        int GetECCCount(int dataCount, int level);
    }

}
