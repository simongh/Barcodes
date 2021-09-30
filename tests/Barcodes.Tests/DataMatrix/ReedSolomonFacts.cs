using Shouldly;
using Xunit;

namespace Barcodes.DataMatrix
{
	public class ReedSolomonFacts
	{
		[Fact(Skip = "finding reliable test data")]
		public void CalculateTest()
		{
			//arrange
			var data = new byte[] { 142, 164, 186 };

			var rs = new ReedSolomon(ReedSolomonTables.Instance);

			//act
			var result = rs.Encode(data);

			//assert
			result.ShouldBe(new byte[] { 114, 25, 5, 88, 102 });
		}
	}
}