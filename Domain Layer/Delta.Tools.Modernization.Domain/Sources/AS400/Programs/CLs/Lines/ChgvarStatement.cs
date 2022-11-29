using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class ChgvarStatement : CLLine
    {
        public readonly string VarName;
        public readonly string ValueOfVar;

        public ChgvarStatement(string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            VarName = TextClipper.ClipParameter(joinedLine, "VAR");
            ValueOfVar = TextClipper.ClipParameter(joinedLine, "VALUE");
        }

        //CHGVAR     VAR(&PGM) VALUE('CQEA120')
    }
}
