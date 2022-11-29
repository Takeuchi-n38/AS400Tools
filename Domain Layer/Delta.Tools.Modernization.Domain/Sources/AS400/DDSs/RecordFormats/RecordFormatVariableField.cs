using Delta.AS400.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatVariableField : IRecordFormatField, IRecordFormatVariableField
    {

        public IDataTypeDefinition TypeDefinition { get; }

        public RecordFormatVariableField(IDataTypeDefinition typeDefinition)
        {
            TypeDefinition = typeDefinition;
        }


    }
}
