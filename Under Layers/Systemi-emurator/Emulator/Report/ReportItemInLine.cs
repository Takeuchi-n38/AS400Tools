
namespace System.Emulater.Report
{
    public class ReportItemInLine
    {
        public ReportItemInLine(string value, int endPositionInLine)
        {
            this.value = value;
            this.endPositionInLine = endPositionInLine;
        }

        public readonly int endPositionInLine;
        public readonly string value;
    }
}
