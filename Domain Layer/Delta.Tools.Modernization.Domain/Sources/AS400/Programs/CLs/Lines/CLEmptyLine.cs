using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CLEmptyLine : CLLine, IEmptyStatement
    {

        CLEmptyLine(int originalLineStartIndex) : base(originalLineStartIndex)
        {

        }

        public static CLEmptyLine Of(int originalLineStartIndex)
        {
            return new CLEmptyLine(originalLineStartIndex);
        }

    }
}
