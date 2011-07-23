using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Barcodes
{
    public static class EANHelper
    {
        public const int USACANADA_START = 00;
        public const int USACANADA_END = 13;
        public const int INSTORE_START = 20;
        public const int INSTORE_END = 29;
        public const int FRANCE_START = 30;
        public const int FRANCE_END = 37;
        public const int GERMANY_START = 40;
        public const int GERMANY_END = 44;
        public const int JAPAN1 = 45;
        public const int RUSSIANFEDERATION = 46;
        public const int TAIWAN = 471;
        public const int ESTONIA = 474;
        public const int LATVIA = 475;
        public const int LITHUANIA = 477;
        public const int SRILANKA = 479;
        public const int PHILIPPINES = 480;
        public const int UKRAINE = 482;
        public const int MOLDOVA = 484;
        public const int ARMENIA = 485;
        public const int GEORGIA = 486;
        public const int KAZAKHSTAN = 487;
        public const int HONGKONG = 489;
        public const int JAPAN2 = 49;
        public const int UNITEDKINGDOM = 50;
        public const int GREECE = 520;
        public const int LEBANON = 528;
        public const int CYPRUS = 529;
        public const int MACEDONIA = 531;
        public const int MALTA = 535;
        public const int IRELAND = 539;
        public const int BELGIUM_LUXEMBOURG = 54;
        public const int PORTUGAL = 560;
        public const int ICELAND = 569;
        public const int DENMARK = 57;
        public const int POLAND = 590;
        public const int ROMANIA = 594;
        public const int HUNGARY = 599;
        public const int SOUTHAFRICA_START = 600;
        public const int SOUTHAFRICA_END = 601;
        public const int MAURITIUS = 609;
        public const int MOROCCO = 611;
        public const int ALGERIA = 613;
        public const int TUNISIA = 619;
        public const int EGYPT = 622;
        public const int JORDAN = 625;
        public const int IRAN = 626;
        public const int FINLAND = 64;
        public const int CHINA_START = 690;
        public const int CHINA_END = 692;
        public const int NORWAY = 70;
        public const int ISRAEL = 729;
        public const int SWEDEN = 73;
        public const int GUATEMALA = 740;
        public const int ELSALVADOR = 741;
        public const int HONDURAS = 742;
        public const int NICARAGUA = 743;
        public const int COSTARICA = 744;
        public const int DOMINICANREPUBLIC = 746;
        public const int MEXICO = 750;
        public const int VENEZUELA = 759;
        public const int SWITZERLAND = 76;
        public const int COLOMBIA = 770;
        public const int URUGUAY = 773;
        public const int PERU1 = 775;
        public const int BOLIVIA = 777;
        public const int ARGENTINA = 779;
        public const int CHILE = 780;
        public const int PARAGUAY = 784;
        public const int PERU2 = 785;
        public const int ECUADOR = 786;
        public const int BRAZIL = 789;
        public const int ITALY_START = 80;
        public const int ITALY_END = 83;
        public const int SPAIN = 84;
        public const int CUBA = 850;
        public const int SLOVAKIA = 858;
        public const int CZECHREPUBLIC = 859;
        public const int YUGLOSLAVIA = 860;
        public const int TURKEY = 869;
        public const int NETHERLANDS = 87;
        public const int SOUTHKOREA = 880;
        public const int THAILAND = 885;
        public const int SINGAPORE = 888;
        public const int INDIA = 890;
        public const int VIETNAM = 893;
        public const int INDONESIA = 899;
        public const int AUSTRIA_START = 90;
        public const int AUSTRIA_END = 91;
        public const int AUSTRALIA = 93;
        public const int NEWZEALAND = 94;
        public const int MALAYSIA = 955;
        public const int ISSN = 977;//International Standard Serial Number for Periodicals
        public const int ISBN = 978;// International Standard Book Numbering (ISBN) 	 	
        public const int ISMN = 979;//International Standard Music Number (ISMN) 	 	
        public const int REFUNDRECEIPTS = 980;
        public const int COMMONCURRENCYCOUPONS_START = 981;
        public const int COMMONCURRENCYCOUPONS_END = 982;
        public const int COUPONS = 99;

        public const int UPC_REGULAR0 = 0;
        public const int UPC_INSTOREWEIGHT = 2;
        public const int UPC_DRUGHEALTH = 3;
        public const int UPC_INSTORENONFOOD = 4;
        public const int UPC_COUPONS = 5;
        public const int UPC_REGULAR7 = 7;

        public const int UPC5_DOLLARS = 50000;
        public const int UPC5_POUNDS = 0;
        public const int UPC5_NOSRP = 90000;
        public const int UPC5_NOCOST = 99991;
        public const int UPC5_USED = 99990;
    }
}
