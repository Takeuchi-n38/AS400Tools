using Delta.AS400.DataTypes.Characters;
using Delta.AS400.DataTypes.Numerics;
using Delta.AS400.Objects;
using Delta.Honsha01;
using Delta.Honsha01.Iidlib.Carsets;
using Delta.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using Delta.Tools.Modernization.Test;
namespace Delta.Honsha01.Iidlib.Carsets
{
    public class CarsetTestHelper : EFEntityTestHelper<Carset>
    {

        public static ObjectID ObjectIDOfEntity => Honsha01LibraryList.Iidlib.ObjectIDOf("CARSET");
        CarsetTestHelper():base(ObjectIDOfEntity)
        {
        }

        public static CarsetTestHelper Of => new CarsetTestHelper();

        public override IEnumerable<string> ToStringValues(byte[] CCSID930Bytes)
        {
            yield return ZonedDecimal.ToStringFrom(CCSID930Bytes, 1, 5, 0);
            yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip(1 - 1).Take(5 - (1 - 1))).PadLeft(5-1+1,'0');
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 6, 9);
            yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip(6 - 1).Take(9 - (6 - 1)));
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 10, 12);
            yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip(10 - 1).Take(12 - (10 - 1)));
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 13, 14);
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 15, 16);
            yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip(15 - 1).Take(16 - (15 - 1)));
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 17, 32);
            yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip(17 - 1).Take(32 - (17 - 1)));
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 33, 36);
            yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip(33 - 1).Take(36 - (33 - 1)));
            yield return ZonedDecimal.ToStringFrom(CCSID930Bytes, 37, 44, 0);
            yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip(37 - 1).Take(44 - (37 - 1))).PadLeft(44-37+1,'0');
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 45, 60);
            yield return BytesExtensions.ToHexString(CCSID930Bytes.Skip(45 - 1).Take(60 - (45 - 1)));
            yield return ZonedDecimal.ToStringFrom(CCSID930Bytes, 61, 68, 0);
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 69, 84);
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 85, 85);
            yield return CodePage930.ToStringFrom(CCSID930Bytes, 86, 86);
        }

        public override IEnumerable<string> ToStringValuesWithQuote(string lineNumber,byte[] CCSID930Bytes, string quote)
        {
            yield return ZonedDecimal.ToStringFrom(CCSID930Bytes, 1, 5, 0);
            yield return $"{quote}{ZonedDecimal.To4sStringFrom(CCSID930Bytes, 1, 5, 0)}{quote}";
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 6, 9, quote);
            yield return $"{quote}{BytesExtensions.ToHexString(CCSID930Bytes.Skip(6 - 1).Take(9 - (6 - 1)))}{quote}";
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 10, 12, quote);
            yield return $"{quote}{BytesExtensions.ToHexString(CCSID930Bytes.Skip(10 - 1).Take(12 - (10 - 1)))}{quote}";
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 13, 14, quote);
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 15, 16, quote);
            yield return $"{quote}{BytesExtensions.ToHexString(CCSID930Bytes.Skip(15 - 1).Take(16 - (15 - 1)))}{quote}";
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 17, 32, quote);
            yield return $"{quote}{BytesExtensions.ToHexString(CCSID930Bytes.Skip(17 - 1).Take(32 - (17 - 1)))}{quote}";
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 33, 36, quote);
            yield return $"{quote}{BytesExtensions.ToHexString(CCSID930Bytes.Skip(33 - 1).Take(36 - (33 - 1)))}{quote}";
            yield return ZonedDecimal.ToStringFrom(CCSID930Bytes, 37, 44, 0);
            yield return $"{quote}{ZonedDecimal.To4sStringFrom(CCSID930Bytes, 37, 44, 0)}{quote}";
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 45, 60, quote);
            yield return $"{quote}{BytesExtensions.ToHexString(CCSID930Bytes.Skip(45 - 1).Take(60 - (45 - 1)))}{quote}";
            yield return ZonedDecimal.ToStringFrom(CCSID930Bytes, 61, 68, 0);
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 69, 84, quote);
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 85, 85, quote);
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 86, 86, quote);
        }

        public static Carset Create(byte[] CCSID930Bytes)
        {
            var entity = new Carset();
            entity.Crpw = (int)ZonedDecimal.ToDecimalFrom(CCSID930Bytes, 1, 5, 0);
            entity.Crdpn = CodePage930.ToStringFrom(CCSID930Bytes, 6, 9);
            entity.Crcol = CodePage930.ToStringFrom(CCSID930Bytes, 10, 12);
            entity.Criro = CodePage930.ToStringFrom(CCSID930Bytes, 13, 14);
            entity.Crpos = CodePage930.ToStringFrom(CCSID930Bytes, 15, 16);
            entity.Crspn = CodePage930.ToStringFrom(CCSID930Bytes, 17, 32);
            entity.Crhri = CodePage930.ToStringFrom(CCSID930Bytes, 33, 36);
            entity.Crymd = (int)ZonedDecimal.ToDecimalFrom(CCSID930Bytes, 37, 44, 0);
            entity.Crpn = CodePage930.ToStringFrom(CCSID930Bytes, 45, 60);
            entity.Crqty = (int)ZonedDecimal.ToDecimalFrom(CCSID930Bytes, 61, 68, 0);
            entity.Crnpn = CodePage930.ToStringFrom(CCSID930Bytes, 69, 84);
            entity.Crnai = CodePage930.ToStringFrom(CCSID930Bytes, 85, 85);
            entity.Crbit = CodePage930.ToStringFrom(CCSID930Bytes, 86, 86);
            ((ISortaleBy4s)entity).Update4sValues();
            return entity;
        }

        public override IEnumerable<string> ColumnNames()
        {
            yield return "CRPW";
            yield return "CRPW4s";
            yield return "CRDPN";
            yield return "CRDPN4s";
            yield return "CRCOL";
            yield return "CRCOL4s";
            yield return "CRIRO";
            yield return "CRPOS";
            yield return "CRPOS4s";
            yield return "CRSPN";
            yield return "CRSPN4s";
            yield return "CRHRI";
            yield return "CRHRI4s";
            yield return "CRYMD";
            yield return "CRYMD4s";
            yield return "CRPN";
            yield return "CRPN4s";
            yield return "CRQTY";
            yield return "CRNPN";
            yield return "CRNAI";
            yield return "CRBIT";
        }

        public override IEnumerable<string> ToStringValuesWithQuote(Carset entity, string quote)
        {
            yield return entity.Crpw.ToString();
            yield return $"{quote}{entity.Crpw4s}{quote}";
            yield return $"{quote}{entity.Crdpn}{quote}";
            yield return $"{quote}{entity.Crdpn4s}{quote}";
            yield return $"{quote}{entity.Crcol}{quote}";
            yield return $"{quote}{entity.Crcol4s}{quote}";
            yield return $"{quote}{entity.Criro}{quote}";
            yield return $"{quote}{entity.Crpos}{quote}";
            yield return $"{quote}{entity.Crpos4s}{quote}";
            yield return $"{quote}{entity.Crspn}{quote}";
            yield return $"{quote}{entity.Crspn4s}{quote}";
            yield return $"{quote}{entity.Crhri}{quote}";
            yield return $"{quote}{entity.Crhri4s}{quote}";
            yield return entity.Crymd.ToString();
            yield return $"{quote}{entity.Crymd4s}{quote}";
            yield return $"{quote}{entity.Crpn}{quote}";
            yield return $"{quote}{entity.Crpn4s}{quote}";
            yield return entity.Crqty.ToString();
            yield return $"{quote}{entity.Crnpn}{quote}";
            yield return $"{quote}{entity.Crnai}{quote}";
            yield return $"{quote}{entity.Crbit}{quote}";
        }

    }
}

