using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Barcodes
{
    /// <summary>
    /// Elements of a pattern
    /// </summary>
    public enum Elements
    {
        WideBlack,
        WideWhite,
        NarrowBlack,
        NarrowWhite,
        Tracker,
        Ascender,
        Descender
    }

    /// <summary>
    /// An individual module or pattern in a barcode
    /// </summary>
    public class Pattern
    {
        private Elements[] _State;

        /// <summary>
        /// Gets or sets the count of wide bars in the module
        /// </summary>
        internal int WideCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the count of narrow bars in the module
        /// </summary>
        internal int NarrowCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the count of black bars in the module
        /// </summary>
        internal int BlackCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the count of white bars in the module
        /// </summary>
        internal int WhiteCount
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
                if ((int)item % 2 == 0)
                    BlackCount++;
                else
                    WhiteCount++;
            }
        }

        private Pattern()
        {
        }

        /// <summary>
        /// Parse a textual pattern description into a module
        /// (0 - white, 1 - black)
        /// (ww - wide white, wb - wide black, nw - narrow white, nb - narrow black)
        /// (t - tracker, a - ascender, d - descender, f - full height)
        /// </summary>
        /// <param name="pattern">string to encode</param>
        /// <returns>pattern object</returns>
        public static Pattern Parse(string pattern)
        {
            Pattern result = new Pattern();

            if (Regex.IsMatch(pattern, "^[01]+$"))
                result.ParseBinary(pattern);
            else if (Regex.IsMatch(pattern, "^((0|1|[wn][wb]) ?)+$"))
                result.ParseFull(pattern);
            else if (Regex.IsMatch(pattern, "^[tadf]+$"))
                result.ParsePost(pattern);

            return result;
        }

        /// <summary>
        /// Parse the pattern using a space seperated description
        /// </summary>
        /// <param name="pattern">pattern to encode</param>
        private void ParseFull(string pattern)
        {
            string[] parts = pattern.Split(' ');
            _State = new Elements[parts.Length];
            
            for (int i = 0; i < parts.Length; i++)
            {
                switch (parts[i])
                {
                    case "ww":
                        AddBar(Elements.WideWhite, i);
                        break;
                    case "wb":
                        AddBar(Elements.WideBlack, i);
                        break;
                    case "0":
                    case "nw":
                        AddBar(Elements.NarrowWhite, i);
                        break;
                    case "1":
                    case "nb":
                        AddBar(Elements.NarrowBlack, i);
                        break;
                    default:
                        throw new ApplicationException("Unknown pattern element.");
                }
            }

        }

        /// <summary>
        /// Parse using a binary pattern
        /// </summary>
        /// <param name="pattern">pattern to encode</param>
        private void ParseBinary(string value)
        {
            _State = new Elements[value.Length];

            for (int i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case '0':
                        AddBar(Elements.NarrowWhite, i);
                        break;
                    case '1':
                        AddBar(Elements.NarrowBlack, i);
                        break;
                }
            }

        }

        /// <summary>
        /// Parse using 4-state encodings
        /// </summary>
        /// <param name="pattern">pattern to encode</param>
        private void ParsePost(string pattern)
        {
            _State = new Elements[(pattern.Length)];

            for (int i = 0; i < pattern.Length; i++)
            {
                switch (pattern[i])
                {
                    case't':
                        AddBar(Elements.Tracker, i);
                        break;
                    case'a':
                        AddBar(Elements.Ascender, i);
                        break;
                    case 'd':
                        AddBar(Elements.Descender, i);
                        break;
                    case 'f':
                        AddBar(Elements.NarrowBlack, i);
                        break;
                }

                //AddBar(Elements.WideWhite, (i * 2) + 1);

            }
        }

        /// <summary>
        /// Add a bar to this pattern
        /// </summary>
        /// <param name="bar">bar to add</param>
        /// <param name="index">index to add bar at</param>
        private void AddBar(Elements bar, int index)
        {
            _State[index] = bar;

            switch (bar)
            {
                case Elements.WideBlack:
                    WideCount++;
                    BlackCount++;
                    break;
                case Elements.WideWhite:
                    WideCount++;
                    WhiteCount++;
                    break;
                case Elements.NarrowWhite:
                    NarrowCount++;
                    WhiteCount++;
                    break;
                case Elements.Tracker:
                case Elements.Ascender:
                case Elements.Descender:
                case Elements.NarrowBlack:
                    NarrowCount++;
                    BlackCount++;
                    break;
            }
        }

        //public static Pattern Parse(char[] pattern)
        //{
        //    Pattern result = new Pattern();
        //    result._State = new Elements[pattern.Length];
        //    result.NarrowCount = pattern.Length;

        //    for (int i = 0; i < pattern.Length; i++)
        //    {
        //        if (pattern[i] == '0')
        //            result._State[i] = Elements.NarrowWhite;
        //        else if (pattern[i] == '1')
        //            result._State[i] = Elements.NarrowBlack;
        //        else
        //            throw new ApplicationException("Unknown pattern element found.");
        //    }

        //    return result;
        //}

        /// <summary>
        /// Draw the module as series of black rectangles
        /// </summary>
        /// <param name="settings">settings to use</param>
        /// <returns>array of black rectangles</returns>
        internal Rectangle[] Paint(BarcodeSettings settings)
        {
            List<Rectangle> result = new List<Rectangle>();

            int offset = (settings.MediumHeight - settings.ShortHeight);
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
                    case Elements.Tracker:
                        rect = new Rectangle(left, offset, settings.NarrowWidth, settings.ShortHeight);
                        left += settings.NarrowWidth;
                        result.Add(rect);
                        break;
                    case Elements.Ascender:
                        rect = new Rectangle(left, 0, settings.NarrowWidth, settings.MediumHeight);
                        left += settings.NarrowWidth;
                        result.Add(rect);
                        break;
                    case Elements.Descender:
                        rect = new Rectangle(left, offset, settings.NarrowWidth, settings.MediumHeight);
                        left += settings.NarrowWidth;
                        result.Add(rect);
                        break;
                }                
            }

            return result.ToArray();
        }
    }
}
