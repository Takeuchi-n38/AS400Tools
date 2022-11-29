using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CrtddmfLine : CLLine
    {

        public readonly ObjectID FromFileObjectID;//file
        public readonly ObjectID ToFileObjectID;//rmt

        public CrtddmfLine(ObjectID FromFileObjectID, ObjectID ToFileObjectID, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FromFileObjectID = FromFileObjectID;
            this.ToFileObjectID = ToFileObjectID;
        }
    }
}
//CRTDDMF    FILE(&CMN/IMMSTRW) RMTFILE(PRODLIB/IMMSTR) RMTLOCNAME('192.168.10.253' * IP)
