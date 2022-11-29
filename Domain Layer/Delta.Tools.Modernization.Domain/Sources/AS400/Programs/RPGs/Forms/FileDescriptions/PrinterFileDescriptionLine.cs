using Delta.AS400.Objects;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class PrinterFileDescriptionLine : FileDescriptionLine, ISingleStatement
    {
        public readonly ObjectID FileObjectID;

        public PrinterFileDescriptionLine(ObjectID FileObjectID, string fileName, string fileType, string fileFormat, string RecordLength, string line, int originalLineStartIndex, int originalLineEndIndex) : base(fileName, fileType, fileFormat, RecordLength, line, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;
        }

    }
}
/*
 １～５	任意	メモやマークなど、使用目的は任意です。
６	仕様書コード	固定文字「Ａ」を記述します。
１７	タイプ	Ｒのとき、レコードレベルの記述、ブランクのときフィールドレベルの記述を意味します。
１９～２８	名前	レコード様式名、またはフィールド名を記述します。
３０～３４	桁数	フィールドレベルのとき、その桁数を右詰で記述します。
３５	データタイプ	フィールドのデータタイプを記述します。
３６～３７	少数以下の桁数	フィールドのデータタイプが数値の場合、小数点以下の桁数を右詰で記述します。
３８	使用目的	入力（Ｉ）か出力（Ｏ）か入出力かを記述します。印刷ファイルの場合、省略します。
３９～４１	行位置	フィールドの行位置を右詰で記述します。
４２～４４	カラム位置	フィールドのカラム位置を右詰で記述します。＋ｎとすると、前のフィールドとの間のブランク数です。
４５～80	機能	固定文字または、キーワードによる機能の記述をします。
 
 */