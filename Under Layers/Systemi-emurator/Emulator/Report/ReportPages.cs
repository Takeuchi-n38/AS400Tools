using System.Collections.Generic;

namespace System.Emulater.Report
{
    public class ReportPages
    {
        public readonly List<ReportPage> values;

        public ReportPage currentPage;

        public ReportPages()
        {
            values = new List<ReportPage>();
        }

        public void Excpt(IOutputRow row)
        {

            if (row.IsPageBreakBeforePrinting())
            {
                AddPage(row.PositionOfLineToBePrintedBeforePrinting());
            }

            if (currentPage == null)
            {
                AddPage(1);
            }

            currentPage.Excpt(row);

            if (row.IsPageBreakAfterPrinting())
            {
                AddPage(row.PositionOfLineToBePrintedAfterPrinting());
            }

        }

        private void AddPage(int positionOfLineAtStart)
        {
            currentPage = new ReportPage(values.Count + 1, positionOfLineAtStart);
            values.Add(currentPage);
        }
    }
}
