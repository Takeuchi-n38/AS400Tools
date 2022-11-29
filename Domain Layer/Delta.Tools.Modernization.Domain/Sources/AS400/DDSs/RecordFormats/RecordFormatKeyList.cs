using System.Collections.Generic;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatKeyList
    {
        public List<RecordFormatKeyLine> RecordFormatKeys = new List<RecordFormatKeyLine>();
        public void Add(RecordFormatKeyLine RecordFormatKeyLine)
        {
            RecordFormatKeys.Add(RecordFormatKeyLine);
        }

    }
}
