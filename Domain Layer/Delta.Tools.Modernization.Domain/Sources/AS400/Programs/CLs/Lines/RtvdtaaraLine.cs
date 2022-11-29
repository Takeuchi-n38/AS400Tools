using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class RtvdtaaraLine : CLLine
    {
        public readonly ObjectID DtaaraObjectID;
        public string DtaaraName=> DtaaraObjectID.Name;

        public readonly int StartingPosition;
        public readonly int SubstringLength;
        public readonly string Rtnvar;
        public RtvdtaaraLine(ObjectID aDtaaraObjectID, int aStartingPosition, int aSubstringLength, string aRtnvar, string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex, originalLineStartIndex)
        {
            this.DtaaraObjectID = aDtaaraObjectID;
            this.StartingPosition = aStartingPosition;
            this.SubstringLength = aSubstringLength;
            this.Rtnvar = aRtnvar;
        }
        //0103 RTVDTAARA  DTAARA(SRCDTAARA (1 10))   RTNVAR(&ZZZ)
    }
}

