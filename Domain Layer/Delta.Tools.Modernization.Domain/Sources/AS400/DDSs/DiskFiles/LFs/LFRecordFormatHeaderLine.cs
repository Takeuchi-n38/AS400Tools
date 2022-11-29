using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.DDSs.DiskFiles.LFs
{
    public class LFRecordFormatHeaderLine : RecordFormatHeaderLine, ISingleStatement
    {

        public LFRecordFormatHeaderLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {

        }

    }
}
