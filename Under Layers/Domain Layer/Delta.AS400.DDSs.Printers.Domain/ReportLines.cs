using System.Collections.Generic;

namespace Delta.AS400.DDSs.Printers
{
    public class ReportLines
    {
        public readonly ReportPage page;

        public readonly Dictionary<int, ReportLine> Lines;

        private int currentLineNumber;
        public int CurrentLineNumber => currentLineNumber;
        private bool isOverFlow;
        public bool IsOverFlow()
        {
            return isOverFlow;
        }

        public ReportLine CurrentLine;

        readonly int capacityEachLine;
        public ReportLines(ReportPage page, int positionOfLineAtStart, int capacityEachLine)
        {
            this.page = page;
            Lines = new Dictionary<int, ReportLine>();
            currentLineNumber = positionOfLineAtStart;
            isOverFlow = false;
            this.capacityEachLine = capacityEachLine;
        }

        public void Excpt(OutputRow row)
        {

            currentLineNumber += row.NumberOfLineBreaksBeforePrinting();

            CurrentLine = new ReportLine(page, currentLineNumber, capacityEachLine);
            Lines.Add(currentLineNumber, CurrentLine);

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

    }
}
