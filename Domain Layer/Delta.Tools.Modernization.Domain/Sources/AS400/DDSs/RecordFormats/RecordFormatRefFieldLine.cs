using Delta.AS400.Objects;
using Delta.Tools.Sources.Statements.Singles;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatRefFieldLine : DDSLine, ISingleStatement
    {

        public ObjectID RefObjectID;
        public string Reffld;

        public RecordFormatRefFieldLine(string value, int originalLineStartIndex, ObjectID RefObjectID, string Reffld) : base(value, originalLineStartIndex)
        {
            this.RefObjectID = RefObjectID;
            this.Reffld = Reffld;
        }

    }
}
