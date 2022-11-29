using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dss
{
    public class DsItemLine4 : RPGLine4, ISingleStatement
    {
        public DsItemLine4(IRPGDefinitionLine4 line) : base(line.FormType, line.Value, line.StartLineIndex)
        {

        }

    }
}
