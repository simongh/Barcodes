using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    /// <summary>
    /// Stores coefficients used by the Reed Solomon function
    /// </summary>
    internal class CoefficientsCollection : System.Collections.ObjectModel.KeyedCollection<int, byte[]>
    {
        protected override int GetKeyForItem(byte[] item)
        {
            return item.Length;
        }
    }
}
