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
        private static Code93 _Code93;
        private static Code2of5 _Code2of5;
        private static Codabar _Codabar;
        private static Interleaved2of5 _Interleaved2of5;
        private static ExtendedCode3of9 _XCode3of9;
        private static PostNet _PostNet;
        private static CPC _Cpc;

        public static EAN13 Ean13
        {
            get { return GetInstance<EAN13>(ref _Ean13); }
        }

        public static EAN8 Ean8
        {
            get { return GetInstance<EAN8>(ref _Ean8); }
        }

        public static UPC Upc
        {
            get { return GetInstance<UPC>(ref _Upc); }
        }

        public static UPC2 Upc2
        {
            get { return GetInstance<UPC2>(ref _Upc2); }
        }

        public static UPC5 Upc5
        {
            get { return GetInstance<UPC5>(ref _Upc5); }
        }

        public static UPCE UpcE
        {
            get { return GetInstance<UPCE>(ref _UpcE); }
        }

        public static Code3of9 Code3of9
        {
            get { return GetInstance<Code3of9>(ref _Code3of9); }
        }

        public static ExtendedCode3of9 ExtendedCode3of9
        {
            get { return GetInstance<ExtendedCode3of9>(ref _XCode3of9); }
        }

        public static Code128 Code128
        {
            get { return GetInstance<Code128>(ref _Code128); }
        }

        public static Code11 Code11
        {
            get { return GetInstance<Code11>(ref _Code11); }
        }

        public static Code93 Code93
        {
            get { return GetInstance<Code93>(ref _Code93); }
        }

        public static Code2of5 Code2of5
        {
            get { return GetInstance<Code2of5>(ref _Code2of5); }
        }

        public static Codabar Codabar
        {
            get { return GetInstance<Codabar>(ref _Codabar); }
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
                return GetInstance<Interleaved2of5>(ref _Interleaved2of5);
            }
        }

        public static PostNet PostNet
        {
            get { return GetInstance<PostNet>(ref _PostNet); }
        }

        public static CPC Cpc
        {
            get { return GetInstance<CPC>(ref _Cpc); }
        }

        private static T GetInstance<T>(ref T item) where T:BarcodeBase, new()
        {
            if (item != null) return item;

            lock (_thisLock)
            {
                if (item != null)
                    return item;

                item = new T();

                return item;
            }
        }
    }
}
