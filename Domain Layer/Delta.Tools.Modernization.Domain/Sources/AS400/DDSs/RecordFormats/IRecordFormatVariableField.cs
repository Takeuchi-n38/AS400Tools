using Delta.AS400.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public interface IRecordFormatVariableField
    {
        IDataTypeDefinition TypeDefinition { get; }
    }
}
