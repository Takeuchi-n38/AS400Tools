using Delta.AS400.DataTypes;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs
{
    public interface IRPGInputLine : IRPGLine
    {
        FormType IRPGLine.FormType => FormType.Input;

        public bool IsNestedBlockStartStatement => (Value.Substring(7, 1).Trim() != string.Empty);
        public bool IsDsLine => Value.Substring(18, 2).TrimEnd() == "DS";

        bool IsRefer=> ReferName!=string.Empty;
        string ReferName => Value.Substring(20, 7).TrimEnd();
        //     ICONTRLR
        //     I              CONVA10                         CNVA10
        //     INSIBFL  AA  01   1 CB   2 CC   3 C1
        public bool IsCondition => Value.Substring(23, 1).TrimEnd() != string.Empty & Value.Substring(24, 1).TrimEnd() == string.Empty;

        public string InternalDataType => Value.Substring(42, 1).TrimEnd();

        string FileName { get;}
        string FromLocation { get; }
        int FromLocationInIntType => ToInt(FromLocation);
        string ToLocation { get; }
        int ToLocationInIntType => ToInt(ToLocation);

        int ToInt(string value)
        {
            int result;
            if(int.TryParse(value,out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }
        public int Length => ToLocationInIntType - FromLocationInIntType + 1;
        public string DecimalPositions { get; }
        public int DecimalPositionsInIntType => ToInt(DecimalPositions);

        public string FieldName { get; }

        public IDataTypeDefinition ToTypeDefinition => DataTypeDefinition.Of(this.FieldName, this.Length.ToString(), InternalDataType, this.DecimalPositions);
    }
}
