using Delta.AS400.Objects;
using Delta.Tools.AS400.Programs.CLs.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Sources.AS400.Programs.CLs.Lines
{
    public class CpytoimpfLine : CLLine
    {

        public readonly ObjectID FromFileObjectID;
        public readonly ObjectID TargetFileObjectID;
        public readonly bool HaveStmFile;
        readonly string Mbropt;

        public CpytoimpfLine(ObjectID FromFileObjectID, ObjectID TargetFileObjectID, bool HaveStmFile, string aMbropt, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FromFileObjectID = FromFileObjectID;
            this.TargetFileObjectID = TargetFileObjectID;
            this.HaveStmFile = HaveStmFile;
            Mbropt = aMbropt;
        }
        public bool IsReplace => Mbropt == "*REPLACE";

        //0144 CPYTOIMPF  FROMFILE(IIDLIB/ASP16Z) TOFILE(IIDLIB/ASP16ZDTL) MBROPT(*REPLACE)
    }
}
