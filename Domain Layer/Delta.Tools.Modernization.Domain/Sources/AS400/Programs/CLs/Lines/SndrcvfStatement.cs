using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class SndrcvfStatement : CLLine
    {
        public readonly string Rcdfmt;
        public SndrcvfStatement(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex, originalLineStartIndex)
        {
            Rcdfmt = TextClipper.ClipParameter(joinedLine, "RCDFMT");
        }

    }
}
