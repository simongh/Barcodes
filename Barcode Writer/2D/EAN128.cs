using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Barcode_Writer
{
    public class EAN128 : Code128
    {
        #region AI Constants

        public const int SSCC18 = 0;
        public const int SSCC14 = 1;
        public const int NumberofContainersContained = 2;
        public const int BatchNumbers = 10;
        public const int ProductionDate = 11;
        public const int PackagingDate = 13;
        public const int SellByDate = 15;
        public const int ExpirationDate = 17;
        public const int ProductVariant = 20;
        public const int SerialNumber = 21;
        public const int HIBCCQuantity = 22;
        public const int LotNumber = 23;
        public const int AdditionalProductIdentification = 240;
        public const int SecondSerialNumber = 250;
        public const int QuantityEach = 30;
        public const int ProductNetWeightKg = 310;
        public const int ProductLengthMeters = 311;
        public const int ProductWidthMeters = 312;
        public const int ProductDepthMeters = 313;
        public const int ProductAreaSquareMeters = 314;
        public const int ProductVolumeLiters = 315;
        public const int ProductVolumeCubicMeters = 316;
        public const int ProductNetWeightPounds = 320;
        public const int ProductLengthInches = 321;
        public const int ProductLengthFeet = 322;
        public const int ProductLengthYards = 323;
        public const int ProductWidthInches = 324;
        public const int ProductWidthFeet = 325;
        public const int ProductWidthYards = 326;
        public const int ProductDepthInches = 327;
        public const int ProductDepthFeet = 328;
        public const int ProductDepthYards = 329;
        public const int ContainerGrossWeightKg = 330;
        public const int ContainerLengthMeters = 331;
        public const int ContainerWidthMeters = 332;
        public const int ContainerDepthMeters = 333;
        public const int ContainerAreaSquareMeters = 334;
        public const int ContainerGrossVolumeLiters = 335;
        public const int ContainerGrossVolumeCubicMeters = 336;
        public const int ContainerGrossWeightPounds = 340;
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
        public const int ProductVolumeQuarts = 360;
        public const int ProductVolumeGallons = 361;
        public const int ContainerGrossVolumeQuarts = 362;
        public const int ContainerGrossVolumeGallons = 363;
        public const int ProductVolumeCubicInches = 364;
        public const int ProductVolumeCubicFeet = 365;
        public const int ProductVolumeCubicYards = 366;
        public const int ContainerGrossVolumeCubicInches = 367;
        public const int ContainerGrossVolumeCubicFeet = 368;
        public const int ContainerGrossVolumeCubicYards = 369;
        public const int NumberofUnitsContained = 37;
        public const int CustomerPurchaseOrderNumber = 400;
        public const int ShipToLocationCode = 410;
        public const int BillToLocationCode = 411;
        public const int PurchaseFromLocationCode = 412;
        public const int ShipToPostalCodeSingle = 420;
        public const int ShipToPostalCodeMultiple = 421;
        public const int RollProducts = 8001;
        public const int ElectronicSerialNumber = 8002;
        public const int EANNumberSerialNumber = 8003;
        public const int EANSerialIdentification = 8004;
        public const int PriceperUnit = 8005;
        public const int CouponExtendedCode = 8100;
        public const int CouponExtendedCodeEndofOffer = 8101;
        public const int CouponExtendedCode0 = 8102;
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

        public Bitmap Generate(int aiCode, string value)
        {
            return Generate(aiCode, value, DefaultSettings);
        }

        public Bitmap Generate(int aiCode, int specifier, string value)
        {
            return Generate(aiCode, specifier, value, DefaultSettings);
        }

        public Bitmap Generate(int aiCode, int specifier, string value, BarcodeSettings settings)
        {
            return Generate(CreateEAN128(aiCode, specifier, value), settings);
        }

        public Bitmap Generate(int aiCode, string value, BarcodeSettings settings)
        {
            return Generate(CreateEAN128(aiCode,value), settings);
        }

        public Bitmap Generate(Dictionary<int, string> values)
        {
            return Generate(values, DefaultSettings);
        }

        public Bitmap Generate(Dictionary<int, string> values, BarcodeSettings settings)
        {
            StringBuilder s = new StringBuilder();

            s.Append(Code128Helper.StartVariantB);
            foreach (KeyValuePair<int, string> item in values)
            {
                s.Append(CreateEAN128(item.Key, item.Value));
            }

            return Generate(s.ToString(), settings);
        }

        protected override string ParseText(string value, CodedValueCollection codes)
        {
            value = base.ParseText(value, codes);

            return System.Text.RegularExpressions.Regex.Replace(value, "£(.+?)£", "($1)");
        }

        public string CreateEAN128(int aiCode, string value)
        {
            return string.Format("{0}£{1}£{2}", Code128Helper.FNC1, aiCode, value);
        }

        public string CreateEAN128(int aiCode, int specifier, string value)
        {
            return string.Format("{0}£{1}{2}£{3}", Code128Helper.FNC1, aiCode, specifier, value);
        }
    }
}
