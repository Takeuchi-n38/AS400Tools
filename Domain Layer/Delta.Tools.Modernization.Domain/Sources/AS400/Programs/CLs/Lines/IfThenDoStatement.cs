using Delta.Tools.Sources.Statements.Blocks.Ifs;
using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class IfThenDoStatement : CLLine, IIfBlockStartStatement
    {

        public readonly string Cond;
        public readonly string Then;

        public IfThenDoStatement(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex, originalLineStartIndex)
        {
            //0031 IF       COND(&IN09 = '1') THEN(DO)
            //0032 WRKSPLF    SELECT(SALE)
            //0033 ENDDO            
            Cond = TextClipper.ClipParameter(joinedLine, "COND");
            Then = TextClipper.ClipParameter(joinedLine, "THEN");
        }

    }
}
