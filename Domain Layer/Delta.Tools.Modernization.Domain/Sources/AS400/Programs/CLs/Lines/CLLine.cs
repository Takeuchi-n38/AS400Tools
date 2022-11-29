using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public abstract class CLLine : Line, ISingleStatement
    {

        protected CLLine(string value, int originalLineStartIndex, int originalLineEndIndex) : base(value, originalLineStartIndex, originalLineEndIndex)
        {

        }

        protected CLLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {

        }
        protected CLLine(int originalLineStartIndex) : base(originalLineStartIndex)
        {

        }

        public static bool IsJoinee(string originalLine)
        {
            return originalLine.EndsWith("+");
        }

        public static bool IsSingleLineComment(string singleLine) => singleLine.StartsWith(@"/*") && singleLine.EndsWith(@"*/");

    }
}
