using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcodes.Datamatrix
{
    /// <summary>
    /// Initialiser for the Reed Solomon calculator
    /// </summary>
    public class DataMatrixInitialiser : IInitialiser
    {
        private DataMatrixDefinition _Definition;

        /// <summary>
        /// Gets the polynomial value
        /// </summary>
        public int G
        {
            get { return 0x012D; }
        }

        /// <summary>
        /// Initialise the class for the specified definition
        /// </summary>
        /// <param name="definition">Definition to use</param>
        internal DataMatrixInitialiser(DataMatrixDefinition definition)
        {
            _Definition = definition;
        }

        /// <summary>
        /// Gets the ECC word count for the given size & error correction level
        /// </summary>
        /// <param name="dataCount">Data word count</param>
        /// <param name="level">Error correction level. For DataMatrix, this must be 1</param>
        /// <returns>the number of ECC words to calculate</returns>
        public int GetECCCount(int dataCount, int level)
        {
            if (level != 1)
                throw new ArgumentOutOfRangeException("DataMatrix supports only one level of error correction.");

            return _Definition.EccWords;
        }
    }
}
