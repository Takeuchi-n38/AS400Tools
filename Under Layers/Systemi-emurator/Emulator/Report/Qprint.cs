using Delta.Pdfs;
using System.Collections.Generic;
using System.Emulater.Indicator;

namespace System.Emulater.Report
{
    public class Qprint : IReportPagesCreator
    {
        public readonly int capacityEachLine;
        public readonly ReportPages reportPages;
        public readonly OverflowIndicator OF;
        
        private readonly Font itsFont;
        public Font Font => itsFont;

        public new ReportPages ReportPages()
        {
            return reportPages;
        }

        public new int CapacityEachLine()
        {
            return capacityEachLine;
        }

        public Qprint(int capacityEachLine, Font font)
        {
            this.capacityEachLine = capacityEachLine;
            this.itsFont = font;
            this.OF = OverflowIndicator.CreateBy('F');
            reportPages = new ReportPages();
        }

        public Qprint(int capacityEachLine):this(capacityEachLine, Font.DefaultFont)
        {

        }

        public Qprint() : this(123)
        {

        }

        public void Excpt(IOutputRow row)
        {

            reportPages.Excpt(row);

            if (reportPages.currentPage.lines.IsOverFlow())
            {
                OF.SetOn();
            }

        }

        public void Excpt(IOutputRowsContainer outputRowsContainer)
        {
            List<IOutputRow> outputRows = new List<IOutputRow>();
            outputRowsContainer.AddOutputRows(outputRows);
            outputRows.ForEach(row => Excpt(row));
        }

    }

}
