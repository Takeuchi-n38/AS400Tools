using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class ClrpfLine : CLLine
    {

        public readonly ObjectID FileObjectID;

        public ClrpfLine(ObjectID FileObjectID, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;
        }

    }
}
