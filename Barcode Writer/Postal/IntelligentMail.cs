using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Barcode_Writer
{
    /// <summary>
    /// USPS Intelligent Mail barcode
    /// </summary>
    public class IntelligentMail : BarcodeBase
    {
        protected override void Init()
        {
            DefaultSettings.IsTextShown = false;
            DefaultSettings.BarHeight = 12;
            DefaultSettings.ShortHeight = 4;
            DefaultSettings.MediumHeight = 8;
            DefaultSettings.IsChecksumCalculated = false;
            DefaultSettings.ModulePadding = DefaultSettings.NarrowWidth;
        }

        protected override void CreatePatternSet()
        {
            PatternSet = new Dictionary<int, Pattern>();

            PatternSet.Add(IntelligentMailHelper.ASCENDER, Pattern.Parse("a"));
            PatternSet.Add(IntelligentMailHelper.DESCENDER, Pattern.Parse("d"));
            PatternSet.Add(IntelligentMailHelper.FULLBAR, Pattern.Parse("f"));
            PatternSet.Add(IntelligentMailHelper.TRACKER, Pattern.Parse("t"));
        }

        public override bool IsValidData(string value)
        {
            value = Regex.Replace(value,"[-\\s]","");

            if (!Regex.IsMatch(value, @"^\d[0-4]\d{3}"))
                throw new ApplicationException("The barcode identifier or service type was invalid.");

            if (!Regex.IsMatch(value, @"^\d{5}([0-8]\d{5}\d{9}|9\d{8}\d{6})"))
                throw new ApplicationException("The customer identifer or sequence number were invalid.");

            if (!Regex.IsMatch(value, @"^\d{20}(\d{5}(\d{4}(\d{2})?)?)?$"))
                throw new ApplicationException("The delivery point ZIP code was invalid.");

            return true;
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            value = base.ParseText(value, codes);
            value = Regex.Replace(value, "[-\\s]", "");

            byte[] data1 = IntelligentMailHelper.Instance.ConvertToBytes(IntelligentMailHelper.Instance.ConvertRoutingCode(value.Substring(20)), value.Substring(0,20));
            int fcs = IntelligentMailHelper.Instance.CRC11(data1);

            short[] data2 = IntelligentMailHelper.Instance.ConvertToCodewords(data1);

            if (IntelligentMailHelper.Instance.CheckFcs(fcs))
                data2[0] += 659;

            IntelligentMailHelper.Instance.ConvertToCharacters(data2);

            IntelligentMailHelper.Instance.IncludeFcs(data2, fcs);

            codes.AddRange(IntelligentMailHelper.Instance.ConvertToBars(data2));

            return null;
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, CodedValueCollection codes)
        {
            width = settings.NarrowWidth * (codes.Count - 1);
            return base.OnCalculateWidth(width, settings, codes);
        }
    }
}
