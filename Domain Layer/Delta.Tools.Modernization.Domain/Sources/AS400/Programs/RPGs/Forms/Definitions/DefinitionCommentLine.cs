using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Singles.Comments;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public class DefinitionCommentLine : RPGLine4, ICommentStatement
    {

        public DefinitionCommentLine(string originalLine, int originalLineStartIndex) : base(FormType.Definition, originalLine, originalLineStartIndex)
        {

        }

        //string ICommentStatement.Comment => $"{OriginalComment}";

    }
}
