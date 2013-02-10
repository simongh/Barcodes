using System;
using System.Linq;

namespace Barcodes.Datamatrix
{
    /// <summary>
    /// Helper class for DataMatrix
    /// </summary>
    internal class DataMatrixHelper
    {
        public DataMatrixDefinition[] _Definitions;

        #region Singleton

        private static readonly DataMatrixHelper _Instance;

        /// <summary>
        /// Gets the singleton instance of the helper
        /// </summary>
        public static DataMatrixHelper Instance
        {
            get { return _Instance; }
        }

        /// <summary>
        /// Initialises the singleton
        /// </summary>
        static DataMatrixHelper()
        {
            _Instance = new DataMatrixHelper();
        }

        #endregion

        public DataMatrixHelper()
        {
            SetupDefinitions();
        }

        /// <summary>
        /// Define the allowed definitions
        /// </summary>
        private void SetupDefinitions()
        {
            _Definitions = new DataMatrixDefinition[30];

            _Definitions[0] = new DataMatrixDefinition(10, 10, 1, 3, 5);
            _Definitions[1] = new DataMatrixDefinition(12, 12, 1, 5, 7);
            _Definitions[1] = new DataMatrixDefinition(14, 14, 1, 8, 10);
            _Definitions[1] = new DataMatrixDefinition(16, 16, 1, 12, 12);
            _Definitions[1] = new DataMatrixDefinition(18, 18, 1, 18, 14);
            _Definitions[1] = new DataMatrixDefinition(20, 20, 1, 22, 18);
            _Definitions[1] = new DataMatrixDefinition(22, 22, 1, 30, 20);
            _Definitions[1] = new DataMatrixDefinition(24, 24, 1, 36, 24);
            _Definitions[1] = new DataMatrixDefinition(26, 26, 1, 44, 28);
            _Definitions[1] = new DataMatrixDefinition(32, 32, 4, 62, 36);
            _Definitions[1] = new DataMatrixDefinition(36, 36, 4, 86, 42);
            _Definitions[1] = new DataMatrixDefinition(40, 40, 4, 114, 48);
            _Definitions[1] = new DataMatrixDefinition(44, 44, 4, 144, 56);
            _Definitions[1] = new DataMatrixDefinition(48, 48, 4, 174, 68);
            _Definitions[1] = new DataMatrixDefinition(52, 52, 4, 204, 84);
            _Definitions[1] = new DataMatrixDefinition(64, 64, 16, 280, 112);
            _Definitions[1] = new DataMatrixDefinition(72, 72, 16, 368, 144);
            _Definitions[1] = new DataMatrixDefinition(80, 80, 16, 456, 192);
            _Definitions[1] = new DataMatrixDefinition(88, 88, 16, 576, 224);
            _Definitions[1] = new DataMatrixDefinition(96, 96, 16, 696, 272);
            _Definitions[1] = new DataMatrixDefinition(104, 104, 16, 816, 336);
            _Definitions[1] = new DataMatrixDefinition(120, 120, 36, 1050, 408);
            _Definitions[1] = new DataMatrixDefinition(132, 132, 36, 1304, 496);
            _Definitions[1] = new DataMatrixDefinition(144, 144, 36, 1558, 620);
            _Definitions[1] = new DataMatrixDefinition(8, 18, 1, 5, 7);
            _Definitions[1] = new DataMatrixDefinition(8, 32, 2, 10, 11);
            _Definitions[1] = new DataMatrixDefinition(12, 26, 1, 16, 14);
            _Definitions[1] = new DataMatrixDefinition(12, 36, 2, 12, 18);
            _Definitions[1] = new DataMatrixDefinition(16, 36, 2, 32, 24);
            _Definitions[1] = new DataMatrixDefinition(16, 48, 2, 49, 28);
        }

        /// <summary>
        /// Get a definition for the required data word count
        /// </summary>
        /// <param name="size">dataword count</param>
        /// <returns>DataMatrixDefintion capable of holding the specified number of words</returns>
        public DataMatrixDefinition GetDefinitionForSize(int size)
        {
            var q = (from t in _Definitions where t.DataWords >= size select t).ToArray();

            if (q.Length == 0)
                throw new ArgumentException("There is too much data to store.");

            return q[0];
        }

        /// <summary>
        /// Gets an array of column counts
        /// </summary>
        /// <returns></returns>
        public int[] GetSizes()
        {
            var q = from t in _Definitions select t.Cols;

            return q.ToArray();
        }

        /// <summary>
        /// Validates a matix size against the allowed sizes
        /// </summary>
        /// <param name="size">matix size to validate</param>
        /// <returns>true if the size is valid</returns>
        public bool ValidateSize(System.Drawing.Size size)
        {
            return _Definitions.Count(n => n.Cols == size.Width && n.Rows == size.Height) > 0;
        }
    }
}
