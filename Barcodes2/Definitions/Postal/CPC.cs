using System.Collections.Generic;
using System.Linq;

namespace Barcodes2.Definitions.Postal
{
	public class CPC : DefaultDefinition
	{
		private const int ALIGNMENTBAR = 0x100;
		private const int ODDCOUNT = 0x101;

		private Dictionary<string, int> _Lookup;

		public CPC()
			:base()
		{
			CreateLookups();
		}

		protected override System.Text.RegularExpressions.Regex GetRegex()
		{
			return new System.Text.RegularExpressions.Regex("^[A-Z-[DFIOQU]]\\d[A-Z-[DFIOQU]] \\d[A-Z-[DFIOQU]]\\d$");
		}

		protected override void CreatePatternSet()
		{
			PatternSet = new Dictionary<int, Pattern>();

			PatternSet.Add(0, Pattern.Parse("nw nw nw"));
			PatternSet.Add(1, Pattern.Parse("nw nb nw"));
			PatternSet.Add(ALIGNMENTBAR, Pattern.Parse("nw nb nw"));
			PatternSet.Add(ODDCOUNT, Pattern.Parse("nw nw nw"));
		}

		/// <summary>
		/// Create the conversion table
		/// </summary>
		private void CreateLookups()
		{
			_Lookup = new Dictionary<string, int>();
			_Lookup.Add("A", 0x07);
			_Lookup.Add("B", 0x0C);
			_Lookup.Add("C", 0x0B);
			_Lookup.Add("E", 0x0D);
			_Lookup.Add("G", 0x09);
			_Lookup.Add("H", 0x08);
			_Lookup.Add("J", 0x06);
			_Lookup.Add("K", 0x03);
			_Lookup.Add("L", 0x02);
			_Lookup.Add("M", 0x04);
			_Lookup.Add("N", 0x16);
			_Lookup.Add("P", 0x1C);
			_Lookup.Add("R", 0x05);
			_Lookup.Add("S", 0x0A);
			_Lookup.Add("T", 0x14);
			_Lookup.Add("V", 0x11);
			_Lookup.Add("W", 0x18);
			_Lookup.Add("X", 0x13);
			_Lookup.Add("Y", 0x0E);
			_Lookup.Add("Z", 0x1A);

			int[] numbers = new int[] { 0xA, 0x2, 0x9, 0x3, 0xB, 0x5, 0x6, 0x7, 0xD, 0xE };
			for (int i = 0; i < numbers.Length; i++)
			{
				_Lookup.Add(i.ToString(), numbers[i]);
			}

			_Lookup.Add("X0", 0x11);
			_Lookup.Add("X1", 0x14);
			_Lookup.Add("X2", 0x1C);
			_Lookup.Add("X3", 0x41);
			_Lookup.Add("X4", 0x44);
			_Lookup.Add("X5", 0x4C);
			_Lookup.Add("X6", 0xC1);
			_Lookup.Add("X7", 0xC4);
			_Lookup.Add("X8", 0xCC);
			_Lookup.Add("X9", 0x84);
		}

		public override void TransformSettings(BarcodeSettings settings)
		{
			base.TransformSettings(settings);

			settings.WideWidth = 2 * settings.NarrowWidth;
			settings.IsTextShown = false;
			settings.ModulePadding = 0;
		}

		public override CodedValueCollection GetCodes(string value)
		{
			var codes = new CodedValueCollection();

			value = value.Replace(" ", "").ToUpper();

			var index = ToBinary(ParsePair(value.Substring(0, 2)), 0, 8, codes);
			index = ToBinary(_Lookup[value.Substring(2, 1)], index, 5, codes);
			index = ToBinary(_Lookup[value.Substring(3, 1)], index, 4, codes);
			ToBinary(ParsePair(value.Substring(4, 2)), index, 8, codes);

			int p = codes.Sum() + 1;
			codes.Add(ALIGNMENTBAR);
			if (p % 2 == 0)
				codes.Insert(0, ALIGNMENTBAR);
			else
				codes.Insert(0, ODDCOUNT);

			return codes;
		}

		private int ToBinary(int value, int index, int count, CodedValueCollection codes)
		{
			for (int i = 0; i < count; i++)
			{
				codes.Insert(index, value % 2);
				value >>= 1;
			}

			return index + count;
		}

		/// <summary>
		/// Take a double digit pair & get it's hex value from the conversion table and rules
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private int ParsePair(string value)
		{
			if (_Lookup.ContainsKey(value))
				return _Lookup[value];

			int ho = _Lookup[value.Substring(0, 1)];
			int lo = _Lookup[value.Substring(1, 1)];
			if (ho < 0x11)
				return (ho * 0x10) + lo;
			else if (ho == 0x11)
				return 0x10 + lo;
			else if (ho == 0x1a)
				return lo * 0x10;
			else if (ho == 0x16)
				return (lo * 0x10) + 1;
			else
				return (ho - 0x10) + (lo * 0x10);
		}
	}
}
