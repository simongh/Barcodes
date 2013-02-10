using System;

namespace Barcodes
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

        public EAN8()
            : base()
        {
            _DigitGrouping = new int[] { 0, 4, 4 };
        }

        protected override void Init()
        {
            base.Init();

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^\\d{7,8}$");

            AddChecksum += new EventHandler<AddChecksumEventArgs>(EAN8_AddChecksum);
        }

        void EAN8_AddChecksum(object sender, AddChecksumEventArgs e)
        {
            if (e.Codes.Count == 8)
                return;

            int total=0;
            for (int i = 0; i < e.Codes.Count; i++)
            {
                if (i % 2 == 0)
                    total += e.Codes[i] * 3;
                else
                    total += e.Codes[i];
            }

            total = total % 10;
            e.Codes.Add(total == 0 ? 20 : 30 - total);
            e.Text += (total == 0 ? 0 : 10 - total).ToString();
        }

        protected override void CalculateParity(CodedValueCollection codes)
        {
            for (int i = 4; i < codes.Count; i++)
            {
                codes[i] += 20;
            }
        }
    }
}
