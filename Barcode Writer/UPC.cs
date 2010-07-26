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

         protected override int[] DigitGrouping
        {
            get { return _DigitGrouping; }
        }

        public UPC()
            : base()
        {
            _DigitGrouping = new int[] { 0, 6, 6 };
        }

        protected override void Init()
        {
            base.Init();
            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d{11,12}$");

            AddChecksum += new EventHandler<AddChecksumEventArgs>(UPC_AddChecksum);
        }

        void UPC_AddChecksum(object sender, AddChecksumEventArgs e)
        {
            if (e.Codes.Count == 12)
                return;

            int total = 0;
            for (int i = 0; i < e.Codes.Count; i++)
            {
                if (i % 2 == 1)
                    total += (e.Codes[i] % 10);
                else
                    total += 3 * (e.Codes[i] % 10);
            }

            total = total % 10;
            e.Codes.Add(total == 0 ? 20 : 30 - total);
            e.Text += (total == 0 ? 0 : 10 - total).ToString();

        }

        protected override void CalculateParity(CodedValueCollection codes)
        {
            codes.Insert(0, 0);
            base.CalculateParity(codes);
        }
    }
}
