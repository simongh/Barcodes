using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    /// <summary>
    /// Universal Product Code (UPC) 12 digit code
    /// Also known as UPC-A
    /// </summary>
    public class UPC : EAN
    {
        private readonly int[] _DigitGrouping;

        public readonly static UPC Instance;

        protected override int[] DigitGrouping
        {
            get { return _DigitGrouping; }
        }

        private UPC()
            : base()
        {
            _DigitGrouping = new int[] { 0, 6, 6 };
        }

        static UPC()
        {
            Instance = new UPC();
        }

        protected override void Init()
        {
            base.Init();
            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d{12}$");
        }

        protected override void CalculateParity(List<int> codes)
        {
            codes.Insert(0, 0);
            base.CalculateParity(codes);
        }
    }
}
