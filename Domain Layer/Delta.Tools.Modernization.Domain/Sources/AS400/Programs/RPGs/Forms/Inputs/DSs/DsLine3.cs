using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.DSs
{
    public class DsLine3 : RPGLine3, INestedBlockStartStatement
    {
        public DsLine3(IRPGInputLine3 line) : base(FormType.Input, line.Value, line.StartLineIndex)
        {

        }

    }

}
