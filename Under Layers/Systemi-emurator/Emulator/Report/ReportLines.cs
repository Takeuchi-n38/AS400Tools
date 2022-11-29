using System.Collections.Generic;

namespace System.Emulater.Report
{
    public class ReportLines
    {
        public readonly ReportPage page;

        public readonly Dictionary<int, ReportLine> lines;

        private int currentLineNumber;
        private bool isOverFlow;
        public bool IsOverFlow()
        {
            return isOverFlow;
        }

        public ReportLine CurrentLine;

        public ReportLines(ReportPage page, int positionOfLineAtStart)
        {
            this.page = page;
            lines = new Dictionary<int, ReportLine>();
            currentLineNumber = positionOfLineAtStart;
            isOverFlow = false;
        }

        public void Excpt(IOutputRow row)
        {

            currentLineNumber += row.NumberOfLineBreaksBeforePrinting();

            AddLine(currentLineNumber);

            row.AddReportItem(CurrentLine);

            if (currentLineNumber >= 60)
            {
                isOverFlow = true;//あふれ行が印刷された時OF標識が立つ
            }

            currentLineNumber += row.NumberOfLineBreaksAfterPrinting();

            if (currentLineNumber > 60)
            {
                isOverFlow = true;//印刷行があふれ行を超えた時OF標識が立つ
            }

        }

        private void AddLine(int currentLineNumber)
        {
            CurrentLine = new ReportLine(page, currentLineNumber);
            lines.Add(currentLineNumber, CurrentLine);
        }
    }
}
