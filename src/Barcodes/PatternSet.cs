using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Barcodes
{
	public class PatternSet : IEnumerable<Pattern>
	{
		private IEnumerable<Pattern> _patterns;

		internal PatternSet(IEnumerable<Pattern> patterns)
		{
			_patterns = patterns;
		}

		public IEnumerator<Pattern> GetEnumerator() => _patterns.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => _patterns.GetEnumerator();

		public Pattern Find(char value) => _patterns.FirstOrDefault(p => p.Value == value);

		public Pattern Find(int value) => _patterns.FirstOrDefault(p => p.Value == value);

		public Pattern Index(int value) => _patterns.ElementAt(value);
	}
}