//0000      A*カーセットテーブル
//0001      A*
//0002      A                                      UNIQUE
//0003      A          R CARSETR                   TEXT('レコード様式')
//0004      A*
//0005      A            CRPW           5S 0       COLHDG('マツダ計画週')
//0006      A            CRDPN          4A         COLHDG('代表')
//0007      A            CRCOL          3A         COLHDG('内装色')
//0008      A            CRIRO          2A         COLHDG('色')
//0009      A            CRPOS          2A         COLHDG('位置')
//0010      A            CRSPN         16A         COLHDG('セット品番')
//0011      A            CRHRI          4A         COLHDG('払い')
//0012      A            CRYMD          8S 0       COLHDG('年月')
//0013      A            CRPN          16A         COLHDG('シート品番')
//0014      A            CRQTY          8S 0       COLHDG('数量')
//0015      A            CRNPN         16A         COLHDG('実際構成品番')
//0016      A            CRNAI          1A         COLHDG('内示有無')
//0017      A            CRBIT          1A         COLHDG('品番割出済')
//0018      A*
//0019      A          K CRPW
//0020      A          K CRDPN
//0021      A          K CRCOL
//0022      A          K CRPOS
//0023      A          K CRSPN
//0024      A          K CRHRI
//0025      A          K CRYMD
//0026      A          K CRPN
//1.0.0.1