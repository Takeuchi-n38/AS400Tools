using Delta.AS400.DataTypes;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatFieldLine : DDSLine, IDataTypeDefinition
    {
        string IDataTypeDefinition.Name => ((IDDSLine)this).Name;

        string IDataTypeDefinition.Length => ((IDDSLine)this).Length;

        string IDataTypeDefinition.InternalDataType => ((IDDSLine)this).DataType;

        string IDataTypeDefinition.DecimalPositions => ((IDDSLine)this).DecimalPositions;

        string IDataTypeDefinition.Summary => ((IDDSLine)this).Colhdg;

        public RecordFormatFieldLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {

        }

    }
}
