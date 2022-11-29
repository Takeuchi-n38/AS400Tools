using System.Collections.Generic;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatHeaderLine : DDSLine
    {
        public RecordFormatHeaderLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {

        }

        public List<IDDSLine> RecordFormatFields = new List<IDDSLine>();//RecordFormatFieldLine or RecordFormatRefFieldLine
        public void AddRecordFormatFieldLine(IDDSLine recordFormatFieldLine)
        {
            RecordFormatFields.Add(recordFormatFieldLine);
        }

        public RecordFormatKeyList RecordFormatKeyList = new RecordFormatKeyList();
        public void AddRecordFormatKeyLine(RecordFormatKeyLine RecordFormatKeyLine)
        {
            RecordFormatKeyList.Add(RecordFormatKeyLine);
        }

        public (List<RecordFormatKeyLine> RecordFormatKeys, List<IDDSLine> RecordFormatFields) KeysAndFields => (RecordFormatKeyList.RecordFormatKeys, RecordFormatFields);



    }
}
