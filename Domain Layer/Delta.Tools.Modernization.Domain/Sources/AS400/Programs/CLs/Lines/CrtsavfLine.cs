using Delta.AS400.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{

    public class CrtsavfLine : CLLine
    {

        public readonly ObjectID FileObjectID;


        public CrtsavfLine(ObjectID FileObjectID, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.FileObjectID= FileObjectID;
        }

    }
}
//CRTSAVF    FILE(&SVF/JJJC10M0S1)