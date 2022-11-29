using Delta.AS400.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Extensions
{
    public interface IRPGExtensionLine3: IRPGExtensionLine,IDataTypeDefinition
    {
        //27-32	配列名 任意の配列名
        string IDataTypeDefinition.Name => Value.Substring(26, 6).TrimEnd();
        string IRPGExtensionLine.Name => Value.Substring(26, 6).TrimEnd();

        //33-35	1レコードあたりの項目数	コンパイル時配列・テーブルの場合に使用
        string IRPGExtensionLine.Perrcd => Value.Substring(32, 3).TrimStart();

        //36-39	配列の要素数	1-9999
        string IRPGExtensionLine.ArrayLength => Value.Substring(35, 4).TrimStart();

        //40-42	要素の桁長	1-256
        string IDataTypeDefinition.Length => Value.Substring(39, 3).TrimStart();
        string IRPGExtensionLine.ItemLength => Value.Substring(39, 3).TrimStart();

        //44	小数点以下の桁数	数値の場合に指定（0-9）
        string IDataTypeDefinition.DecimalPositions => Value.Substring(43, 1).TrimStart();
        string IRPGExtensionLine.DecimalPositions => Value.Substring(43, 1).TrimStart();

        string IDataTypeDefinition.InternalDataType => string.Empty;

    }
}
/*
実行時配列
 ...+... 1 ...+... 2 ...+... 3 ...+... 4 ...+... 5 ...+... 6 ...+... 7
     E                    ARY        12  9 0              売上 
桁	意味	備考
6	仕様書コード	E
27-32	配列名	任意の配列名
36-39	配列の要素数	1-9999
40-42	要素の桁長	1-256
44	小数点以下の桁数	数値の場合に指定（0-9）
45	順序（昇順・降順）	A：昇順　D：降順
SORTA命令やLOKUP命令などに使用
58-74	注釈

コンパイル時配列
 ...+... 1 ...+... 2 ...+... 3 ...+... 4 ...+... 5 ...+... 6 ...+... 7
     E                    ARY    12  12  2 0              月度 
**
040506070809101112010203
桁	意味	備考
6	仕様書コード	E
27-32	配列名	任意の配列名
33-35	1レコードあたりの項目数	コンパイル時配列・テーブルの場合に使用
36-39	配列の要素数	1-9999
40-42	要素の桁長	1-256
44	小数点以下の桁数	数値の場合に指定（0-9）
45	順序（昇順・降順）	A：昇順　D：降順
SORTA命令やLOKUP命令などに使用
58-74	注釈

テーブル（交差配列）
 ...+... 1 ...+... 2 ...+... 3 ...+... 4 ...+... 5 ...+... 6 ...+... 7
     E                    TABCD   6   6  1   TABDEC  2 0  コード 
**
A11B22C33D44E55F66
桁	意味	備考
6	仕様書コード	E
27-32	テーブル名	TABで始まる任意のテーブル名
33-35	1レコードあたりの項目数	コンパイル時配列・テーブルの場合に使用
36-39	テーブルの項目数	1-9999
40-42	要素の桁長	1-256
44	小数点以下の桁数	数値の場合に指定（0-9）
45	順序（昇順・降順）	A：昇順　D：降順
LOKUP命令などに使用
46-51	テーブル名（交互形式のもう一つ）	TABで始まる任意のテーブル名
52-54	要素の桁長	1-256
56	小数点以下の桁数	数値の場合に指定（0-9）
57	順序（昇順・降順）	A：昇順　D：降順
LOKUP命令に使用
58-74	注釈	

*/