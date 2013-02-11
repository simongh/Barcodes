using System.Drawing;

namespace Barcodes
{
	public class EAN128 : Code128
	{
		private GS1.GS1Builder _Value;
		//public Bitmap Generate(int aiCode, string value)
		//{
		//    return Generate(aiCode, value, DefaultSettings);
		//}

		//public Bitmap Generate(int aiCode, int specifier, string value)
		//{
		//    return Generate(aiCode, specifier, value, DefaultSettings);
		//}

		//public Bitmap Generate(int aiCode, int specifier, string value, BarcodeSettings settings)
		//{
		//    return Generate(CreateEAN128(aiCode, specifier, value), settings);
		//}

		//public Bitmap Generate(int aiCode, string value, BarcodeSettings settings)
		//{
		//    return Generate(CreateEAN128(aiCode,value), settings);
		//}

		//public Bitmap Generate(Dictionary<int, string> values)
		//{
		//    return Generate(values, DefaultSettings);
		//}

		//public Bitmap Generate(Dictionary<int, string> values, BarcodeSettings settings)
		//{
		//    StringBuilder s = new StringBuilder();

		//    s.Append(Code128Helper.StartVariantB);
		//    foreach (KeyValuePair<int, string> item in values)
		//    {
		//        s.Append(CreateEAN128(item.Key, item.Value));
		//    }

		//    return Generate(s.ToString(), settings);
		//}

		public Bitmap Generate(GS1.GS1Builder value)
		{
			AiMarker = value.FNC1;
			_Value = value;
			return Generate(value.ToString());
		}

		public Bitmap Generate(GS1.GS1Builder value, BarcodeSettings settings)
		{
			AiMarker = value.FNC1;
			_Value = value;
			return Generate(value.ToString(), settings);
		}

		protected override string ParseText(string value, CodedValueCollection codes)
		{
			value = base.ParseText(value, codes);

			if (_Value != null)
			{
				value = _Value.ToDisplayString();
				_Value = null;
			}

			return value;
		}

		//public string CreateEAN128(int aiCode, string value)
		//{
		//    return string.Format("{0}£{1}£{2}", Code128Helper.FNC1, aiCode, value);
		//}

		//public string CreateEAN128(int aiCode, int specifier, string value)
		//{
		//    return string.Format("{0}£{1}{2}£{3}", Code128Helper.FNC1, aiCode, specifier, value);
		//}
	}
}
