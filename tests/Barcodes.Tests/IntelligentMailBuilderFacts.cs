using System.Linq;
using Shouldly;
using Xunit;

namespace Barcodes
{
	public class IntelligentMailBuilderFacts
	{
		//based on https://www.usps.com/election-mail/creating-imb-election-mail-kit.pdf
		[Fact]
		public void BuildTest()
		{
			//arrange
			var zip = "00-270-123456-200800001-98765-4321-01";

			var builder = new IntelligentMailBuilder();

			//act
			var result = builder.Build(zip);

			//assert
			var bars = "TTFAFDADTFFFADTAFAFTTDATDFAAFTDAFDFDFDATFDFTDDDDFADFFDADDTDDTTDAT"
				.ToCharArray()
				.Select(c => (byte)c);

			result.ShouldBe(bars);
		}
	}
}