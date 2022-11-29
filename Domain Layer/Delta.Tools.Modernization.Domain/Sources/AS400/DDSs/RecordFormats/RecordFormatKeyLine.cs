
namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatKeyLine : DDSLine
    {
        public string KeyName => ((IDDSLine)this).Name;

        public RecordFormatKeyLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {
        }

    }
}
