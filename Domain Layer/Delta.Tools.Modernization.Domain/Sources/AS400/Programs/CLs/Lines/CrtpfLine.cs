using Delta.AS400.Objects;
using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CrtpfLine : CLLine
    {

        public readonly ObjectID FileObjectID;

        public readonly string File;
        public readonly string Rcdlen;
        public readonly string Srcfile;
        public readonly string Srcmbr;

        public CrtpfLine(ObjectID FileObjectID, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;
            this.File= TextClipper.ClipParameter(joinedLine, "FILE");
            this.Rcdlen= TextClipper.ClipParameter(joinedLine, "RCDLEN");
            this.Srcfile = TextClipper.ClipParameter(joinedLine, "SRCFILE");
            this.Srcmbr = TextClipper.ClipParameter(joinedLine, "SRCMBR");
        }

    }

}
/*
CRTPF      FILE(URIIND) RCDLEN(140) IGCDTA(*YES) +
                          OPTION(*NOSRC *NOLIST) SIZE(*NOMAX) +
                          LVLCHK(*NO)

CRTPF      FILE(&LFB/TRMPICCHK) SRCFILE(&LFB/QDDSSRC) +
                          SRCMBR(TRMPIC) OPTION(*NOSRC *NOLIST) +
                          SIZE(*NOMAX) LVLCHK(*NO)
 */
