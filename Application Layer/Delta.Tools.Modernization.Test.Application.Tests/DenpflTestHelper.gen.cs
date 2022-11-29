using Delta.AS400.DataTypes.Characters;
using Delta.AS400.Objects;
using Delta.Koubai01;
using Delta.Koubai01.Salelib.Denpfls;
//using Delta.Entities;
using System.Collections.Generic;
using System;
using Delta.Tools.Modernization.Test;
namespace Delta.Koubai01.Salelib.Denpfls
{
    public class DenpflTestHelper : EntityTestHelper
    {

        public static ObjectID ObjectIDOfEntity => Koubai01LibraryList.Salelib.ObjectIDOf("DENPFL");
        DenpflTestHelper():base(ObjectIDOfEntity)
        {
        }

        public static DenpflTestHelper Of => new DenpflTestHelper();

        public override IEnumerable<string> ToStringValues(byte[] CCSID930Bytes)
        {
            yield return CCSID930.ToStringFrom(CCSID930Bytes, 1, 2);
            yield return CCSID930.ToHexString(CCSID930Bytes, 1, 2);
            yield return CCSID930.ToStringFrom(CCSID930Bytes, 3, 14);
            yield return CCSID930.ToStringFromZone(CCSID930Bytes, 15, 20, 0);
            yield return CCSID930.ToStringFromZone(CCSID930Bytes, 21, 28, 0);
            yield return CCSID930.ToStringFromZone(CCSID930Bytes, 29, 36, 0);
        }

        public override IEnumerable<string> ToStringValuesWithQuote(string lineNumber,byte[] CCSID930Bytes, string quote)
        {
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 1, 2, quote);
            yield return $"{quote}{CCSID930.ToHexString(CCSID930Bytes, 1, 2)}{quote}";
            yield return ToStringValueFromStringWithQuote(CCSID930Bytes, 3, 14, quote);
            yield return CCSID930.ToStringFromZone(CCSID930Bytes, 15, 20, 0);
            yield return CCSID930.ToStringFromZone(CCSID930Bytes, 21, 28, 0);
            yield return CCSID930.ToStringFromZone(CCSID930Bytes, 29, 36, 0);
        }

        //public static Denpfl Create(byte[] CCSID930Bytes)
        //{
        //    var entity = new Denpfl();
        //    entity.Slipcd = CCSID930.ToStringFrom(CCSID930Bytes, 1, 2);
        //    entity.Slipnm = CCSID930.ToStringFrom(CCSID930Bytes, 3, 14);
        //    entity.Slipno = (int)CCSID930.ToDecimalFromZone(CCSID930Bytes, 15, 20, 0);
        //    entity.Opdt = (int)CCSID930.ToDecimalFromZone(CCSID930Bytes, 21, 28, 0);
        //    entity.Updt = (int)CCSID930.ToDecimalFromZone(CCSID930Bytes, 29, 36, 0);
        //    ((ISortaleBy4s)entity).Update4sValues();
        //    return entity;
        //}

    }
}

//0000      A*伝票№ファイル         PREFIX=DE
//0001      A                                      UNIQUE
//0002      A          R DENPFLR                   TEXT('伝票№ファイル')
//0003      A*
//0004      A            SLIPCD         2A         COLHDG('伝票種別コード')
//0005      A            SLIPNM        12O         COLHDG('伝票名')
//0006      A            SLIPNO         6S 0       COLHDG('伝票№')
//0007      A            OPDT           8S 0       COLHDG('作成日')
//0008      A            UPDT           8S 0       COLHDG('最終更新日')
//0009      A*
//0010      A          K SLIPCD
//1.0.0.1