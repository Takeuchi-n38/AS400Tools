using Delta.AS400.DataTypes;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatBindField : RecordFormatOutputField, IRecordFormatVariableField
    {

        public IDataTypeDefinition TypeDefinition { get; }
        public bool IsReadOnly { get; }

        public RecordFormatBindField(IDisplayFileLine dfileLine,int line, int position, IDataTypeDefinition typeDefinition, bool IsReadOnly) : base(dfileLine.Value, dfileLine.StartLineIndex,line,position)
        {
            TypeDefinition = typeDefinition;
            this.IsReadOnly = IsReadOnly;
        }
    }
}
