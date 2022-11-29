using Delta.Tools.AS400.DDSs.DisplayFiles.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatHeader
    {
        public readonly string PublicModernName;

        public readonly RecordFormatFieldList RecordFormatFields = new RecordFormatFieldList();

        public virtual int LineSpan => RecordFormatFields.LineSpan;

        public List<AttentionCommand> AttentionCommands = new List<AttentionCommand>();
        public List<FunctionCommand> FunctionCommands = new List<FunctionCommand>();

        public RecordFormatHeader(string PublicModernName)
        {
            this.PublicModernName = PublicModernName;
        }

        public void Add(IRecordFormatField recordFormatField)
        {
            RecordFormatFields.Add(recordFormatField);
        }

        public bool IsProtect { get;set;} = false;
        public bool IsOverlay { get; set; } = false;

    }
}
