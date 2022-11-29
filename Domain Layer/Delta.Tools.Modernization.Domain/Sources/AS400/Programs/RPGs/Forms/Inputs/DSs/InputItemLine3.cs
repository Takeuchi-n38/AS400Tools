using Delta.AS400.DataTypes;
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
    public class InputItemLine3 : RPGLine3, ISingleStatement
    {
        public InputItemLine3(IRPGInputLine3 line) : base(line.FormType,line.Value, line.StartLineIndex)
        {

        }

        public DataTypeDefinition TypeDefinition => DataTypeDefinition.Of(((IRPGInputLine3)this).FieldName, ((IRPGInputLine3)this).Length.ToString(), 
            string.Empty, ((IRPGInputLine3)this).DecimalPositions);

    }
}
