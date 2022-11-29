using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Extensions.Dims
{
    public class DimLine3 : RPGLine3, ISingleStatement
    {

        public DimLine3(IRPGExtensionLine3 line) : base(FormType.Extension, line.Value, line.StartLineIndex)
        {
        }
    }
}
