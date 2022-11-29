using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.DDSs
{
    public class UnKnownDdsLine : DDSLine, ISingleStatement
    {
        public UnKnownDdsLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {

        }

    }
}
