using Delta.AS400.DataTypes;
using Delta.Types.Fundamentals;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatFieldList : FirstClassList<IRecordFormatField>
    {
        public RecordFormatFieldList() : base(new List<IRecordFormatField>())
        {

        }

        public IEnumerable<IDataTypeDefinition> ITypeDefinitions => ItList().Where(f => f is IRecordFormatVariableField).Select(f => ((IRecordFormatVariableField)f).TypeDefinition);

        public IEnumerable<IRecordFormatOutputField> OutputFields => ItList().Where(f => f is IRecordFormatOutputField).Cast<IRecordFormatOutputField>();

        public int FirstLine => OutputFields.Min(f => f.Line);

        public int LineSpan => OutputFields.Max(f => f.Line) - FirstLine + 1;

    }
}
