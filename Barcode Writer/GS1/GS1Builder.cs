using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcode_Writer.GS1
{
    public class GS1Builder
    {
        #region AI Constants

        public const int SSCC18 = 0;
        public const int GTIN = 1;
        public const int NumberofContainersContained = 2;
        public const int BatchNumbers = 10;
        public const int ProductionDate = 11;
        public const int DueDate = 12;
        public const int PackagingDate = 13;
        public const int BestBeforeDate = 15;
        public const int ExpirationDate = 17;
        public const int VariantNumber = 20;
        public const int SerialNumber = 21;
        public const int HIBCCQuantity = 22;
        public const int SecondaryDataField = 22;
        public const int LotNumber = 23;
        public const int AdditionalItemIdentification = 240;
        public const int CustomerPartNumber = 241;
        public const int MadeToOrderVariationNumber = 242;
        public const int SecondSerialNumber = 250;
        public const int ReferenceToSourceEntity = 251;
        public const int GDTI = 253;
        public const int GLNExtension = 254;
        public const int CountofItems = 30;
        public const int ProductNetWeightKg = 310;
        public const int ProductLengthMeters = 311;
        public const int ProductWidthMeters = 312;
        public const int ProductDepthMeters = 313;
        public const int ProductAreaSquareMeters = 314;
        public const int NetVolumeLiters = 315;
        public const int NetVolumeCubicMeters = 316;
        public const int NetWeightPounds = 320;
        public const int ProductLengthInches = 321;
        public const int ProductLengthFeet = 322;
        public const int ProductLengthYards = 323;
        public const int ProductWidthInches = 324;
        public const int ProductWidthFeet = 325;
        public const int ProductWidthYards = 326;
        public const int ProductDepthInches = 327;
        public const int ProductDepthFeet = 328;
        public const int ProductDepthYards = 329;
        public const int LogiticWeightKg = 330;
        public const int ContainerLengthMeters = 331;
        public const int ContainerWidthMeters = 332;
        public const int ContainerDepthMeters = 333;
        public const int ContainerAreaSquareMeters = 334;
        public const int LogisticVolumeLiters = 335;
        public const int LogisticVolumeCubicMeters = 336;
        public const int KilogramsPerSquareMetre = 337;
        public const int LogisticWeightPounds = 340;
        public const int ContainerLengthInches = 341;
        public const int ContainerLengthFeet = 342;
        public const int ContainerLengthYards = 343;
        public const int ContainerWidthInches = 344;
        public const int ContainerWidthFeet = 345;
        public const int ContainerWidthYards = 346;
        public const int ContainerDepthInches = 347;
        public const int ContainerDepthFeet = 348;
        public const int ContainerDepthYards = 349;
        public const int ProductAreaSquareInches = 350;
        public const int ProductAreaSquareFeet = 351;
        public const int ProductAreaSquareYards = 352;
        public const int ContainerAreaSquareInches = 353;
        public const int ContainerAreaSquareFeet = 354;
        public const int ContainerAreaSuqareYards = 355;
        public const int NetWeightTroyOunces = 356;
        public const int NetWeightOunces = 357;
        public const int NetVolumeQuarts = 360;
        public const int NetVolumeGallons = 361;
        public const int LogisticVolumeQuarts = 362;
        public const int LogisticVolumeGallons = 363;
        public const int NetVolumeCubicInches = 364;
        public const int NetVolumeCubicFeet = 365;
        public const int NetVolumeCubicYards = 366;
        public const int LogisticGrossVolumeCubicInches = 367;
        public const int LogisticGrossVolumeCubicFeet = 368;
        public const int LogisticGrossVolumeCubicYards = 369;
        public const int CountofTradeItems = 37;
        public const int AmountPayable = 390;
        public const int AmountPayableISO = 391;
        public const int AmountPayableArea = 392;
        public const int AmountPayableAreaISO = 393;
        public const int CustomerPurchaseOrderNumber = 400;
        public const int GINC = 401;
        public const int GSIN = 402;
        public const int RoutingCode = 403;
        public const int ShipToLocationCode = 410;
        public const int BillToLocationCode = 411;
        public const int PurchaseFromLocationCode = 412;
        public const int ShipForLocationCode = 413;
        public const int PhysicalLocationId = 414;
        public const int InvoicingPartyLocationCode = 415;
        public const int ShipToPostalCode = 420;
        public const int ShipToPostalCodeISO = 421;
        public const int CountryofOrigin = 422;
        public const int CountryOfInitialProcessing = 423;
        public const int CountryOfProcessing = 424;
        public const int CountryOfDisassembly = 425;
        public const int CountryCoveringProcessChain = 426;
        public const int NATOStockNumber = 7001;
        public const int UNCutClassification = 7002;
        public const int ExpirationDateTime = 7003;
        public const int ActivePotency = 7004;
        public const int RollProducts = 8001;
        public const int CellularMobileID = 8002;
        public const int GRAI = 8003;
        public const int GIAI = 8004;
        public const int PricePerUnit = 8005;
        public const int ComponentId = 8006;
        public const int IBAN = 8007;
        public const int ProductionDateTime = 8008;
        public const int GSRN = 8018;
        public const int PaymentSlipReference = 8020;
        public const int CouponExtendedCode = 8100;
        public const int CouponExtendedCodeEndofOffer = 8101;
        public const int CouponExtendedCode0 = 8102;
        public const int CouponCodeIDNorthAmerica = 8110;
        public const int TradingPartners = 90;
        public const int InternalCompanyCodes1 = 91;
        public const int InternalCompanyCodes2 = 92;
        public const int InternalCompanyCodes3 = 93;
        public const int InternalCompanyCodes4 = 94;
        public const int InternalCompanyCodes5 = 95;
        public const int InternalCompanyCoeds6 = 96;
        public const int InternalCompanyCodes7 = 97;
        public const int InternalCompanyCodes8 = 98;
        public const int InternalCompanyCodes9 = 99;

        #endregion

        private List<int> _Ais;
        private List<string> _Values;
        private char _FNC1;

        public System.Collections.ObjectModel.ReadOnlyCollection<int> AICollection
        {
            get { return new System.Collections.ObjectModel.ReadOnlyCollection<int>(_Ais); }
        }

        public System.Collections.ObjectModel.ReadOnlyCollection<string> Values
        {
            get { return new System.Collections.ObjectModel.ReadOnlyCollection<string>(_Values); }
        }

        public GS1Builder()
            : this('£')
        { }

        public GS1Builder(char fnc1)
        {
            _Ais = new List<int>();
            _Values = new List<string>();
            _FNC1 = fnc1;
        }

        public void Add(int ai, string value)
        {
            Validate(ai, value);

            _Ais.Add(ai);
            _Values.Add(value);
        }

        public void Add(int ai, int value)
        {
            Add(ai, value.ToString());
        }

        public void Add(int ai, decimal value, int precision)
        {
            string tmp = (value * (decimal)Math.Pow(10, precision)).ToString();
            Validate(ai, tmp);

            if (precision < 0 || precision > 6)
                throw new ArgumentOutOfRangeException("precision", "Decimal precision must be between 0 and 6.");

            Add(ai, precision.ToString() + tmp);
        }

        public void Add(int ai, DateTime value)
        {
            Add(ai, value, false);
        }

        public void Add(int ai, DateTime value, bool ignoreDay)
        {
            string tmp;
            if (ignoreDay)
                tmp = value.ToString("yyMM00");
            else
                tmp = value.ToString("yyMMdd");

            Add(ai, tmp);
        }

        public void RemoveAt(int index)
        {
            _Ais.RemoveAt(index);
            _Values.RemoveAt(index);
        }

        private bool Validate(int ai, string value)
        {
            string pattern;

            switch (ai)
            {
                case SSCC18:
                case ComponentId:
                case GSRN:
                    pattern = "^\\d{18}$";
                    break;
                case GTIN:
                case NumberofContainersContained:
                    pattern = "^\\d{14}$";
                    break;
                case BatchNumbers:
                case SerialNumber:
                    pattern = "^[a-z0-9]{1,20}$";
                    break;
                case ProductionDate:
                case DueDate:
                case PackagingDate:
                case BestBeforeDate:
                case ExpirationDate:
                    pattern = "^\\d{2}(0[1-9]|1[0-2])([0-2]\\d|3[01])$";
                    break;
                case VariantNumber:
                case CouponExtendedCode0:
                    pattern = "^\\d{2}$";
                    break;
                case SecondaryDataField:
                    pattern = "^[a-z0-9]{1,29}$";
                    break;
                case AdditionalItemIdentification:
                case CustomerPartNumber:
                case SecondSerialNumber:
                case ReferenceToSourceEntity:
                case CustomerPurchaseOrderNumber:
                case GINC:
                case RoutingCode:
                case UNCutClassification:
                case GIAI:
                case IBAN:
                case CouponCodeIDNorthAmerica:
                case TradingPartners:
                case InternalCompanyCodes1:
                case InternalCompanyCodes2:
                case InternalCompanyCodes3:
                case InternalCompanyCodes4:
                case InternalCompanyCodes5:
                case InternalCompanyCoeds6:
                case InternalCompanyCodes7:
                case InternalCompanyCodes8:
                case InternalCompanyCodes9:
                    pattern = "^[a-z0-9]{1,30}$";
                    break;
                case MadeToOrderVariationNumber:
                    pattern = "^\\d{1,6}";
                    break;
                case GDTI:
                    pattern = "^\\d{14,30}$";
                    break;
                case GLNExtension:
                    pattern = "^[a-z0-9]{1,20}$";
                    break;
                case CountofItems:
                case CountofTradeItems:
                    pattern = "^\\d{1,8}$";
                    break;
                case ProductNetWeightKg:
                case ProductLengthMeters:
                case ProductWidthMeters:
                case ProductDepthMeters:
                case ProductAreaSquareMeters:
                case NetVolumeLiters:
                case NetVolumeCubicMeters:
                case NetWeightPounds:
                case ProductLengthInches:
                case ProductLengthFeet:
                case ProductLengthYards:
                case ProductWidthInches:
                case ProductWidthFeet:
                case ProductWidthYards:
                case ProductDepthInches:
                case ProductDepthFeet:
                case ProductDepthYards:
                case LogiticWeightKg:
                case ContainerLengthMeters:
                case ContainerWidthMeters:
                case ContainerDepthMeters:
                case ContainerAreaSquareMeters:
                case LogisticVolumeLiters:
                case LogisticVolumeCubicMeters:
                case KilogramsPerSquareMetre:
                case LogisticWeightPounds:
                case ContainerLengthInches:
                case ContainerLengthFeet:
                case ContainerLengthYards:
                case ContainerWidthInches:
                case ContainerWidthFeet:
                case ContainerWidthYards:
                case ContainerDepthInches:
                case ContainerDepthFeet:
                case ContainerDepthYards:
                case ProductAreaSquareInches:
                case ProductAreaSquareFeet:
                case ProductAreaSquareYards:
                case ContainerAreaSquareInches:
                case ContainerAreaSquareFeet:
                case ContainerAreaSuqareYards:
                case NetWeightTroyOunces:
                case NetWeightOunces:
                case NetVolumeQuarts:
                case NetVolumeGallons:
                case LogisticVolumeQuarts:
                case LogisticVolumeGallons:
                case NetVolumeCubicInches:
                case NetVolumeCubicFeet:
                case NetVolumeCubicYards:
                case LogisticGrossVolumeCubicInches:
                case LogisticGrossVolumeCubicFeet:
                case LogisticGrossVolumeCubicYards:
                    pattern = "^\\d{7}$";
                    break;
                case PricePerUnit:
                case CouponExtendedCode:
                    pattern = "^\\d{6}$";
                    break;
                case AmountPayable:
                case AmountPayableArea:
                    pattern = "^\\d{1,15}$";
                    break;
                case AmountPayableISO:
                case AmountPayableAreaISO:
                    pattern = "^\\d{4,18}$";
                    break;
                case GSIN:
                    pattern = "^\\d{17}$";
                    break;
                case ShipToLocationCode:
                case BillToLocationCode:
                case PurchaseFromLocationCode:
                case ShipForLocationCode:
                case PhysicalLocationId:
                case InvoicingPartyLocationCode:
                case NATOStockNumber:
                    pattern = "^\\d{13}$";
                    break;
                case ShipToPostalCode:
                    pattern = "^[a-z0-9]{1,20}$";
                    break;
                case ShipToPostalCodeISO:
                    pattern = "^\\d{3}[a-z0-9]{1,9}$";
                    break;
                case CountryofOrigin:
                case CountryOfProcessing:
                case CountryOfDisassembly:
                case CountryCoveringProcessChain:
                    pattern = "^\\d{3}$";
                    break;
                case CountryOfInitialProcessing:
                    pattern = "^\\d{3,15}$";
                    break;
                case ExpirationDateTime:
                    pattern = "^\\d{10}$";
                    break;
                case ActivePotency:
                    pattern = "^\\d{1,4}$";
                    break;
                case RollProducts:
                    pattern = "^\\d{14}$";
                    break;
                case CellularMobileID:
                    pattern = "^[a-z0-9]{1,20}$";
                    break;
                case GRAI:
                    pattern = "^\\d{14}[a-z0-9]{1,16}$";
                    break;
                case ProductionDateTime:
                    pattern = "^\\d{8,12}$";
                    break;
                case PaymentSlipReference:
                    pattern = "^[a-z0-9]{1,25}$";
                    break;
                case CouponExtendedCodeEndofOffer:
                    pattern = "^\\d{10}$";
                    break;
                default:
                    throw new NotSupportedException("Unsupported AI code [" + ai.ToString() + "].");
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(value, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                return true;

            throw new ApplicationException("The value does not meet the specification for the AI.");
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _Ais.Count; i++)
            {
                sb.Append(_FNC1);
                sb.Append(_Ais[i]);
                sb.Append(_Values[i]);
            }

            return sb.ToString();
        }

        public string ToString(char fnc1)
        {
            _FNC1 = fnc1;
            return ToString();
        }

        public string ToDisplayString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _Ais.Count; i++)
            {
                sb.AppendFormat("({0})", _Ais[i]);
                sb.Append(_Values[i]);
            }

            return sb.ToString();
        }
    }
}
