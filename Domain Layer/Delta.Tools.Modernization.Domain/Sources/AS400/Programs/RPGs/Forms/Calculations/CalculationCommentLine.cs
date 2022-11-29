using Delta.Tools.Sources.Statements.Singles.Comments;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class CalculationCommentLine : CalculationLine, ICommentStatement
    {
        public CalculationCommentLine(string originalLine, int originalLineStartIndex) : base(true, originalLine, originalLineStartIndex, originalLineStartIndex)
        {

        }

    }
}
