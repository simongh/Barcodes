using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;

namespace Barcodes
{
	public class AsciiTransformerFacts
	{
		[Fact]
		public void TransformBasicRangeTest()
		{
			//arrange
			var data = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.$/+% ";

			var transformer = new AsciiTransformer();

			var result = new StringBuilder();

			//act

			foreach (var item in data)
			{
				result.Append(transformer.Transform(item));
			}

			//assert
			result.ToString().ShouldBe(data);
		}

		[Fact]
		public void ExtendedRangeTest()
		{
			//arrange
			var transformer = new AsciiTransformer
			{
				Shift1 = '$',
				Shift2 = '%',
				Shift3 = '/',
				Shift4 = '+',
			};

			var result = new List<char>();

			var valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.$/+% ";

			//act
			for (int i = 0; i < 127; i++)
			{
				result.AddRange(transformer.Transform((char)i));
			}

			//assert
			result.ShouldAllBe(c => valid.Contains(c));
		}
	}
}