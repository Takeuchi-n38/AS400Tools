using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatUniqueKeywordLine : RecordFormatKeywordLine
    {
        public RecordFormatUniqueKeywordLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {
        }

    }
}
