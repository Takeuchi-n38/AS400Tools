using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.FileDescriptions
{
    public class UnKnownFileDescriptionLine : FileDescriptionLine, ISingleStatement
    {
        public UnKnownFileDescriptionLine(string line, int originalLineStartIndex, int originalLineEndIndex) : base(line, originalLineStartIndex, originalLineEndIndex)
        {

        }

    }
}
