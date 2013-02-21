using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Barcodes2.Definitions
{
	public abstract class DefaultDefinition : IDefinition
	{
		private Regex _pattern;
		private IDictionary<int, Pattern> _patternSet;

		protected IDictionary<int, Pattern> PatternSet
		{
			get
			{
				if (_patternSet == null)
					CreatePatternSet();

				return _patternSet;
			}
			set
			{
				_patternSet = value;
			}
		}

		public bool IsChecksumRequired
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
			var result = new CodedValueCollection();
			foreach (var item in value)
			{
				result.Add(item);
			}
			return result;
		}

		public virtual string GetDisplayText(string value)
		{
			return value;
		}

		public Pattern GetPattern(int value)
		{
			return PatternSet[value];
		}

		protected abstract void CreatePatternSet();

		public virtual int CalculateWidth(BarcodeSettings settings, CodedValueCollection codes)
		{
			return codes.Sum(x => (PatternSet[x].NarrowCount * settings.NarrowWidth) + (PatternSet[x].WideCount * settings.WideWidth)) + (codes.Count * settings.ModulePadding);
		}

		public virtual string AddChecksum(string value, CodedValueCollection codes)
		{
			throw new NotSupportedException();
		}

		public virtual void TransformSettings(BarcodeSettings settings)
		{ }
	}
}
