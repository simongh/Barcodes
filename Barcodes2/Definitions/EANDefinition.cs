using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Barcodes2.Definitions
{
	public abstract class EANDefinition : IDefinition
	{
		protected enum GuardType
		{
			Limit = 31,
			Split = 32
		}
		protected const int TEXTPADDING = 2;

		private Regex _pattern;

		protected IDictionary<int, Pattern> PatternSet
		{
			get;
			set;
		}

		public bool IsChecksumRequired
		{
			get;
			set;
		}

		/// <summary>
		/// List of parity settings used for calculations
		/// </summary>
		protected List<bool[]> Parity;

		/// <summary>
		/// Gets the digits are groupings
		/// </summary>
		protected int[] DigitGrouping
		{
			get;
			set;
		}

		protected abstract Regex GetRegex();

		public virtual bool IsDataValid(string value)
		{
			if (_pattern == null)
				_pattern = GetRegex();

			return _pattern.IsMatch(value);
		}

		public virtual CodedValueCollection GetCodes(string value)
		{
			var codes = new CodedValueCollection();

			for (int i = 0; i < value.Length; i++)
			{
				codes.Add(int.Parse(value.Substring(i, 1)));

			}

			CalculateParity(codes);

			return codes;
		}

		public virtual string GetDisplayText(string value)
		{
			return value;
		}

		public Pattern GetPattern(int value)
		{
			if (PatternSet == null)
				CreatePatternSet();

			return PatternSet[value];
		}

		protected void CreatePatternSet()
		{
			PatternSet = new Dictionary<int, Pattern>();

			//Odd parity (false)
			PatternSet.Add(0, Pattern.Parse("0 0 0 1 1 0 1"));
			PatternSet.Add(1, Pattern.Parse("0 0 1 1 0 0 1"));
			PatternSet.Add(2, Pattern.Parse("0 0 1 0 0 1 1"));
			PatternSet.Add(3, Pattern.Parse("0 1 1 1 1 0 1"));
			PatternSet.Add(4, Pattern.Parse("0 1 0 0 0 1 1"));
			PatternSet.Add(5, Pattern.Parse("0 1 1 0 0 0 1"));
			PatternSet.Add(6, Pattern.Parse("0 1 0 1 1 1 1"));
			PatternSet.Add(7, Pattern.Parse("0 1 1 1 0 1 1"));
			PatternSet.Add(8, Pattern.Parse("0 1 1 0 1 1 1"));
			PatternSet.Add(9, Pattern.Parse("0 0 0 1 0 1 1"));

			//Even parity (true)
			PatternSet.Add(10, Pattern.Parse("0 1 0 0 1 1 1"));
			PatternSet.Add(11, Pattern.Parse("0 1 1 0 0 1 1"));
			PatternSet.Add(12, Pattern.Parse("0 0 1 1 0 1 1"));
			PatternSet.Add(13, Pattern.Parse("0 1 0 0 0 0 1"));
			PatternSet.Add(14, Pattern.Parse("0 0 1 1 1 0 1"));
			PatternSet.Add(15, Pattern.Parse("0 1 1 1 0 0 1"));
			PatternSet.Add(16, Pattern.Parse("0 0 0 0 1 0 1"));
			PatternSet.Add(17, Pattern.Parse("0 0 1 0 0 0 1"));
			PatternSet.Add(18, Pattern.Parse("0 0 0 1 0 0 1"));
			PatternSet.Add(19, Pattern.Parse("0 0 1 0 1 1 1"));

			//right side
			PatternSet.Add(20, Pattern.Parse("1 1 1 0 0 1 0"));
			PatternSet.Add(21, Pattern.Parse("1 1 0 0 1 1 0"));
			PatternSet.Add(22, Pattern.Parse("1 1 0 1 1 0 0"));
			PatternSet.Add(23, Pattern.Parse("1 0 0 0 0 1 0"));
			PatternSet.Add(24, Pattern.Parse("1 0 1 1 1 0 0"));
			PatternSet.Add(25, Pattern.Parse("1 0 0 1 1 1 0"));
			PatternSet.Add(26, Pattern.Parse("1 0 1 0 0 0 0"));
			PatternSet.Add(27, Pattern.Parse("1 0 0 0 1 0 0"));
			PatternSet.Add(28, Pattern.Parse("1 0 0 1 0 0 0"));
			PatternSet.Add(29, Pattern.Parse("1 1 1 0 1 0 0"));

			PatternSet.Add((int)GuardType.Limit, Pattern.Parse("g1 0 1"));
			PatternSet.Add((int)GuardType.Split, Pattern.Parse("g0 1 0 1 0"));
		}

		protected void CreateParity()
		{
			Parity = new List<bool[]>();
			Parity.Add(new bool[] { false, false, false, false, false, false });
			Parity.Add(new bool[] { false, false, true, false, true, true });
			Parity.Add(new bool[] { false, false, true, true, false, true });
			Parity.Add(new bool[] { false, false, true, true, true, false });
			Parity.Add(new bool[] { false, true, false, false, true, true });
			Parity.Add(new bool[] { false, true, true, false, false, true });
			Parity.Add(new bool[] { false, true, true, true, false, false });
			Parity.Add(new bool[] { false, true, false, true, false, true });
			Parity.Add(new bool[] { false, true, false, true, true, false });
			Parity.Add(new bool[] { false, true, true, false, true, false });
		}

		public int CalculateWidth(BarcodeSettings settings, CodedValueCollection codes)
		{
			return (settings.NarrowWidth * ((codes.Count * 7) + 11)) + (DigitGrouping[0] * 7 * settings.NarrowWidth);
		}

		public abstract string AddChecksum(string value, CodedValueCollection codes);

		public void TransformSettings(BarcodeSettings settings)
		{
			settings.ModulePadding = 0;
		}

		/// <summary>
		/// Calculate parity for given list of codes
		/// </summary>
		/// <param name="codes">list of values</param>
		protected virtual void CalculateParity(CodedValueCollection codes)
		{
			if (Parity == null)
				CreateParity();

			bool[] parity = Parity[codes[0]];

			for (int i = 1; i < codes.Count; i++)
			{
				if (i < 1 + DigitGrouping[1])
				{
					if (parity[i - 1])
						codes[i] += 10;
				}
				else
					codes[i] += 20;
			}
			codes.RemoveAt(0);
		}
	}
}
