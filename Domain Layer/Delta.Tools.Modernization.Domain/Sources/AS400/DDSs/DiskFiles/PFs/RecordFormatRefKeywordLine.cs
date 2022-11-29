using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.DDSs.DiskFiles.PFs
{
    public class RecordFormatRefKeywordLine : RecordFormatKeywordLine
    {
        public ObjectID RefObjectID;
        public RecordFormatRefKeywordLine(string value, int originalLineStartIndex, ObjectID RefObjectID) : base(value, originalLineStartIndex)
        {
            this.RefObjectID= RefObjectID;
        }

    }
}
