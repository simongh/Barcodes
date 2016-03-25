using Barcodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using BarcodeWriterTests.Helpers;
using System.Linq;
using System;

namespace BarcodeWriterTests
{
    [TestClass]
    public class Code128Test
    {
        [TestMethod]
        public void Code128_Constructor()
        {
            var b = new Code128();
            Assert.IsInstanceOfType(value: b, expectedType: typeof(Code128), message: "fail constructor Code128");
        }

        [TestMethod]
        public void Code128_IsValidData()
        {
            var b = new Code128();
            const string s = "\x01 09azAZ\t\n\x7f";
            Assert.IsTrue(condition: b.IsValidData(s), message: "fail IsValidData()");
        }

        [TestMethod]
        public void Code128_Genelate()
        {
            
            var target = new Code128();
            var text = "9784569809731";
            using (var actual_image = target.Generate(text: text))
            {
                var expected = "69ecc3d51fa5fc1c4a7fb2290d5a73ef920e2bbc4638da5d0e5e2856d1f27c1";
                var actual = string.Join("", actual_image.ComputeHash().Select(_ => Convert.ToString(_, 16)));
                Assert.AreEqual(expected: expected, actual: actual, message: "fail Code128.Generate({0})", parameters: text);
            }
        }
    }
}
