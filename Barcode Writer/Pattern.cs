using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public enum Elements
    {
        WideBlack,
        WideWhite,
        NarrowBlack,
        NarrowWhite
    }

    public class Pattern
    {
        private Elements[] _State;

        internal int WideCount
        {
            get;
            set;
        }

        internal int NarrowCount
        {
            get;
            set;
        }

        public Pattern(Elements[] pattern)
            : this()
        {
            _State = pattern;

            foreach (Elements item in _State)
            {
                if ((int)item > 1)
                    NarrowCount++;
                else
                    WideCount++;
            }
        }

        private Pattern()
        {
        }

        public static Pattern Parse(string pattern)
        {
            string[] parts = pattern.Split(' ');

            Pattern result = new Pattern();
            result._State = new Elements[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                switch (parts[i])
                {
                    case "ww":
                        result._State[i] = Elements.WideWhite;
                        result.WideCount++;
                        break;
                    case"wb":
                        result._State[i] = Elements.WideBlack;
                        result.WideCount++;
                        break;
                    case "nw":
                        result._State[i] = Elements.NarrowWhite;
                        result.NarrowCount++;
                        break;
                    case "nb":
                        result._State[i] = Elements.NarrowBlack;
                        result.NarrowCount++;
                        break;
                    default:
                        throw new ApplicationException("Unknown pattern element.");
                }
            }

            return result;
        }

        internal Rectangle[] Paint(BarcodeSettings settings)
        {
            List<Rectangle> result = new List<Rectangle>();

            int left = 0;
            Rectangle rect;
            foreach (Elements item in _State)
            {
                switch (item)
                {
                    case Elements.WideBlack:
                        rect = new Rectangle(left, 0, settings.WideWidth, settings.BarHeight);
                        left += settings.WideWidth;

                        result.Add(rect);
                        break;
                    case Elements.WideWhite:
                        left += settings.WideWidth;
                        break;
                    case Elements.NarrowBlack:
                        rect = new Rectangle(left, 0, settings.NarrowWidth, settings.BarHeight);
                        left += settings.NarrowWidth;

                        result.Add(rect);
                        break;
                    case Elements.NarrowWhite:
                        left += settings.NarrowWidth;
                        break;
                    default:
                        break;
                }                
            }

            return result.ToArray();
        }
    }
}
