using Barcodes2;
using System;

namespace Barcodes.Writer
{
    public static class BarcodeWriter
    {
        private static Drawing.Bitmap _bitmap = new();

        private static ReadOnlySpan<byte> Generate(BarcodeSettings? settings, BaseDefinition definition, string value)
        {
            if (definition.TryParse(value, out var codes))
            {
                return _bitmap.Create(definition, codes, settings ?? new());
            }
            else
                throw new BarcodeException($"'{value} is invalid data for this symbology");
        }

        public static ReadOnlySpan<byte> Codabar(string value, BarcodeSettings? settings = null) => Generate(settings, new Definitions.Codabar(), value);

        public static ReadOnlySpan<byte> Code11(string value, BarcodeSettings? settings = null) => Generate(settings, new Definitions.Code11(), value);

        public static ReadOnlySpan<byte> Code128(string value, BarcodeSettings? settings = null) => Generate(settings, new Definitions.Code128(), value);

        public static ReadOnlySpan<byte> Code2of5(string value, BarcodeSettings? settings = null) => Generate(settings, new Definitions.Code2of5(), value);

        public static ReadOnlySpan<byte> Code3of9(string value, BarcodeSettings? settings = null) => Generate(settings, new Definitions.Code3of9(), value);

        public static ReadOnlySpan<byte> Code93(string value, BarcodeSettings? settings = null) => Generate(settings, new Definitions.Code93(), value);

        public static ReadOnlySpan<byte> Interleaved2of5(string value, BarcodeSettings? settings = null) => Generate(settings, new Definitions.Interleaved2of5(), value);

        public static ReadOnlySpan<byte> Postnet(string value, BarcodeSettings? settings = null) => Generate(settings ?? BarcodeSettings.Postal(), new Definitions.Postnet(), value);

        public static ReadOnlySpan<byte> RM4SCC(string value, BarcodeSettings? settings = null) => Generate(settings ?? BarcodeSettings.Postal(), new Definitions.RM4SCC(), value);

        public static ReadOnlySpan<byte> Cpc(string value, BarcodeSettings? settings = null) => Generate(settings ?? BarcodeSettings.Postal(), new Definitions.Cpc(), value);

        public static ReadOnlySpan<byte> IntelligentMail(string value, BarcodeSettings? settings = null) => Generate(settings ?? BarcodeSettings.Postal(), new Definitions.IntelligentMail(), value);
    }
}