using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Barcodes;
using Barcodes.GS1;
using System.Drawing;
using BarcodeWriterTests.Helpers;
using System.Linq;

namespace BarcodeWriterTests
{
    [TestClass]
    public class EAN128Test
    {
        [TestMethod]
        public void EAN128_GenerateWithGS1Builder()
        {
            var builder = new GS1Builder();
            var data = new[] {
                new { ai=01, Value="14912345678904" },
                new { ai=17, Value="990101" },
                new { ai=30, Value="1000" },
                new { ai=10, Value="1234" },
                new { ai=98, Value="0007" + "0448" + "101110" + "200000" + "2" + "0000" },
            };
            data.ToList().ForEach(d => builder.Add(d.ai, d.Value));            
            var target = new EAN128();
            var expected_filepath = string.Format("EAN128_{0}.bmp", string.Join(
                string.Empty, data.Select(v => string.Format("{0:00}{1}", v.ai, v.Value))
                ));
            using (var actual_image = target.Generate(value: builder))
            {
                var expected = "9e8ff7c269aee9fdd967f1a598c8de38e1c85f77084158fea9dc7a25549a72";
                var actual = string.Join("", actual_image.ComputeHash().Select(_ => Convert.ToString(_, 16)));
                actual_image.Save(expected_filepath);
                Assert.AreEqual(expected: expected, actual: actual, message: "fail EAN128.Generate({0})", parameters: builder.ToDisplayString());
            }
        }

        [TestMethod]
        public void EAN128_GenerateWithGS1BuilderAndBarcodeSettings()
        {
            var builder = new GS1Builder();
            var settings = new BarcodeSettings()
            {
                NarrowWidth = 4,
                ModulePadding = 0,                
            };
            var data = new[] {
                new { ai=98, Value="0007" + "0448" + "101110" + "200000" + "2" + "0000" },
            };
            data.ToList().ForEach(d => builder.Add(d.ai, d.Value));
            var target = new EAN128();
            var expected_filepath = string.Format("EAN128_{0}.bmp", string.Join(
                string.Empty, data.Select(v => string.Format("{0:00}{1}", v.ai, v.Value))
                ));
            using (var actual_image = target.Generate(value: builder, settings: settings))
            {
                var expected = @"23e2df8b58d889fec31f39c2e19122d5b6e8e09fa61a14e2591b89239ae7e1";
                var actual = string.Join("", actual_image.ComputeHash().Select(_ => Convert.ToString(_, 16)));
                actual_image.Save(expected_filepath);
                Assert.AreEqual(expected: expected, actual: actual, message: "fail EAN128.Generate({0})", parameters: builder.ToDisplayString());
            }
        }
    }
}
