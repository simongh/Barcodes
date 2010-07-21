using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public class Interleaved2of5 : BarcodeBase
    {
        private const int STARTMARKER = 101;
        private const int ENDMARKER = 102;

        protected override void Init()
        {
            PatternSet = new Dictionary<int, Pattern>();

            MakePatterns();

            PatternSet.Add(STARTMARKER, Pattern.Parse("nb nw nb nw"));
            PatternSet.Add(ENDMARKER, Pattern.Parse("wb nw nb"));

            AllowedCharsPattern = new System.Text.RegularExpressions.Regex("^(\\d\\d)+$");
        }

        private void MakePatterns()
        {
            string[] widths = { "nnwwn", "wnnnw", "nwnnw", "wwnnn", "nnwnw", "wnwnn", "nwwnn", "nnnww", "wnnwn", "nwnwn" };

            StringBuilder pattern = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                pattern.Clear();

                for (int j = 0; j < 5; j++)
                {
                    pattern.Append(widths[i / 10][j]);
                    pattern.Append("b ");
                    pattern.Append(widths[i % 10][j]);
                    pattern.Append("w ");
                }

                pattern.Remove(pattern.Length - 1, 1);

                PatternSet.Add(i, Pattern.Parse(pattern.ToString()));
            }
        }

        protected override string ParseText(string value, List<int> codes)
        {
            if (!IsValidData(value))
                throw new ApplicationException();

            codes.Add(STARTMARKER);

            for (int i = 0; i < value.Length; i+=2)
            {
                codes.Add(int.Parse(value.Substring(i, 2)));
            }

            codes.Add(ENDMARKER);

            return value;
        }

        protected override int GetModuleWidth(BarcodeSettings settings)
        {
            return (4 * settings.WideWidth) + (6 * settings.NarrowWidth);
        }

        protected override int OnCalculateWidth(int width, BarcodeSettings settings, List<int> codes)
        {
            return width - (2 * GetModuleWidth(settings)) + (6 * settings.NarrowWidth) + (settings.WideWidth);
        }

        protected override void OnAfterDrawModule(State state, int index)
        {
            if (state.ModuleValue == STARTMARKER)
            {
                state.Left += (4 * state.Settings.NarrowWidth);
            }
            else
                base.OnAfterDrawModule(state, index);
        }
    }
}
