using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    /// <summary>
    /// PostNet barcode used by the US post office
    /// </summary>
    public class PostNet : BarcodeBase
    {
        private const int STARTSTOP = 11;

        protected override void Init()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add(0, Pattern.Parse("aattt"));
            PatternSet.Add(1, Pattern.Parse("tttaa"));
            PatternSet.Add(2, Pattern.Parse("ttata"));
            PatternSet.Add(3, Pattern.Parse("ttaat"));
            PatternSet.Add(4, Pattern.Parse("tatta"));
            PatternSet.Add(5, Pattern.Parse("tatat"));
            PatternSet.Add(6, Pattern.Parse("taatt"));
            PatternSet.Add(7, Pattern.Parse("attta"));
            PatternSet.Add(8, Pattern.Parse("attat"));
            PatternSet.Add(9, Pattern.Parse("atatt"));

            PatternSet.Add(STARTSTOP, Pattern.Parse("f"));

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex(@"^\d{5}((\s|-)?\d{4}((\s|-)?\d{2})?)?$");

            AddChecksum += new EventHandler<AddChecksumEventArgs>(Postnet_AddChecksum);
        }

        void Postnet_AddChecksum(object sender, AddChecksumEventArgs e)
        {
            int total = 0;
            for (int i = 1; i < e.Codes.Count - 1; i++)
            {
                total += e.Codes[i];
            }

            total = total % 10;

            e.Codes.Insert(e.Codes.Count - 2, total == 0 ? 0 : 10 - total);
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            value = base.ParseText(value, codes);
            
            value = value.Replace(" ", "").Replace("-", "");

            for (int i = 0; i < value.Length; i++)
            {
                codes.Add(int.Parse(value.Substring(i, 1)));
            }

            codes.Insert(0, STARTSTOP);
            codes.Add(STARTSTOP);

            return value;
        }

        protected override int GetModuleWidth(BarcodeSettings settings)
        {
            return (settings.NarrowWidth + settings.WideWidth) * 5;
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
        {
            return base.OnCalculateWidth(width, settings, codes) + (2 * settings.NarrowWidth) + settings.WideWidth;
        }

        public override BarcodeSettings GetDefaultSettings()
        {
            BarcodeSettings s = base.GetDefaultSettings();
            s.ModulePadding = 0;
            s.IsTextShown = false;
            s.IsChecksumCalculated = true;

            return s;
        }
    }
}
