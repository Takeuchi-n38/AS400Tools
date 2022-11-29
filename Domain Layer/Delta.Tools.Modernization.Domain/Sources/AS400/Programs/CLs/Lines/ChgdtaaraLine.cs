using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class ChgdtaaraLine : CLLine
    {
        public readonly ObjectID DtaaraObjectID;
        public string DtaaraName => DtaaraObjectID.Name;
        public readonly int StartingPosition;
        public readonly int SubstringLength;
        public readonly string Value;
        public ChgdtaaraLine(ObjectID aDtaaraObjectID, int aStartingPosition, int aSubstringLength, string aValue, string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex, originalLineStartIndex)
        {
            this.DtaaraObjectID = aDtaaraObjectID;
            this.StartingPosition = aStartingPosition;
            this.SubstringLength = aSubstringLength;
            this.Value = aValue;
        }
        //CHGDTAARA  DTAARA(COMNLIB/NIKAIME (1 1)) VALUE('2')
    }
}
