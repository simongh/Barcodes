﻿using System.Collections.Generic;

namespace BarcodeReader.Types
{
	public class EncodedData
	{
		public IList<Pattern> Codes { get; }

		public string DisplayText { get; set; }

		public EncodedData()
			: this(new List<Pattern>())
		{ }

		public EncodedData(IList<Pattern> codes)
		{
			Codes = codes;
		}
	}
}