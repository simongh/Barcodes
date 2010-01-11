using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class EAN13 : EAN
    {
        private readonly int[] _DigitGrouping;

        public readonly static EAN13 Instance;

        protected override int[] DigitGrouping
        {
            get { return _DigitGrouping; }
        }

        private EAN13()
            : base()
        {
            _DigitGrouping = new int[] { 1, 6, 6 };
        }

        static EAN13()
        {
            Instance = new EAN13();
        }

        protected override void Init()
        {
            base.Init();

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d{13}$");
        }

    }
}
