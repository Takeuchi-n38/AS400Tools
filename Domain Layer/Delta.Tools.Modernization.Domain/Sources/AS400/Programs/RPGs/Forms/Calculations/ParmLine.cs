using Delta.AS400.DataTypes;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class ParmLine : CalculationLine, ISingleStatement, IDataTypeDefinition
    {

        string IDataTypeDefinition.Name => ResultField;

        string IDataTypeDefinition.Length => FieldLength;

        string IDataTypeDefinition.InternalDataType => string.Empty;

        string IDataTypeDefinition.DecimalPositions => DecimalPositions;

        public ParmLine(CalculationLine RPGCLine) : base(RPGCLine)
        {

        }

    }
}

/*
     C     *ENTRY        PLIST
     C                   PARM                    P\DATAID          2
     C                   PARM                    P\KBKB            3
 */
