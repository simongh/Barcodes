using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    /// <summary>
    /// European Article Number (EAN) 8 digit code
    /// </summary>
    public class EAN8 : EAN
    {
        private readonly int[] _DigitGrouping;

        protected override int[] DigitGrouping
        {
            get { return _DigitGrouping; }
        }

        internal EAN8()
            : base()
        {
            _DigitGrouping = new int[] { 0, 4, 4 };
        }

        protected override void Init()
        {
            base.Init();

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d{8}$");
        }

        protected override void CalculateParity(List<int> codes)
        {
            for (int i = 4; i < codes.Count; i++)
            {
                codes[i] += 20;
            }
        }
    }
}
