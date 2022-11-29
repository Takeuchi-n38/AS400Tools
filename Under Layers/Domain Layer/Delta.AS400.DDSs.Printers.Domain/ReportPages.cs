using System.Collections.Generic;

namespace Delta.AS400.DDSs.Printers
{
    public class ReportPages
    {
        public readonly List<ReportPage> Pages;

        public ReportPage currentPage;

        readonly int capacityEachLine;
        public ReportPages(int capacityEachLine)
        {
            this.capacityEachLine = capacityEachLine;
            Pages = new List<ReportPage>();
            this.capacityEachLine = capacityEachLine;
        }

        public void Excpt(OutputRow row)
        {

            if (row.PositionOfLineToBePrintedBeforePrinting() > 0) 
            {
                if(row.PositionOfLineToBePrintedBeforePrinting() < currentPage.Lines.CurrentLineNumber)
                {
                    AddPage(row.PositionOfLineToBePrintedBeforePrinting() + 1);
                }            
            }

            if (currentPage == null)
            {
                AddPage(1);
            }

            currentPage.Excpt(row);

            if (row.PositionOfLineToBePrintedAfterPrinting() > 0)
            {
                if (row.PositionOfLineToBePrintedAfterPrinting() < currentPage.Lines.CurrentLineNumber)
                {
                    AddPage(row.PositionOfLineToBePrintedAfterPrinting() + 1);
                }
            }

        }

        private void AddPage(int positionOfLineAtStart)
        {
            currentPage = new ReportPage(Pages.Count + 1, positionOfLineAtStart, capacityEachLine);
            Pages.Add(currentPage);
        }
    }
}
