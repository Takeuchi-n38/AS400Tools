using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class MvsrcLine : CLLine
    {

        public readonly ObjectID FromFileObjectID;
        public readonly ObjectID ToFileObjectID;

        public MvsrcLine(ObjectID FromFileSource, ObjectID ToFileSource, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            FromFileObjectID = FromFileSource;
            ToFileObjectID = ToFileSource;
        }
    }
}
