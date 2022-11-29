using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class RstobjLine : CLLine
    {

        public readonly ObjectID SavfObjectID;
        public readonly List<ObjectID> Obj;

        public RstobjLine(ObjectID aSavfObjectID, List<ObjectID> aObj ,string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.SavfObjectID = aSavfObjectID;
            this.Obj=aObj;
        }
    }
}
