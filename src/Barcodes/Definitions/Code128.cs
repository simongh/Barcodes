using System.Linq;

namespace Barcodes.Definitions
{
	public class Code128 : IDefinition, IChecksum, ILimits
	{
		private const byte STOP = 106;

		private static readonly PatternSet _patternSet = new PatternSet(new[]
		{
			Pattern.Parse(0,   "22322332233"),
			Pattern.Parse(1,   "22332232233"),
			Pattern.Parse(2,   "22332233223"),
			Pattern.Parse(3,   "23323322333"),
			Pattern.Parse(4,   "23323332233"),
			Pattern.Parse(5,   "23332332233"),
			Pattern.Parse(6,   "23322332333"),
			Pattern.Parse(7,   "23322333233"),
			Pattern.Parse(8,   "23332233233"),
			Pattern.Parse(9,   "22332332333"),

			Pattern.Parse(10,  "22332333233"),
			Pattern.Parse(11,  "22333233233"),
			Pattern.Parse(12,  "23223322233"),
			Pattern.Parse(13,  "23322322233"),
			Pattern.Parse(14,  "23322332223"),
			Pattern.Parse(15,  "23222332233"),
			Pattern.Parse(16,  "23322232233"),
			Pattern.Parse(17,  "23322233223"),
			Pattern.Parse(18,  "22332223323"),
			Pattern.Parse(19,  "22332322233"),

			Pattern.Parse(20,  "22332332223"),
			Pattern.Parse(21,  "22322233233"),
			Pattern.Parse(22,  "22332223233"),
			Pattern.Parse(23,  "22232232223"),
			Pattern.Parse(24,  "22232332233"),
			Pattern.Parse(25,  "22233232233"),
			Pattern.Parse(26,  "22233233223"),
			Pattern.Parse(27,  "22232233233"),
			Pattern.Parse(28,  "22233223233"),
			Pattern.Parse(29,  "22233223323"),

			Pattern.Parse(30,  "22322322333"),
			Pattern.Parse(31,  "22322333223"),
			Pattern.Parse(32,  "22333223223"),
			Pattern.Parse(33,  "23233322333"),
			Pattern.Parse(34,  "23332322333"),
			Pattern.Parse(35,  "23332333223"),
			Pattern.Parse(36,  "23223332333"),
			Pattern.Parse(37,  "23332232333"),
			Pattern.Parse(38,  "23332233323"),
			Pattern.Parse(39,  "22323332333"),

			Pattern.Parse(40,  "22333232333"),
			Pattern.Parse(41,  "22333233323"),
			Pattern.Parse(42,  "23223222333"),
			Pattern.Parse(43,  "23223332223"),
			Pattern.Parse(44,  "23332232223"),
			Pattern.Parse(45,  "23222322333"),
			Pattern.Parse(46,  "23222333223"),
			Pattern.Parse(47,  "23332223223"),
			Pattern.Parse(48,  "22232223223"),
			Pattern.Parse(49,  "22323332223"),

			Pattern.Parse(50,  "22333232223"),
			Pattern.Parse(51,  "22322232333"),
			Pattern.Parse(52,  "22322233323"),
			Pattern.Parse(53,  "22322232223"),
			Pattern.Parse(54,  "22232322333"),
			Pattern.Parse(55,  "22232333223"),
			Pattern.Parse(56,  "22233323223"),
			Pattern.Parse(57,  "22232232333"),
			Pattern.Parse(58,  "22232233323"),
			Pattern.Parse(59,  "22233322323"),

			Pattern.Parse(60,  "22232222323"),
			Pattern.Parse(61,  "22332333323"),
			Pattern.Parse(62,  "22223332323"),
			Pattern.Parse(63,  "23233223333"),
			Pattern.Parse(64,  "23233332233"),
			Pattern.Parse(65,  "23323223333"),
			Pattern.Parse(66,  "23323333223"),
			Pattern.Parse(67,  "23333232233"),
			Pattern.Parse(68,  "23333233223"),
			Pattern.Parse(69,  "23223323333"),

			Pattern.Parse(70,  "23223333233"),
			Pattern.Parse(71,  "23322323333"),
			Pattern.Parse(72,  "23322333323"),
			Pattern.Parse(73,  "23333223233"),
			Pattern.Parse(74,  "23333223323"),
			Pattern.Parse(75,  "22333323323"),
			Pattern.Parse(76,  "22332323333"),
			Pattern.Parse(77,  "22223222323"),
			Pattern.Parse(78,  "22333323233"),
			Pattern.Parse(79,  "23332222323"),

			Pattern.Parse(80,  "23233222233"),
			Pattern.Parse(81,  "23323222233"),
			Pattern.Parse(82,  "23323322223"),
			Pattern.Parse(83,  "23222233233"),
			Pattern.Parse(84,  "23322223233"),
			Pattern.Parse(85,  "23322223323"),
			Pattern.Parse(86,  "22223233233"),
			Pattern.Parse(87,  "22223323233"),
			Pattern.Parse(88,  "22223323323"),
			Pattern.Parse(89,  "22322322223"),

			Pattern.Parse(90,  "22322223223"),
			Pattern.Parse(91,  "22223223223"),
			Pattern.Parse(92,  "23232222333"),
			Pattern.Parse(93,  "23233322223"),
			Pattern.Parse(94,  "23332322223"),
			Pattern.Parse(95,  "23222232333"),
			Pattern.Parse(96,  "23222233323"),
			Pattern.Parse(97,  "22223232333"),
			Pattern.Parse(98,  "22223233323"),
			Pattern.Parse(99,  "23222322223"),

			Pattern.Parse(100, "23222232223"),
			Pattern.Parse(101, "22232322223"),
			Pattern.Parse(102, "22223232223"),
			Pattern.Parse(103, "22323333233"),
			Pattern.Parse(104, "22323323333"),
			Pattern.Parse(105, "22323322233"),

            //STOP pattern
            Pattern.Parse(STOP, "2233322232322", true),
			});

		public PatternSet PatternSet => _patternSet;

		public bool IsChecksumRequired => true;

		public bool ValidateInput(string value)
		{
			return !value.Any(c => c > 127);
		}

		public void AddChecksum(EncodedData data)
		{
			var total = 0;

			for (int i = 0; i < data.Codes.Count; i++)
			{
				if (i == 0)
					total = data.Codes[i].Value;
				else
					total += data.Codes[i].Value * i;
			}

			data.AddToEnd(PatternSet.Find(total % 103));
		}

		public void AddLimits(EncodedData data)
		{
			data.AddToEnd(PatternSet.Find(STOP));
		}
	}
}