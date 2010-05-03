using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    /// <summary>
    /// Drawing state used to pass between methods
    /// </summary>
    public class State
    {
        /// <summary>
        /// Gets or sets the current left position in pixels
        /// </summary>
        public int Left
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current top position in pixels
        /// </summary>
        public int Top
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the current canvas
        /// </summary>
        public Graphics Canvas
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the current barcode settings
        /// </summary>
        public BarcodeSettings Settings
        {
            get;
            private set;
        }

        public State(Graphics canvas, BarcodeSettings settings, int left, int top)
        {
            Left = left;
            Top = top;
            Canvas = canvas;
            Settings = settings;
        }
    }
}
