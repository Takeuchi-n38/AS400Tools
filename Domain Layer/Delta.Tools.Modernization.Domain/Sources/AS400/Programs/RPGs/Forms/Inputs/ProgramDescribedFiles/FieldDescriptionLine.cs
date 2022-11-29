using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles
{
    public class FieldDescriptionLine : InputLine, ISingleStatement
    {
        //Position 6 (Form Type)
        //Positions 7-30 (Reserved)
        //Positions 31-34 (Data Attributes - External)
        //Position 35 (Date/Time Separator)
        //Position 36 (Data Format)
        //Positions 37-46 (Field Location)
        string FromLocation => Value.Substring(39, 2).Trim();
        string ToLocation => Value.Substring(44, 2).Trim();
        public int Length => int.Parse(ToLocation) - int.Parse(FromLocation) + 1;
        //Positions 47-48 (Decimal Positions)
        public string DecimalPositions => Value.Substring(46, 2).Trim();
        //Positions 49-62 (Field Name)
        public string FieldName => Value.Substring(48, 14).Trim();

        //Positions 63-64 (Control Level)
        //Positions 65-66 (Matching Fields)
        //Positions 67-68 (Field Record Relation)
        //Positions 69-74 (Field Indicators - Program Described)

        public FieldDescriptionLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {
        }

        //internal string ModernTypeName
        //{
        //    get
        //    {
        //        if (DecimalPositions == string.Empty) return "string";
        //        if (int.Parse(DecimalPositions) == 0)
        //        {
        //            if (Length > 9) return "long";
        //            if (Length > 4) return "int";
        //            return "short";
        //        }
        //        if (int.Parse(DecimalPositions) > 0) return "decimal";
        //        throw new ArgumentException();
        //    }
        //}
    }
}
/*
 ファイル名
7-14桁目	ファイル名	
15-16桁目	順序	AA、BBなど適当な文字
19-20桁目	レコード識別標識	読取時にONになる標識番号
無くてもOK
フィールド名
43桁目	P/B/L/R	パック10進数の場合にはPを入れる
44-47桁目	開始位置	フィールドの桁位置の開始位置
48-51桁目	終了位置	フィールドの桁位置の終了位置
52桁目	小数部桁数	数値フィールドの場合に小数部桁数を指定
53-58桁目	フィールド名	内部記述フィールド名
65-70	結果標識	結果標識の受取
 */