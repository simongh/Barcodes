using Shouldly;
using Xunit;

namespace Barcodes
{
	public class Code128BuilderFacts
	{
		[Fact]
		public void SimpleAddTest()
		{
			//arrange
			var builder = new Code128Builder();

			builder.Add("test");

			//act
			var result = builder.ToArray();

			//assert
			result.ShouldBe(new byte[] { 0x54, 0x45, 0x53, 0x54 });
		}

		[Fact]
		public void CodeATest()
		{
			//arrange
			var builder = Code128Builder
				.UsingCodeA()
				.Add("TEST");

			//act
			var result = builder.ToArray();

			//assert
			result.ShouldBe(new byte[] { 0x67, 0x34, 0x25, 0x33, 0x34 });
		}

		[Fact]
		public void CodeCTest()
		{
			//arrange
			var builder = Code128Builder
				.UsingCodeC()
				.Add("20304060");

			//act
			var result = builder.ToArray();

			//assert
			result.ShouldBe(new byte[] { 0x69, 20, 30, 40, 60 });
		}

		[Fact]
		public void CodeSwitchTest()
		{
			//arrange
			var builder = new Code128Builder()
				.Add("aBc")
				.SwitchToCodeC()
				.Add("5065")
				.SwitchToCodeA()
				.Add("A");

			//act
			var result = builder.ToArray();

			//assert
			result.ShouldBe(new byte[] { 0x41, 0x22, 0x43, 0x63, 50, 65, 0x65, 0x21 });
		}
	}
}