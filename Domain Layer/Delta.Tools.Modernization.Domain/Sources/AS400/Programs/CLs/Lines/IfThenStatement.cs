using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class IfThenStatement : CLLine
    {
        public readonly string Cond;
        public readonly string Then;

        public IfThenStatement(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex, originalLineStartIndex)
        {
            //IF COND(&IN03 = '1') THEN(RETURN)
            //IF (&BIT = ' ') THEN(GOTO CMDLBL(LOOP))
            if (Value.Contains("COND"))
            {
                Cond = TextClipper.ClipParameter(joinedLine, "COND");
            }
            else
            {
                if (Value.EndsWith("DO"))
                {
                    //IF         (&IN93 *EQ '1') DO
                    var s = Value.LastIndexOf("(");
                    var e = Value.LastIndexOf(")");
                    Cond = Value.Substring(s + 1, e - s - 1);
                }
                else
                {
                    var cond = Value.Contains("THEN(")? Value.Substring(2, Value.LastIndexOf("THEN(") - 2):Value;
                    var s = cond.LastIndexOf("(");
                    var e = cond.LastIndexOf(")");
                    Cond = cond.Substring(s + 1, e - s - 1);
                }
            }

            Then = TextClipper.ClipParameter(joinedLine, "THEN");
        }

    }
}
