
namespace System.Emulater.Report
{
    public class ReportPage
    {
        public readonly int number;
        public readonly ReportLines lines;

        public ReportPage(int pageNumber, int positionOfLineAtStart)
        {
            this.number = pageNumber;
            this.lines = new ReportLines(this, positionOfLineAtStart);
        }

        public void Excpt(IOutputRow row)
        {
            lines.Excpt(row);
        }
    }
}
