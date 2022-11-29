using Delta.Tools.Sources.Statements.Singles.Comments;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class FileDescriptionCommentLine : FileDescriptionLine, ICommentStatement
    {

        public FileDescriptionCommentLine(string originalLine, int originalLineStartIndex, int originalLineEndIndex) : base(originalLine, originalLineStartIndex, originalLineEndIndex)
        {

        }
        public FileDescriptionCommentLine(string originalLine, int originalLineStartIndex) : this(originalLine, originalLineStartIndex, originalLineStartIndex)
        {

        }

    }
}
