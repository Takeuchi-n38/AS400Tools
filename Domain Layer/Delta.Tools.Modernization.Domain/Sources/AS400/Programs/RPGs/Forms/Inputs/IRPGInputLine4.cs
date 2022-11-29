using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs
{
    public interface IRPGInputLine4 : IRPGInputLine
    {

        string IRPGInputLine.FileName => Value.Substring(6, 10).TrimEnd();

        //Position 6 (Form Type)
        //Positions 7-30 (Reserved)
        //Positions 31-34 (Data Attributes - External)
        //Position 35 (Date/Time Separator)
        //Position 36 (Data Format)
        //Positions 37-46 (Field Location)
        string IRPGInputLine.FromLocation => Value.Substring(39, 2).Trim();
        string IRPGInputLine.ToLocation => Value.Substring(44, 2).Trim();
        //Positions 47-48 (Decimal Positions)
        string IRPGInputLine.DecimalPositions => Value.Substring(46, 2).Trim();
        //Positions 49-62 (Field Name)
        string IRPGInputLine.FieldName => Value.Substring(48, 14).Trim();

        //Positions 63-64 (Control Level)
        //Positions 65-66 (Matching Fields)
        //Positions 67-68 (Field Record Relation)
        //Positions 69-74 (Field Indicators - Program Described)
    }
}
