using Delta.Tools.Sources.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.DDSs.FMTs
{
    //見出し仕様
    //見出し仕様では、出力ファイルのタイプ、ソートキーの最大長さなどを記述します。
    //６桁目の仕様書コードを'Ｈ'とします。６桁目を＊にすると注釈となります。
    //見出し仕様の記述要領は次のとおりです。
    public interface IFormatDataHeaderLine: IFormatDataLine
    {
        //６桁 Ｈ   仕様書コードを'Ｈ'とします。

        //7～12桁 		出力ファイルのタイプ。次の３種類が指定できます。
        string OutputType => Value.Substring(6,6);
        //SORTR 	通常のソート結果
        bool IsSort => OutputType.TrimEnd()== "SORTR";
        bool IsSortAndSummary => OutputType.TrimEnd() == "SORTRS";//SORTRS	通常のソートとサマリー
        //SORTA	出力ファイルは相対レコード番号（ＲＲＮ）が４バイトの２進数でセットされます。

        //15～17桁 		ソートキーのフィールドの最大長
        //ブランク	制御フィールドなしを意味します。
        //1-256	最大制御フィールド（ソートキー）のバイト数        
        int SortKeyLength => int.Parse(Value.Substring(14, 3).TrimStart());

        //18桁 昇順／降順の別
        //ブランク
        //A 制御フィールドによる昇順を意味します。
        //D 降順を意味します。
        string OrderType => Value.Substring(17, 1);
        bool IsAsc => OrderType=="A";

        //28桁		ソートキーのフィールドを出力への組込むか否かの指定
        //ブランク	ソートキーのフィールドは出力レコードに組込まれる。
        //X	組込まれない。
        string SortKeyOutputType => Value.Substring(27, 1);
        bool IsSortKeyOutput => SortKeyOutputType == "X";
    }
}
