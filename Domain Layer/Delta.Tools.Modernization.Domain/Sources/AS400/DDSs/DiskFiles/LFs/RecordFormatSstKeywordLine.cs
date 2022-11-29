using Delta.AS400.Objects;
using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.DDSs.DiskFiles.LFs
{
    public class RecordFormatSstKeywordLine : DDSLine, ISingleStatement
    {

        public string TargetFieldName;
        public int StartPosision;
        public int EndPosision;
        public int Length=>EndPosision-StartPosision+1;
        //SST(HINBAN 1 12)

        public RecordFormatSstKeywordLine(string value, int originalLineStartIndex, 
            string aTargetFieldName, int aStartPosision,int aEndPosision) : base(value, originalLineStartIndex)
        {
            this.TargetFieldName = aTargetFieldName;
            this.StartPosision = aStartPosision;
            this.EndPosision = aEndPosision;
        }

    }
}
