using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Barcodes.GS1
{
	public class GS1Service : IGS1Service
	{
		public bool IsValid(GS1Value value)
		{
			string pattern;

			switch (value.Identifier)
			{
				case ApplicationIdentifier.SSCC18:
				case ApplicationIdentifier.ComponentId:
				case ApplicationIdentifier.GSRN:
					pattern = "^\\d{18}$";
					break;

				case ApplicationIdentifier.GTIN:
				case ApplicationIdentifier.NumberofContainersContained:
					pattern = "^\\d{14}$";
					break;

				case ApplicationIdentifier.BatchNumbers:
				case ApplicationIdentifier.SerialNumber:
					pattern = "^[a-z0-9]{1,20}$";
					break;

				case ApplicationIdentifier.ProductionDate:
				case ApplicationIdentifier.DueDate:
				case ApplicationIdentifier.PackagingDate:
				case ApplicationIdentifier.BestBeforeDate:
				case ApplicationIdentifier.ExpirationDate:
					pattern = "^\\d{2}(0[1-9]|1[0-2])([0-2]\\d|3[01])$";
					break;

				case ApplicationIdentifier.VariantNumber:
				case ApplicationIdentifier.CouponExtendedCode0:
					pattern = "^\\d{2}$";
					break;

				case ApplicationIdentifier.SecondaryDataField:
					pattern = "^[a-z0-9]{1,29}$";
					break;

				case ApplicationIdentifier.AdditionalItemIdentification:
				case ApplicationIdentifier.CustomerPartNumber:
				case ApplicationIdentifier.SecondSerialNumber:
				case ApplicationIdentifier.ReferenceToSourceEntity:
				case ApplicationIdentifier.CustomerPurchaseOrderNumber:
				case ApplicationIdentifier.GINC:
				case ApplicationIdentifier.RoutingCode:
				case ApplicationIdentifier.UNCutClassification:
				case ApplicationIdentifier.GIAI:
				case ApplicationIdentifier.IBAN:
				case ApplicationIdentifier.CouponCodeIDNorthAmerica:
				case ApplicationIdentifier.TradingPartners:
				case ApplicationIdentifier.InternalCompanyCodes1:
				case ApplicationIdentifier.InternalCompanyCodes2:
				case ApplicationIdentifier.InternalCompanyCodes3:
				case ApplicationIdentifier.InternalCompanyCodes4:
				case ApplicationIdentifier.InternalCompanyCodes5:
				case ApplicationIdentifier.InternalCompanyCoeds6:
				case ApplicationIdentifier.InternalCompanyCodes7:
				case ApplicationIdentifier.InternalCompanyCodes8:
				case ApplicationIdentifier.InternalCompanyCodes9:
					pattern = "^[a-z0-9]{1,30}$";
					break;

				case ApplicationIdentifier.MadeToOrderVariationNumber:
					pattern = "^\\d{1,6}";
					break;

				case ApplicationIdentifier.GDTI:
					pattern = "^\\d{14,30}$";
					break;

				case ApplicationIdentifier.GLNExtension:
					pattern = "^[a-z0-9]{1,20}$";
					break;

				case ApplicationIdentifier.CountofItems:
				case ApplicationIdentifier.CountofTradeItems:
					pattern = "^\\d{1,8}$";
					break;

				case ApplicationIdentifier.ProductNetWeightKg:
				case ApplicationIdentifier.ProductLengthMeters:
				case ApplicationIdentifier.ProductWidthMeters:
				case ApplicationIdentifier.ProductDepthMeters:
				case ApplicationIdentifier.ProductAreaSquareMeters:
				case ApplicationIdentifier.NetVolumeLiters:
				case ApplicationIdentifier.NetVolumeCubicMeters:
				case ApplicationIdentifier.NetWeightPounds:
				case ApplicationIdentifier.ProductLengthInches:
				case ApplicationIdentifier.ProductLengthFeet:
				case ApplicationIdentifier.ProductLengthYards:
				case ApplicationIdentifier.ProductWidthInches:
				case ApplicationIdentifier.ProductWidthFeet:
				case ApplicationIdentifier.ProductWidthYards:
				case ApplicationIdentifier.ProductDepthInches:
				case ApplicationIdentifier.ProductDepthFeet:
				case ApplicationIdentifier.ProductDepthYards:
				case ApplicationIdentifier.LogiticWeightKg:
				case ApplicationIdentifier.ContainerLengthMeters:
				case ApplicationIdentifier.ContainerWidthMeters:
				case ApplicationIdentifier.ContainerDepthMeters:
				case ApplicationIdentifier.ContainerAreaSquareMeters:
				case ApplicationIdentifier.LogisticVolumeLiters:
				case ApplicationIdentifier.LogisticVolumeCubicMeters:
				case ApplicationIdentifier.KilogramsPerSquareMetre:
				case ApplicationIdentifier.LogisticWeightPounds:
				case ApplicationIdentifier.ContainerLengthInches:
				case ApplicationIdentifier.ContainerLengthFeet:
				case ApplicationIdentifier.ContainerLengthYards:
				case ApplicationIdentifier.ContainerWidthInches:
				case ApplicationIdentifier.ContainerWidthFeet:
				case ApplicationIdentifier.ContainerWidthYards:
				case ApplicationIdentifier.ContainerDepthInches:
				case ApplicationIdentifier.ContainerDepthFeet:
				case ApplicationIdentifier.ContainerDepthYards:
				case ApplicationIdentifier.ProductAreaSquareInches:
				case ApplicationIdentifier.ProductAreaSquareFeet:
				case ApplicationIdentifier.ProductAreaSquareYards:
				case ApplicationIdentifier.ContainerAreaSquareInches:
				case ApplicationIdentifier.ContainerAreaSquareFeet:
				case ApplicationIdentifier.ContainerAreaSuqareYards:
				case ApplicationIdentifier.NetWeightTroyOunces:
				case ApplicationIdentifier.NetWeightOunces:
				case ApplicationIdentifier.NetVolumeQuarts:
				case ApplicationIdentifier.NetVolumeGallons:
				case ApplicationIdentifier.LogisticVolumeQuarts:
				case ApplicationIdentifier.LogisticVolumeGallons:
				case ApplicationIdentifier.NetVolumeCubicInches:
				case ApplicationIdentifier.NetVolumeCubicFeet:
				case ApplicationIdentifier.NetVolumeCubicYards:
				case ApplicationIdentifier.LogisticGrossVolumeCubicInches:
				case ApplicationIdentifier.LogisticGrossVolumeCubicFeet:
				case ApplicationIdentifier.LogisticGrossVolumeCubicYards:
					pattern = "^\\d{7}$";
					break;

				case ApplicationIdentifier.PricePerUnit:
				case ApplicationIdentifier.CouponExtendedCode:
					pattern = "^\\d{6}$";
					break;

				case ApplicationIdentifier.AmountPayable:
				case ApplicationIdentifier.AmountPayableArea:
					pattern = "^\\d{1,15}$";
					break;

				case ApplicationIdentifier.AmountPayableISO:
				case ApplicationIdentifier.AmountPayableAreaISO:
					pattern = "^\\d{4,18}$";
					break;

				case ApplicationIdentifier.GSIN:
					pattern = "^\\d{17}$";
					break;

				case ApplicationIdentifier.ShipToLocationCode:
				case ApplicationIdentifier.BillToLocationCode:
				case ApplicationIdentifier.PurchaseFromLocationCode:
				case ApplicationIdentifier.ShipForLocationCode:
				case ApplicationIdentifier.PhysicalLocationId:
				case ApplicationIdentifier.InvoicingPartyLocationCode:
				case ApplicationIdentifier.NATOStockNumber:
					pattern = "^\\d{13}$";
					break;

				case ApplicationIdentifier.ShipToPostalCode:
					pattern = "^[a-z0-9]{1,20}$";
					break;

				case ApplicationIdentifier.ShipToPostalCodeISO:
					pattern = "^\\d{3}[a-z0-9]{1,9}$";
					break;

				case ApplicationIdentifier.CountryofOrigin:
				case ApplicationIdentifier.CountryOfProcessing:
				case ApplicationIdentifier.CountryOfDisassembly:
				case ApplicationIdentifier.CountryCoveringProcessChain:
					pattern = "^\\d{3}$";
					break;

				case ApplicationIdentifier.CountryOfInitialProcessing:
					pattern = "^\\d{3,15}$";
					break;

				case ApplicationIdentifier.ExpirationDateTime:
					pattern = "^\\d{10}$";
					break;

				case ApplicationIdentifier.ActivePotency:
					pattern = "^\\d{1,4}$";
					break;

				case ApplicationIdentifier.RollProducts:
					pattern = "^\\d{14}$";
					break;

				case ApplicationIdentifier.CellularMobileID:
					pattern = "^[a-z0-9]{1,20}$";
					break;

				case ApplicationIdentifier.GRAI:
					pattern = "^\\d{14}[a-z0-9]{1,16}$";
					break;

				case ApplicationIdentifier.ProductionDateTime:
					pattern = "^\\d{8,12}$";
					break;

				case ApplicationIdentifier.PaymentSlipReference:
					pattern = "^[a-z0-9]{1,25}$";
					break;

				case ApplicationIdentifier.CouponExtendedCodeEndofOffer:
					pattern = "^\\d{10}$";
					break;

				default:
					throw new NotSupportedException("Unsupported AI code [" + value.Identifier.ToString() + "].");
			}

			return Regex.IsMatch(value.Value, pattern, RegexOptions.IgnoreCase);
		}

		public GS1Value Create(ApplicationIdentifier applicationIdentifier, string value)
		{
			var g = new GS1Value(applicationIdentifier, value);
			return Create(g);
		}

		public GS1Value Create(ApplicationIdentifier applicationIdentifier, int value)
		{
			var g = new GS1Value(applicationIdentifier);
			g.SetValue(value);
			return Create(g);
		}

		public GS1Value Create(ApplicationIdentifier applicationIdentifier, decimal value, int precision)
		{
			var g = new GS1Value(applicationIdentifier);
			g.SetValue(value, precision);
			return Create(g);
		}

		public GS1Value Create(ApplicationIdentifier applicationIdentifier, DateTime value, bool ignoreDay = false)
		{
			var g = new GS1Value(applicationIdentifier);
			g.SetValue(value, ignoreDay);
			return Create(g);
		}

		private GS1Value Create(GS1Value value)
		{
			if (!IsValid(value))
				return null;

			return value;
		}

		public string ToString(Collections.GS1Collection collection, char fnc1 = Helpers.Code128Values.FNC1)
		{
			return ToDisplayString(collection, fnc1 + "{0}{1}");
		}

		public string ToDisplayString(Collections.GS1Collection collection, string format = "({0:d}){1}")
		{
			var sb = new StringBuilder();
			foreach (var item in collection)
			{
				if (!IsValid(item))
					throw new BarcodeException("Invalid format in GS1 value, ApplicationIdentifier [" + item.Identifier + "], value[" + item.Value + "]");

				sb.AppendFormat(format, item.Identifier, item.Value);
			}

			return sb.ToString();
		}

		public int CheckDigitCalculate(Collections.CodedValueCollection codes)
		{
			int total = 0;
			bool flip = true;
			for (int i = codes.Count - 1; i > 0; i--)
			{
				if (flip)
					total += codes[i] * 3;
				else
					total += codes[i];

				flip = !flip;
			}

			total = total % 10;
			return total == 0 ? 0 : 10 - total;
		}

		public string Normalise(string value)
		{
			if (value.Length < 13)
				value = value.PadLeft(13, '0');

			return value;
		}
	}
}