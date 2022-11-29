
namespace System.Emulater.Report
{
    public class ReportText
    {
        public readonly int PageNumber;
        public readonly int LineNumner;
        public readonly ReportItemInLine ItemInLine;

        public ReportText(int pageNumber, int lineNumner, ReportItemInLine itemInLine)
        {
            this.PageNumber = pageNumber;
            this.LineNumner = lineNumner;
            this.ItemInLine = itemInLine;
        }
    }
}
