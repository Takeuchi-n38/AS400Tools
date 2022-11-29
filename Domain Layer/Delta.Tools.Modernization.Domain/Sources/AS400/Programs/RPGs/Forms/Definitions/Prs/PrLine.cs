using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Prs
{
    public class PrLine : RPGLine4, INestedBlockStartStatement
    {
        public PrLine(IRPGDefinitionLine4 line) : base(line.FormType, line.Value, line.StartLineIndex)
        {

        }

        public bool IsPrameter(string rpgName)
        {
            var extpgm = TextClipper.ClipParameter(Value, "EXTPGM").Replace("\'", string.Empty);
            return rpgName.ToUpper() == extpgm;
        }
    }
}
//     D PQEA700         PR                  EXTPGM('PQEA700') .
