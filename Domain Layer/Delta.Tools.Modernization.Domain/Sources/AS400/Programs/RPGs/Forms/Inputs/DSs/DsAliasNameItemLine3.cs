using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.DSs
{
    public class DsAliasNameItemLine3 : RPGLine3, ISingleStatement
    {
        public DsAliasNameItemLine3(IRPGInputLine3 line) : base(line.Value, line.StartLineIndex)
        {

        }

    }
}
