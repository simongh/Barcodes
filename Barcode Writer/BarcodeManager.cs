using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer
{
    public static class BarcodeManager
    {
        private static object _thisLock = new object();
        
        private static EAN13 _Ean13;
        private static EAN8 _Ean8;
        private static UPC _Upc;
        private static UPC2 _Upc2;
        private static UPC5 _Upc5;
        private static UPCE _UpcE;
        private static Code3of9 _Code3of9;
        private static Code128 _Code128;
        private static Code11 _Code11;
        private static Codabar _Codabar;
        private static Interleaved2of5 _Interleaved2of5;

        public static EAN13 Ean13
        {
            get
            {
                if (_Ean13==null)
                    lock (_thisLock)
                    {
                        if (_Ean13 == null)
                            _Ean13 = new EAN13();
                    }

                return _Ean13;
            }
        }

        public static EAN8 Ean8
        {
            get
            {
                if (_Ean8 == null)
                    lock (_thisLock)
                    {
                        if (_Ean8 == null)
                            _Ean8 = new EAN8();
                    }

                return _Ean8;
            }
        }

        public static UPC Upc
        {
            get
            {
                if (_Upc == null)
                    lock (_thisLock)
                    {
                        if (_Upc == null)
                            _Upc = new UPC();
                    }

                return _Upc;
            }
        }

        public static UPC2 Upc2
        {
            get
            {
                if (_Upc2 == null)
                    lock (_thisLock)
                    {
                        if (_Upc2 == null)
                            _Upc2 = new UPC2();
                    }

                return _Upc2;
            }
        }

        public static UPC5 Upc5
        {
            get
            {
                if (_Upc5 == null)
                    lock (_thisLock)
                    {
                        if (_Upc5 == null)
                            _Upc5 = new UPC5();
                    }

                return _Upc5;
            }
        }

        public static UPCE UpcE
        {
            get
            {
                if (_UpcE == null)
                    lock (_thisLock)
                    {
                        if (_UpcE == null)
                            _UpcE = new UPCE();
                    }

                return _UpcE;
            }
        }

        public static Code3of9 Code3of9
        {
            get
            {
                if (_Code3of9 == null)
                    lock (_thisLock)
                    {
                        if (_Code3of9 == null)
                            _Code3of9 = new Code3of9();
                    }

                return _Code3of9;
            }
        }

        public static Code128 Code128
        {
            get
            {
                if (_Code128 == null)
                    lock (_thisLock)
                    {
                        if (_Code128 == null)
                            _Code128 = new Code128();
                    }

                return _Code128;
            }
        }

        public static Code11 Code11
        {
            get
            {
                if (_Code11 == null)
                    lock (_thisLock)
                    {
                        if (_Code11 == null)
                            _Code11 = new Code11();
                    }

                return _Code11;
            }
        }

        public static Codabar Codabar
        {
            get
            {
                if (_Codabar == null)
                    lock (_thisLock)
                    {
                        if (_Codabar == null)
                            _Codabar = new Codabar();
                    }

                return _Codabar;
            }
        }

        public static Codabar Code2of7
        {
            get
            {
                return Codabar;
            }
        }

        public static Interleaved2of5 Interleaved2of5
        {
            get
            {
                if (_Interleaved2of5 == null)
                    lock (_thisLock)
                    {
                        if (_Interleaved2of5 == null)
                            _Interleaved2of5 = new Interleaved2of5();
                    }

                return _Interleaved2of5;
            }
        }
    }
}
