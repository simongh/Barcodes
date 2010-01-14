using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public class State
    {
        public int Left
        {
            get;
            set;
        }

        public int Top
        {
            get;
            set;
        }

        public Graphics Canvas
        {
            get;
            private set;
        }

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
