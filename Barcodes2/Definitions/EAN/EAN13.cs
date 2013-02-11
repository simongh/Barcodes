
namespace Barcodes2.Definitions.EAN
{
	public class EAN13 : EANDefinition
	{
		public EAN13()
		{
			DigitGrouping = new int[] { 1, 6, 6 };
			IsChecksumRequired = true;
		}

		protected override System.Text.RegularExpressions.Regex GetRegex()
		{
			return new System.Text.RegularExpressions.Regex("^\\d{12,13}$");
		}

		public override string AddChecksum(string value, CodedValueCollection codes)
		{
			if (codes.Count == 13)
				return value;

			int total = 0;
			for (int i = 0; i < codes.Count; i++)
			{
				if (i % 2 == 0)
					total += (codes[i] % 10);
				else
					total += 3 * (codes[i] % 10);
			}

			total = total % 10;
			codes.Add(total == 0 ? 20 : 30 - total);
			
			return value + (total == 0 ? 0 : 10 - total).ToString();
		}

		public override CodedValueCollection GetCodes(string value)
		{
			var codes = base.GetCodes(value);

			codes.Insert(6, (int)GuardType.Split);
			codes.Insert(0, (int)GuardType.Limit);
			codes.Add((int)GuardType.Limit);

			return codes;
		}
	}
}
