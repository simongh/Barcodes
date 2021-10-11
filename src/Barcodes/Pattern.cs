using System;
using System.Collections.Generic;
using System.Linq;

namespace Barcodes
{
	public class Pattern
	{
		public byte Value { get; }

		public IEnumerable<Element> Elements { get; }

		public int WideCount => Elements.Count(e => e.IsWide());

		public int NarrowCount => Elements.Count() - WideCount;

		public int BlackCount => Elements.Count(e => e.IsWhite());

		public int WhiteCount => Elements.Count() - WhiteCount;

		public bool IsGuard { get; }

		public Pattern(byte value, IEnumerable<Element> elements, bool isGuard = false)
			: this()
		{
			Guard.IsNotNull(elements, nameof(elements));
			Guard.IsNotNull(value, nameof(value));

			Value = value;
			Elements = elements;
		}

		private Pattern()
		{ }

		public static Pattern Parse(char value, string pattern, bool isGuard = false) => Parse((byte)value, pattern, isGuard);

		public static Pattern Parse(byte value, string pattern, bool isGuard = false)
		{
			Guard.IsNotNull(pattern, nameof(pattern));

			return new Pattern(value, pattern.Select(ToElement).ToArray(), isGuard);
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