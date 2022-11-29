using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs
{
    public interface IRPGOutputLine3 : IRPGOutputLine
    {
        //レコード   7～14桁	ファイル名
        string IRPGOutputLine.FileName => Value.Substring(6, 8).TrimEnd();

        //レコード   15桁	サイクル外の出力を行うとき、Ｅをセット
        string IRPGOutputLine.LineNameMark => Value.Substring(14, 1);

        //16-18桁目 ADD/DEL ADD：レコード追加DEL：レコード削除ブランク：レコード更新
        string IRPGOutputLine.UpdateType => Value.Substring(15, 3);

        //レコード   32～37桁	レコード様式名
        //フィールド 32～37桁 フィールド名
        string IRPGOutputLine.Name => (LineNameMark == "D" || LineNameMark == "T")? LineNameMark: Value.Substring(31, 6).TrimEnd();

        //フィールド 38桁目 編集コード   DSPFのEDTCDEキーワードと同じ編集コード
        string IRPGOutputLine.EditCodes => Value.Substring(37, 1).TrimEnd();

        //フィールド 40～43桁	フィールドの終りの桁位置を指定します
        string IRPGOutputLine.EndPositionInLine => Value.Substring(39, 4).TrimStart();

        //45-70桁目	固定情報/編集語	‘（シングルクォーテーション）で括る
        //編集語も同様に’    /  /  ’のように編集語とスペースを’で括る
        string IRPGOutputLine.StaticValue => Value.Substring(44, 26).Contains('\'')? Value.Substring(44, Value.LastIndexOf('\'')-43).Trim() : Value.Substring(44, 26).Trim();

        //17-18桁目 スペース前・後 出力するレコードの前後にいくつ行スペース（改行）を置くか指定 ブランクもしくは0-3
        int IRPGOutputLine.SpaceBefore => ToIntForSpaceAndSkip(Value.Substring(16, 1));
        int IRPGOutputLine.SpaceAfter => ToIntForSpaceAndSkip(Value.Substring(17, 1));
        //19-22桁目 スキップ前・後 スキップする行番号の指定
        //01-99：1-99
        //A0-A9：100-109
        //B0-B2：110-112
        int IRPGOutputLine.SkipBefore => ToIntForSpaceAndSkip(Value.Substring(18, 2));
        int IRPGOutputLine.SkipAfter => ToIntForSpaceAndSkip(Value.Substring(20, 2));

        static int ToIntForSpaceAndSkip(string original)
        {
            if (original.Trim() == string.Empty) return 0;
            if (original.Trim().Length == 2)
            {
                if (original.Trim()[0] == 'A') return 100 + int.Parse(original.Trim()[1].ToString());
                if (original.Trim()[0] == 'B') return 110 + int.Parse(original.Trim()[1].ToString());
            }
            return int.Parse(original.Trim());
        }
    }
}
/*
     OFILEB   D        01 10
     O                         DATA     128
     O                                  108 '    '
     OFILEC   D        01 20
     O                         DATA     128
     O                                  108 '    '

     OPRINT   D  201   L1                                             
     O       OR        OFNL1                                          
     O                                    9 'PLFB285 '                

 */
/*
レコード
7-14桁目	ファイル名	出力ファイル名
15桁目	タイプ	H：見出レコード
D：明細レコード
T：合計レコード
E：例外レコード
17-18桁目	スペース前・後	出力するレコードの前後にいくつ行スペース（改行）を置くか指定
　ブランクもしくは0-3
19-22桁目	スキップ前・後	スキップする行番号の指定
　01-99：1-99
　A0-A9：100-109
　B0-B2：110-112
19-20桁目	条件標識	レコード様式を出力する為の条件標識
32-37桁目	例外レコード名	EXCPT名


フィールド
23-31桁目	条件標識	フィールド毎の出力条件
32-37桁目	フィールド名	出力するフィールド
39桁目	後で消去	B：後で消去
（Bを指定すると指定行のフィールドを出力後にクリア）
40-43桁目	終了桁	終了する桁を指定
+nnnで前のフィールドの最終桁を参照
45-70桁目	固定情報/編集語	‘（シングルクォーテーション）で括る
編集語も同様に’    /  /  ’のように編集語とスペースを’で括る
 * */

/*
 * レコードレベル
 *7-14桁目	ファイル名	内部記述：ファイル名
外部記述：レコード様式名
15桁目	タイプ	H：見出し
D：明細
T：合計
E：例外

16-18桁目	ADD/DEL	ADD：レコード追加DEL：レコード削除ブランク：レコード更新
23-31桁目	条件標識	任意の標識
32-37桁目	EXCPT名	タイプ：Eの例外出力の場合のEXCPT名

フィールドレベル
	内部記述	外部記述
終了桁	必要	不要
パック10進数（44桁目：P）	必要	不要
固定値の使用	OK	NG

 */