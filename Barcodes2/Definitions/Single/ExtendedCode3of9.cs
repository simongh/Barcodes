using System.Text;

namespace Barcodes2.Definitions.Single
{
	public class ExtendedCode3of9 : Code3of9
	{
		public override CodedValueCollection GetCodes(string value)
		{
			var v = new StringBuilder();
			foreach (char item in value.ToCharArray())
			{
				v.Append(AsciiEncoder.Lookup(item));
			}

			return base.GetCodes(v.ToString());
		}

		public override string GetDisplayText(string value)
		{
			return value;
		}
	}
}
