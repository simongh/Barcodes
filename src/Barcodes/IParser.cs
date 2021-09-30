using System.Collections.Generic;

namespace Barcodes
{
	public interface IParser
	{
		IEnumerable<Pattern> Parse(byte[] value);
	}
}