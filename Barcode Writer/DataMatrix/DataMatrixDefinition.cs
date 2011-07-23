using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcodes.Datamatrix
{
    /// <summary>
    /// Holds the defination for DataMatrix layout
    /// </summary>
    internal class DataMatrixDefinition
    {
        /// <summary>
        /// Gets the number of data rows
        /// </summary>
        public int Rows
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of data columns
        /// </summary>
        public int Cols
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of data regions
        /// </summary>
        public int Regions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the data word capacity
        /// </summary>
        public int DataWords
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the ECC word count
        /// </summary>
        public int EccWords
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialise a new definition
        /// </summary>
        /// <param name="rows">row count</param>
        /// <param name="cols">column count</param>
        /// <param name="regions">region count</param>
        /// <param name="datawords">data word count</param>
        /// <param name="eccwords">ecc word count</param>
        public DataMatrixDefinition(int rows, int cols, int regions, int datawords, int eccwords)
        {
            Rows = rows;
            Cols = cols;
            Regions = regions;
            DataWords = datawords;
            EccWords = eccwords;
        }
    }
}
