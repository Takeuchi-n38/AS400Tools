using Delta.Tools.AS400.DDSs.DisplayFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatConstField : RecordFormatOutputField
    {
        public string JoinedValue { get; }

        public RecordFormatConstField(IDisplayFileLine dfileLine, int line, int position, string joinedValue) : base(dfileLine.Value, dfileLine.StartLineIndex, line, position)
        {
            JoinedValue = joinedValue;
        }
    }
}
