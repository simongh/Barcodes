using Barcodes2;
using SkiaSharp;
using System;
using System.Drawing;

namespace Barcodes.Writer.Drawing
{
    public class Bitmap
    {
        public ReadOnlySpan<byte> Create(BaseDefinition definition, CodedCollection code, BarcodeSettings settings)
        {
            var size = GetDimensions(settings, definition, code);

            var image = new SKBitmap(size.Width, size.Height);
            using var canvas = new SKCanvas(image);
            canvas.Clear(SKColors.White);

            Paint(settings, definition, code, canvas, 0);
            DrawText(settings, definition, code.Value, canvas);

            using var data = image.Encode(SKEncodedImageFormat.Png, 100);

            return data.AsSpan();
        }

        private Size GetDimensions(BarcodeSettings settings, BaseDefinition definition, CodedCollection code)
        {
            var width = definition.CalculateWidth(settings, code) + settings.LeftMargin + settings.RightMargin;
            var height = settings.BarHeight + settings.TopMargin + settings.BottomMargin;

            if (definition.IsTextShown && settings.IsTextShown)
            {
                height += settings.TextPadding + settings.TextHeight;
            }

            if (settings.MaxWidth > 0 && width > settings.MaxWidth)
                throw new BarcodeException("The barcode width exceeds the maximum allowed width");
            if (settings.MaxHeight > 0 && height > settings.MaxHeight)
                throw new BarcodeException("The barcode height exceeds the maximum allowed height");

            if (settings.Width > 0)
            {
                if (settings.Width < width)
                    throw new BarcodeException("The barcode width is larger than the defined width");

                settings.LeftMargin = (settings.Width - width) / 2;
                settings.RightMargin = settings.Width - width - settings.LeftMargin;
                width = settings.Width;
            }
            if (settings.Height > 0)
            {
                if (settings.Height < height)
                    throw new BarcodeException("The barcode height is larger than the defined height");
                settings.TopMargin = (settings.Height - height) / 2;
                settings.BottomMargin = settings.Height - height - settings.TopMargin;
                height = settings.Height;
            }

            return new Size((int)Math.Ceiling(width * settings.Scale), (int)Math.Ceiling(height * settings.Scale));
        }

        private void Paint(BarcodeSettings settings, BaseDefinition definition, CodedCollection code, SKCanvas canvas, int width)
        {
            var left = settings.LeftMargin;

            foreach (var codeItem in code)
            {
                var start = new SKPoint(left, settings.TopMargin);
                left = DrawPattern(settings, codeItem, canvas, start);
            }
        }

        private int DrawPattern(BarcodeSettings settings, Pattern pattern, SKCanvas canvas, SKPoint start)
        {
            var left = start.X;
            foreach (var item in pattern.Elements)
            {
                int width;
                if ((item & Element.Wide) == Element.Wide)
                    width = settings.WideWidth;
                else
                    width = settings.NarrowWidth;

                var isBlack = (item & Element.Black) == Element.Black;

                if (isBlack)
                {
                    var paint = new SKPaint
                    {
                        Color = SKColors.Black,
                        Style = SKPaintStyle.Fill,
                    };
                    canvas.DrawRect(left, start.Y, width, settings.BarHeight, paint);
                }

                left += width;
            }

            return (int)(left + settings.ModulePadding);
        }

        private void DrawText(BarcodeSettings settings, BaseDefinition definition, string text, SKCanvas canvas)
        {
            if (!settings.IsTextShown || !definition.IsTextShown)
                return;

            using var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                TextSize = settings.TextHeight * 1.25F,
                Typeface = SKTypeface.FromFamilyName("GenericMonospace"),
            };

            var rect = new SKRect();
            paint.MeasureText(text, ref rect);

            var bounds = canvas.DeviceClipBounds;

            var x = (bounds.Width / 2) - (rect.Width / 2);
            var y = bounds.Height - settings.BottomMargin;

            canvas.DrawText(text, x, y, paint);
        }
    }
}