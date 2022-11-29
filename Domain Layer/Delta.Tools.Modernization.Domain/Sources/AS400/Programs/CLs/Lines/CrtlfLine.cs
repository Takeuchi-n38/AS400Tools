using Delta.AS400.Objects;
using Delta.Tools.AS400.Structures;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CrtlfLine : CLLine
    {
        //public List<OperatedFile> OperatedFiles => new List<OperatedFile>() { OperatedFile.Of(FromFileSource, FileOperations.CreateAsView), OperatedFile.Of(ToFileSource, FileOperations.ReferAsDefinition) };

        public readonly ObjectID FromFileObjectID;//src
        public readonly ObjectID ToFileObjectID;//file

        public CrtlfLine(ObjectID FromFileSource, ObjectID ToFileSource, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            FromFileObjectID = FromFileSource;
            ToFileObjectID = ToFileSource;
        }

    }
}
//CRTLF FILE(&VAA / KTANDM01) SRCFILE(TANLIB / QDDSSRC) OPTION(*NOLIST * NOSRC) MAXMBRS(*NOMAX) LVLCHK(*NO)
