using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class OvrprtfLine : CLLine
    {

        public readonly ObjectID FileObjectID;
        public readonly string OUTQ;
        public readonly string SPLFNAME;

        public OvrprtfLine(ObjectID FileObjectID, string OUTQ,string SPLFNAME, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID = FileObjectID;
            this.OUTQ = OUTQ;
            this.SPLFNAME = SPLFNAME;
        }

    }
}
//0291 OVRPRTF    FILE(QPRINT) DEV(PRTAFP) DEVTYPE(*AFPDS) PAGESIZE(8.25 11.7 *UOM) LPI(9) FRONTMGN(0.96 0.9) CDEFNT(XZK308) PAGRTT(90) DUPLEX(*YES) OUTQ(CLASSZ) HOLD(*YES) SAVE(*YES) USRDTA(&X1PGID) IGCCDEFNT(XZK808)
//0386 OVRPRTF    FILE(QPRINT) DUPLEX(*YES) OUTQ(EIGYPDFQ) FORMTYPE(ANAN) SAVE(*YES) USRDTA(H1DEIGY) SPLFNAME(JJJB15M001)