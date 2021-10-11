using System.Collections.Generic;

namespace Barcodes.Definitions
{
	public abstract class EanBase : ILimits, IChecksum
	{
		protected const byte Limit = 31;
		protected const byte Split = 32;
		protected const byte LimitStart = 33;
		protected const byte LimitEnd = 34;

		protected static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			//Odd parity (false)
			Pattern.Parse(0, "3332232"),
			Pattern.Parse(1, "3322332"),
			Pattern.Parse(2, "3323322"),
			Pattern.Parse(3, "3222232"),
			Pattern.Parse(4, "3233322"),
			Pattern.Parse(5, "3223332"),
			Pattern.Parse(6, "3232222"),
			Pattern.Parse(7, "3222322"),
			Pattern.Parse(8, "3223222"),
			Pattern.Parse(9, "3332322"),

			//Even parity (true)
			Pattern.Parse(10, "3233222"),
			Pattern.Parse(11, "3223322"),
			Pattern.Parse(12, "3322322"),
			Pattern.Parse(13, "3233332"),
			Pattern.Parse(14, "3322232"),
			Pattern.Parse(15, "3222332"),
			Pattern.Parse(16, "3333232"),
			Pattern.Parse(17, "3323332"),
			Pattern.Parse(18, "3332332"),
			Pattern.Parse(19, "3323222"),

			//right side
			Pattern.Parse(20, "2223323"),
			Pattern.Parse(21, "2233223"),
			Pattern.Parse(22, "2232233"),
			Pattern.Parse(23, "2333323"),
			Pattern.Parse(24, "2322233"),
			Pattern.Parse(25, "2332223"),
			Pattern.Parse(26, "2323333"),
			Pattern.Parse(27, "2333233"),
			Pattern.Parse(28, "2332333"),
			Pattern.Parse(29, "2223233"),

			Pattern.Parse(Limit, "232", true),
			Pattern.Parse(Split, "32323", true),
			Pattern.Parse(LimitStart, "2322"),
			Pattern.Parse(LimitEnd, "32"),
		});

		protected static readonly List<bool[]> _parity = new List<bool[]>
		{
			new [] { false, false, false, false, false, false },
			new [] { false, false, true, false, true, true },
			new [] { false, false, true, true, false, true },
			new [] { false, false, true, true, true, false },
			new [] { false, true, false, false, true, true },
			new [] { false, true, true, false, false, true },
			new [] { false, true, true, true, false, false },
			new [] { false, true, false, true, false, true },
			new [] { false, true, false, true, true, false },
			new [] { false, true, true, false, true, false }
		};

		public bool IsChecksumRequired => true;

		public PatternSet PatternSet => _patternSet;

		public virtual void AddChecksum(EncodedData data)
		{
			if (data.IsChecksumed)
				return;

			var total = 0;
			for (int i = 0; i < data.Codes.Count; i++)
			{
				if (i % 2 == 0)
					total += data.Codes[i].Value % 10;
				else
					total += 3 * (data.Codes[i].Value % 10);
			}

			total %= 10;

			data.AddToEnd(PatternSet.Find((total == 0 ? 20 : 30) - total));
			data.DisplayText += ((total == 0 ? 0 : 10) - total).ToString();

			data.IsChecksumed = true;
		}

		public abstract void AddLimits(EncodedData data);
	}
}