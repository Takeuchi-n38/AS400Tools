using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class ChkobjLine : CLLine
    {

        public readonly ObjectID FileObjectID;

        public ChkobjLine(ObjectID FileObjectID, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;
        }

    }
}
//CHKOBJ     OBJ(&VAA/VAA530IN) OBJTYPE(*FILE)
//CHKOBJ     OBJ(QSYS/&LIB)       OBJTYPE(*LIB)