using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Ls
{
    public class LLine : Line, IRPGLine
    {
        FormType IRPGLine.FormType => FormType.L;

        public LLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {
        }

    }
}
