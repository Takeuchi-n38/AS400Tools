using Delta.AS400.DataTypes;
using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public interface IRPGDefinitionLine4 : IRPGDefinitionLine, IDataTypeDefinition
    {
        //Positions 7-21 (Name)
        public new string Name => Value[6..21].TrimEnd();
        string IRPGDefinitionLine.Name => Name;

        string IDataTypeDefinition.Name => Name;

        //Position 22 (External Description)
        //Position 23 (Type of Data Structure)
        //Positions 24-25 (Definition Type)
        public virtual string DefinitionType => Value.Substring(23, 2).Trim();

        //Positions 26-32 (From Position)
        public string FromPosition => Value.Substring(25, 7).Trim();
        int FromIndex => FromPosition == string.Empty ? 0 : int.Parse(FromPosition) - 1;

        //Positions 33-39 (To Position / Length)
        string IRPGDefinitionLine.ToPosition_Length => Value[32..39].Trim();

        string IDataTypeDefinition.Length => ((ToPosition_Length == string.Empty ? 0 : int.Parse(ToPosition_Length)) - FromIndex).ToString();
        //     E    HRITABLE        TABKEY  1 999  4   TABDAT  3              

        //Position 40 (Internal Data Type)
        //ブランク 文字列
        //Ｓ ゾーン１０進数
        //Ｐ パック１０進数
        //Ｂ	２進数
        //＊	ポインター
        string IDataTypeDefinition.InternalDataType => Value.Substring(39, 1).Trim();

        //Positions 41-42 (Decimal Positions)
        public new string DecimalPositions => Value[40..42].Trim();
        string IRPGDefinitionLine.DecimalPositions => DecimalPositions;
        string IDataTypeDefinition.DecimalPositions => DecimalPositions;

        //Position 43 (Reserved)
        //Positions 44-80 (Keywords)
        public string Keywords => Value[43..80];

        string IRPGDefinitionLine.Perrcd => TextClipper.ClipParameter(Keywords, "PERRCD");

        //public  Variable Of(IRPGDefinitionLine4 line) => Of(((ITypeDefinition)TypeDefinition.Of(ICSharpGeneratable.ToCSharpOperand(line.Name), ((line.ToPosition_Length == string.Empty ? 0 : int.Parse(line.ToPosition_Length)) - line.FromIndex).ToString(), line.InternalDataType, line.DecimalPositions)));

    }
}
