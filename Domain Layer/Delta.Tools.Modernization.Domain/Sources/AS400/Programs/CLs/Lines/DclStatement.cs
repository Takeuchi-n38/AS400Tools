using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class DclStatement : CLLine
    {

        public readonly string Var;

        public readonly string Type;
        readonly string Len;

        public readonly string InitialValue;

        public bool HasInitialValue => InitialValue != string.Empty;

        public readonly int Length;

        public int DecimalPositions { get; } = 0;

        public DclStatement(string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            //DCL   VAR(&CUST  )  TYPE(*CHAR)  LEN(4)
            //DCL   &BYTESPROV *CHAR 4         /* BYTES PROVIDED   */ VALUE(X'00000010')       /* 16 BYTE STRUCTURE*/
            Var = TextClipper.ClipParameter(joinedLine, "VAR");
            if (Var == string.Empty)
            {
                var varStart = joinedLine.Substring(joinedLine.IndexOf("&"));
                Var =varStart.Substring(0,varStart.IndexOf(" "));
            }

            Type = TextClipper.ClipParameter(joinedLine, "TYPE");
            /*
             *DEC
            パック10進数値を含む10進変数。
            *CHAR
            文字ストリング値を含む文字変数。
            *LGL
            論理値'1'または'0'のいずれかを含む論理変数。
            *INT
            符号付き2進値を含む整変数。
            *UINT
            符号なし2進値を含む整変数。
            *PTR
            アドレスが入っているポインター変数。
             */
            if (Type == string.Empty)
            {
                var typeStart = joinedLine.Substring(joinedLine.IndexOf("*"));
                Type = typeStart.Substring(0, typeStart.IndexOf(" "));
                var lenStart = typeStart.Substring(typeStart.IndexOf(" ")+1);
                if (lenStart.IndexOf("LEN") < 0)
                {
                    Len = lenStart.Substring(0, lenStart.IndexOf(" "));
                }
                else
                {
                    Len = TextClipper.ClipParameter(joinedLine, "LEN");
                }
            }
            else if(Type== "*LGL")
            {
                Len = "0";
            }
            else
            {
                Len = TextClipper.ClipParameter(joinedLine, " LEN");
            }

            if (joinedLine.Contains("VALUE"))
            {
                var v = TextClipper.ClipParameter(joinedLine, "VALUE").Replace("'", "").Trim();
                if (Type == "*CHAR")
                {
                    InitialValue = $"\"{v}\"";
                }
                else
                {
                    InitialValue = v;
                }
            }
            else
            {
                InitialValue = string.Empty;
            }

            var Lens = Len.Split(" ");
            if (Lens.Length == 2)
            {
                DecimalPositions = int.Parse(Lens[1]);
            }
            Length = int.Parse(Lens[0]);
        }

    }
}
