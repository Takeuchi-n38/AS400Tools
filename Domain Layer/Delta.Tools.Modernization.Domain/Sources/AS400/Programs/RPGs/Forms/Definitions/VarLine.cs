using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public class VarLine : RPGLine4, ISingleStatement
    {

        public VarLine(IRPGDefinitionLine4 line) : base(line.FormType, line.Value, line.StartLineIndex)
        {

        }

    }
}
