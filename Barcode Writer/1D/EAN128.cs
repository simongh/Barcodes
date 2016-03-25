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
            if (_Value == null)
            {
                return base.ParseText(value, codes);
            }
            else
            {
                if (_Value.ToString() != value)
                    base.ParseText(value, codes);
                else
                {
                    int i = 0;
                    foreach(var element_string in _Value.CalculateElementStrings())
                    {
                        var cds = new CodedValueCollection();
                        if (!element_string.EndsWith(char.ToString(Code128Helper.FNC1)))
                        {
                            base.ParseText(element_string, cds);
                            cds.RemoveAt(cds.Count - 1);
                        }
                        else
                        {
                            base.ParseText(element_string.Substring(0, element_string.Length - 1), cds);
                            cds.RemoveAt(cds.Count - 1);
                            cds.Add(Code128Helper.FNC1 - 50);
                        }

                        if (i == 0)
                            cds.Insert(1, Code128Helper.FNC1 - 50);
                        else if (codes[0] == cds[0])
                            cds.RemoveAt(0);
                        else
                        {
                            var r = cds[0];
                            cds.RemoveAt(0);
                            var ai = _Value.AICollection[i].ToString("{0:00}");
                            cds.Insert(ai.Length, r);
                        }

                        codes.AddRange(cds);
                        ++i;
                    }
                    codes.Add(Code128Helper.STOP);
                }

                var ret = _Value.ToDisplayString();
                _Value = null;
                return ret;
            }

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
