namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class SndfStatement : CLLine
    {
        public readonly string Rcdfmt;
        public SndfStatement(string rcdfmt, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            Rcdfmt = rcdfmt;
        }

    }

}
