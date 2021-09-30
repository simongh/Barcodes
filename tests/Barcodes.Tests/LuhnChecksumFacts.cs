using Shouldly;
using Xunit;

namespace Barcodes
{
	public class LuhnChecksumFacts
	{
		[Fact]
		public void CalculateFromBytesTest()
		{
			var data = new byte[] { 1, 2, 3, 4, };

			var checksum = LuhnChecksum.Calculate(data);

			checksum.ShouldBe((byte)4);
		}

		[Fact]
		public void CalculateFromStringTest()
		{
			//arrange
			var data = "1234";

			//act
			var checksum = LuhnChecksum.Calculate(data);

			//assert
			checksum.ShouldBe('4');
		}

		[Fact]
		public void ExtendedChecksumFromBytesTest()
		{
			//arrange
			var data = new byte[] { 5, 6, 7, 8, 9, 10, 11, 12, 13, };

			//act
			var checksum = LuhnChecksum.Calculate(data, 16);

			//assert
			checksum.ShouldBe((byte)15);
		}

		[Fact]
		public void ExtendedChecksumFromStringTest()
		{
			//arrange
			var data = "56789abcd";

			//act
			var checksum = LuhnChecksum.Calculate(data, 16, c =>
			 {
				 if (char.IsDigit(c))
					 return (byte)(c - '0');
				 else
					 return (byte)(c - 'a');
			 });

			//assert
			checksum.ShouldBe((byte)13);
		}
	}
}