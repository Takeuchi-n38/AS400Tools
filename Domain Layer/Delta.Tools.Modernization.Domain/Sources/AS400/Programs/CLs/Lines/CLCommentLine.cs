using Delta.Tools.Sources.Statements.Singles.Comments;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CLCommentLine : CLLine, ICommentStatement
    {

        CLCommentLine(string singleLineComment, int originalLineStartIndex) : base(singleLineComment, originalLineStartIndex)
        {

        }

        public static CLCommentLine Of(string singleLineComment, int originalLineStartIndex)
        {
            return new CLCommentLine(singleLineComment, originalLineStartIndex);
        }

    }
}
