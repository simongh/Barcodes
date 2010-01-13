using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class EAN8 : EAN
    {
        private readonly int[] _DigitGrouping;
        public static readonly EAN8 Instance;

        protected override int[] DigitGrouping
        {
            get { return _DigitGrouping; }
        }

        protected EAN8()
            : base()
        {
            _DigitGrouping = new int[] { 0, 4, 4 };
        }

        static EAN8()
        {
            Instance = new EAN8();
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
