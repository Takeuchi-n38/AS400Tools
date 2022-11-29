using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CpyfLine : CLLine
    {

        public readonly ObjectID FromFileObjectID;
        public readonly ObjectID ToFileObjectID;
        readonly string Mbropt;//*NONE,*ADD,*UPDADD,*REPLACE
        public CpyfLine(ObjectID FromFileSource, ObjectID ToFileSource, string aMbropt, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            FromFileObjectID = FromFileSource;
            ToFileObjectID = ToFileSource;
            Mbropt=aMbropt;
        }
        public bool IsAdd => Mbropt=="*ADD";
        public bool IsUpdadd => Mbropt == "*UPDADD";
        public bool IsReplace => Mbropt == "*REPLACE";
    }
}
//CPYF FROMFILE(SALELIB/URIBFL) +
//                          TOFILE(SALELIB/URIBFL.W) MBROPT(*REPLACE) +
//                          FMTOPT(*NOCHK)