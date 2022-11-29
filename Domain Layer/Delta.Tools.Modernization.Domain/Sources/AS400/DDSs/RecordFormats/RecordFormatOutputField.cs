using Delta.Tools.AS400.DDSs.DisplayFiles;

namespace Delta.Tools.AS400.DDSs.RecordFormats
{
    public class RecordFormatOutputField : DDSLine, IRecordFormatField, IRecordFormatOutputField
    {
        public int Line { get;}

        public int Position { get; }

        string IRecordFormatOutputField.Color { get; set; }

        public string IndicatorValueString => ((IDisplayFileLine)this).Indicator.ValueString;

        public bool HasIndicator => ((IDisplayFileLine)this).HasIndicator;


        protected RecordFormatOutputField(string value, int originalLineStartIndex,int line,int position) : base(value, originalLineStartIndex)
        {
            this.Line=line;
            this.Position=position;
        }

    }
}
