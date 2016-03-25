using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Barcodes;
using Barcodes.GS1;
using System.Linq;
using System.Text;

namespace BarcodeWriterTests.GS1
{
    [TestClass]
    public class GS1BuilderTest
    {
        [TestMethod]
        public void GS1Builder_Constructor()
        {
            var g = new GS1Builder();
            Assert.IsInstanceOfType(value: g, expectedType: typeof(GS1Builder), message: "fail constructor GS1Builder");
            Assert.AreEqual(Code128Helper.FNC1, g.FNC1, "fail default constructor");
            var g2 = new GS1Builder(Code128Helper.FNC1);
            Assert.AreEqual(Code128Helper.FNC1, g2.FNC1, "fail constructor with FNC1 parameter");
        }

        [TestMethod]
        public void GS1Builder_Add()
        {
            var g = new GS1Builder();
            var data = new[]
            {
                new { ai=   1, value="14912345678904" },
                new { ai= 420, value="1010001" },
                new { ai=  91, value="1234567890" },
                new { ai=  92, value="0" },
            };
            data.ToList()
                .ForEach(v => g.Add(ai: v.ai, value: v.value));

            CollectionAssert.AreEqual(expected: data.Select(v => v.ai).ToArray(), actual: g.AICollection, message: "fail add ai");
            CollectionAssert.AreEqual(expected: data.Select(v => v.value).ToArray(), actual: g.Values, message: "fail add value");
        }

        [TestMethod]
        public void GS1Builder_RemoveAt()
        {
            var g = new GS1Builder();
            var ais = new int[] { 91, 92, 93 };
            var values = new string[] { "1234567890", "0", "93" };
            const int index = 1;
            ais.Select((r, i) => new { ai = r, value = values[i] }).ToList()
                .ForEach(r => g.Add(ai: r.ai, value: r.value));
            g.RemoveAt(index);

            Assert.IsTrue(g.AICollection.SequenceEqual(ais.Where((r, i) => i != index)), "fail add ai");
            Assert.IsTrue(g.Values.SequenceEqual(values.Where((r, i) => i != index)), "fail add values");
        }

        [TestMethod]
        public void GS1Builder_ToStringWithoutParameter()
        {
            var g = new GS1Builder();
            var data = new[]
            {
                new { ai=   1, value="14912345678904", Terminate=String.Empty },
                new { ai= 420, value="1010001", Terminate=Code128Helper.FNC1.ToString() },
                new { ai=  91, value="1234567890", Terminate=Code128Helper.FNC1.ToString() },
                new { ai=  92, value="0", Terminate=Code128Helper.FNC1.ToString() },
            };
            data.ToList()
                .ForEach(v => g.Add(ai: v.ai, value: v.value));
            var sb = new StringBuilder();
            sb.Append(Code128Helper.FNC1);
            data.ToList()
                .ForEach(v =>
                {
                    sb.AppendFormat("{0:00}", v.ai);
                    sb.Append(v.value);
                    sb.Append(v.Terminate);
                });
            var expected = Encoding.ASCII.GetBytes(sb.ToString());
            var actual = Encoding.ASCII.GetBytes(g.ToString());
            CollectionAssert.AreEqual(expected: expected, actual: actual, message: "fail ToString()");
        }

        [TestMethod]
        public void GS1Builder_ToStringWithParameter()
        {
            var g = new GS1Builder();
            var data = new[]
            {
                new { ai=   1, value="14912345678904", Terminate=String.Empty },
                new { ai= 420, value="1010001", Terminate=Code128Helper.FNC1.ToString() },
                new { ai=  91, value="1234567890", Terminate=Code128Helper.FNC1.ToString() },
                new { ai=  92, value="0", Terminate=Code128Helper.FNC1.ToString() },
            };
            data.ToList()
                .ForEach(v => g.Add(ai: v.ai, value: v.value));
            var sb = new StringBuilder();
            sb.Append(Code128Helper.FNC1);
            data.ToList()
                .ForEach(v =>
                {
                    sb.AppendFormat("{0:00}", v.ai);
                    sb.Append(v.value);
                    sb.Append(v.Terminate);
                });
            var expected = Encoding.ASCII.GetBytes(sb.ToString());
            var actual = Encoding.ASCII.GetBytes(g.ToString(Code128Helper.FNC2));
            CollectionAssert.AreEqual(expected: expected, actual: actual, message: "fail ToString(fnc1)");
            Assert.AreEqual(expected: Code128Helper.FNC2, actual: g.FNC1, message: "fail FNC1");
        }

        [TestMethod]
        public void GS1Builder_ToDisplayString()
        {
            var g = new GS1Builder();
            var data = new[]
            {
                new { ai=   1, value="14912345678904", Terminate=String.Empty },
                new { ai= 420, value="1010001", Terminate=Code128Helper.FNC1.ToString() },
                new { ai=  91, value="1234567890", Terminate=Code128Helper.FNC1.ToString() },
                new { ai=  92, value="0", Terminate=Code128Helper.FNC1.ToString() },
            };
            data.ToList()
                .ForEach(v => g.Add(ai: v.ai, value: v.value));
            var sb = new StringBuilder();
            data.ToList()
                .ForEach(v =>
                {
                    sb.AppendFormat("({0:00})", v.ai);
                    sb.Append(v.value);
                });
            var expected = sb.ToString();
            var actual = g.ToDisplayString();
            Assert.AreEqual(expected: expected, actual: actual, message: "fail ToDisplayString()");
        }
    }
}
