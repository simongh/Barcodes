using System.Collections.Generic;

namespace Barcodes
{
	public class EncodedData
	{
		public IList<Pattern> Codes { get; }

		public string DisplayText { get; set; }

		public bool IsChecksumed { get; set; }

		public EncodedData()
			: this(new List<Pattern>())
		{ }

		public EncodedData(IList<Pattern> codes)
		{
			Codes = codes;
		}

		public void AddToStart(Pattern pattern)
		{
			Codes.Insert(0, pattern);
		}

		public void AddToEnd(Pattern pattern)
		{
			Codes.Add(pattern);
		}

		public void Bracket(Pattern pattern)
		{
			AddToStart(pattern);
			AddToEnd(pattern);
		}
	}
}