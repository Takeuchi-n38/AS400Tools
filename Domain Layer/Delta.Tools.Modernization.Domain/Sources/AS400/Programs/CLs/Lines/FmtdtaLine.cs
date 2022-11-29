using System.Collections.Generic;
using Delta.Tools.AS400.Structures;
using Delta.AS400.Objects;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class FmtdtaLine : CLLine
    {

        public readonly List<ObjectID> InFileObjectIDs;
        public readonly ObjectID OutFileObjectID;
        public readonly ObjectID SrcObjectID;

        public FmtdtaLine(List<ObjectID> InFileObjectIDs, ObjectID OutFileObjectID, ObjectID SrcObjectID,string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.InFileObjectIDs = InFileObjectIDs;
            this.OutFileObjectID = OutFileObjectID;
            this.SrcObjectID= SrcObjectID;
        }

    }
}
