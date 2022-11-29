using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Pis
{
    public class PiLine : RPGLine4, INestedBlockStartStatement
    {
        public PiLine(IRPGDefinitionLine4 line) : base(line.FormType, line.Value, line.StartLineIndex)
        {

        }

    }
}
