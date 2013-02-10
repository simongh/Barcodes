using System;

namespace Barcodes
{
    /// <summary>
    /// European Article Number (EAN) 13 digit code
    /// </summary>
    public class EAN13 : EAN
    {
        private readonly int[] _DigitGrouping;

        protected override int[] DigitGrouping
        {
            get { return _DigitGrouping; }
        }

        public EAN13()
            : base()
        {
            _DigitGrouping = new int[] { 1, 6, 6 };
        }

        protected override void Init()
        {
            base.Init();

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d{12,13}$");
            AddChecksum += new EventHandler<AddChecksumEventArgs>(EAN13_AddChecksum);
        }

        void EAN13_AddChecksum(object sender, AddChecksumEventArgs e)
        {
            if (e.Codes.Count == 13)
                return;

            int total =0;
            for (int i = 0; i < e.Codes.Count; i++)
            {
                if (i % 2 == 0)
                    total += (e.Codes[i] % 10);
                else
                    total += 3 * (e.Codes[i] % 10);
            }

            total = total % 10;
            e.Codes.Add(total == 0 ? 20 : 30 - total);
            e.Text += (total == 0 ? 0 : 10 - total).ToString();
        }

    }
}
