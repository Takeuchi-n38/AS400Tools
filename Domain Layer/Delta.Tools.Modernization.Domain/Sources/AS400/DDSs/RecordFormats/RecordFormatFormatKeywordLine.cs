using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs.RecordFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.Modernization.Sources.AS400.DDSs.RecordFormats
{
    public class RecordFormatFormatKeywordLine : RecordFormatKeywordLine
    {
        public ObjectID RefObjectID;
        public RecordFormatFormatKeywordLine(string value, int originalLineStartIndex, ObjectID RefObjectID) : base(value, originalLineStartIndex)
        {
            this.RefObjectID = RefObjectID;
        }

    }
}