using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public class DifinitionBlockItemLine : RPGLine4, ISingleStatement
    {
        public DifinitionBlockItemLine(IRPGDefinitionLine4 line) : base(line.FormType, line.Value, line.StartLineIndex)
        {

        }

    }
}
