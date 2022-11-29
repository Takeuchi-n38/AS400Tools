using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{

    public class RtvnetaStatement : CLLine
    {
        public readonly string Sysname;

        public RtvnetaStatement(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex)
        {
            //0027 RTVNETA    SYSNAME(&SYSTEM)
            var sysname = string.Empty;
            if (Value.Contains("SYSNAME("))
            {
                sysname = TextClipper.ClipParameter(joinedLine, "SYSNAME");
            }
            Sysname = sysname;
        }

    }
}
