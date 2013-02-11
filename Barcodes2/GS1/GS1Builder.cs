using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Barcodes2.GS1
{
	public class GS1Builder : IEnumerable<GS1Value>
	{
		private List<GS1Value> _values;

		public char FNC1
		{
			get;
			private set;
		}

		public System.Collections.ObjectModel.ReadOnlyCollection<GS1Value> Values
		{
			get { return new System.Collections.ObjectModel.ReadOnlyCollection<GS1Value>(_values); }
		}

		public GS1Builder()
			: this(Code128Helper.FNC1)
		{ }

		public GS1Builder(char fnc1)
		{
			_values = new List<GS1Value>();
			FNC1 = fnc1;
		}

		public void Add(int ai, string value)
		{
			var g = GS1Value.Create(ai, value);
			if (g != null)
				_values.Add(g);
		}

		public void Add(int ai, int value)
		{
			var g = GS1Value.Create(ai, value);
			if (g != null)
				_values.Add(g);
		}

		public void Add(int ai, decimal value, int precision)
		{
			var g = GS1Value.Create(ai, value, precision);
			if (g != null)
				_values.Add(g);
		}

		public void Add(int ai, DateTime value)
		{
			var g = GS1Value.Create(ai, value);
			if (g != null)
				_values.Add(g);
		}

		public void Add(int ai, DateTime value, bool ignoreDay)
		{
			var g = GS1Value.Create(ai, value);
			if (g != null)
				_values.Add(g);
		}

		public void RemoveAt(int index)
		{
			_values.RemoveAt(index);
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var item in _values)
			{
				sb.Append(FNC1);
				sb.Append(item.ApplicationIdentifier);
				sb.Append(item.Value);
			}

			return sb.ToString();
		}

		public string ToString(char fnc1)
		{
			FNC1 = fnc1;
			return ToString();
		}

		public string ToDisplayString()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var item in _values)
			{
				sb.AppendFormat("({0})", item.ApplicationIdentifier);
				sb.Append(item.Value);
			}

			return sb.ToString();
		}

		public IEnumerator<GS1Value> GetEnumerator()
		{
			return _values.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)_values).GetEnumerator();
		}
	}
}
