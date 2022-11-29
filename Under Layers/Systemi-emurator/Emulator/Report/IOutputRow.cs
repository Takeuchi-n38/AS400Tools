
namespace System.Emulater.Report
{
    public interface IOutputRow
    {
        public void AddReportItem(ReportLine line);

        public virtual bool IsPageBreakAfterPrinting()
        {
            return PositionOfLineToBePrintedAfterPrinting() > 0;
        }

        public virtual bool IsPageBreakBeforePrinting()
        {
            return PositionOfLineToBePrintedBeforePrinting() > 0;
        }

        public virtual int NumberOfLineBreaksAfterPrinting()
        {
            return 0;
        }

        public virtual int NumberOfLineBreaksBeforePrinting()
        {
            return 0;
        }

        public virtual int PositionOfLineToBePrintedAfterPrinting()
        {
            return 0;
        }

        public virtual int PositionOfLineToBePrintedBeforePrinting()
        {
            return 0;
        }
    }
}