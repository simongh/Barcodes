using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcodes
{
    public class AddChecksumEventArgs : EventArgs
    {
        public string Text
        {
            get;
            set;
        }

        public CodedValueCollection Codes
        {
            get;
            private set;
        }

        public AddChecksumEventArgs(string text, CodedValueCollection codes)
        {
            Text = text;
            Codes = codes;
        }
    }
}
