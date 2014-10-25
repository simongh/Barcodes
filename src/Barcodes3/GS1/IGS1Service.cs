using System;

namespace Barcodes.GS1
{
	public interface IGS1Service
	{
		bool IsValid(GS1Value value);

		GS1Value Create(ApplicationIdentifier applicationIdentifier, string value);

		GS1Value Create(ApplicationIdentifier applicationIdentifier, int value);

		GS1Value Create(ApplicationIdentifier applicationIdentifier, decimal value, int precision);

		GS1Value Create(ApplicationIdentifier applicationIdentifier, DateTime value, bool ignoreDay = false);

		string ToString(Collections.GS1Collection collection, char fnc1 = Helpers.Code128Values.FNC1);

		string ToDisplayString(Collections.GS1Collection collection, string format = "({0:d}){1}");

		int CheckDigitCalculate(Collections.CodedValueCollection codes);

		string Normalise(string value);
	}
}