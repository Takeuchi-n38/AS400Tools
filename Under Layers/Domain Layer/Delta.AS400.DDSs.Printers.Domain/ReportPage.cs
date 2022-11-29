namespace Delta.AS400.DDSs.Printers
{
    public class ReportPage
    {
        public readonly int Number;
        public readonly ReportLines Lines;

        public ReportPage(int pageNumber, int positionOfLineAtStart, int capacityEachLine)
        {
            Number = pageNumber;
            Lines = new ReportLines(this, positionOfLineAtStart, capacityEachLine);
        }

        public void Excpt(OutputRow row)
        {
            Lines.Excpt(row);
        }
    }
}
