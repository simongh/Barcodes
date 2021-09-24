using System;
using System.Collections.Generic;
using System.Linq;

namespace BarcodeReader.Types
{
	public class Pattern
	{
		public char Value { get; }

		public IEnumerable<Element> Elements { get; }

		public int WideCount { get; private set; }

		public int NarrowCount { get; private set; }

		public int BlackCount { get; private set; }

		public int WhiteCount { get; private set; }

		public Pattern(char value, IEnumerable<Element> elements)
			: this()
		{
			Guard.IsNotNull(elements, nameof(elements));
			Guard.IsNotNull(value, nameof(value));

			Value = value;
			Elements = elements;

			WideCount = elements.Count(e => e.IsWide());
			NarrowCount = elements.Count() - WideCount;

			WhiteCount = elements.Count(e => e.IsWhite());
			BlackCount = elements.Count() - WhiteCount;
		}

		private Pattern()
		{ }

		public static Pattern Parse(char value, string pattern)
		{
			Guard.IsNotNull(pattern, nameof(pattern));

			return new Pattern(value, pattern.Select(ToElement).ToArray());
		}

		private static Element ToElement(char value)
		{
			var element = (Element)(value - '0');

			if (!Enum.IsDefined(typeof(Element), element))
				throw new ArgumentOutOfRangeException("pattern", element, "Invalid element value encountered");

			return element;
		}
	}
}