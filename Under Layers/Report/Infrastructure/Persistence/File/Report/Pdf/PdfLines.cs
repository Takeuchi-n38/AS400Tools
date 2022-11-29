using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfLines
    {
        // Maximum length of each line, it related to processing performance.
        private readonly int capacityEachLine;

        // Map of line in a PDF page: <row number, PdfLine object>
        public Dictionary<int, PdfLine> pdfLines = new Dictionary<int, PdfLine>();
        public PdfLines(int capacityEachLine)
        {
            this.capacityEachLine = capacityEachLine;
        }

        /**
         * Add a text to page at row, end column.
         * 
         * @param row - row number
         * @param endCol - end column number
         * @param text - value
         */

        public void AddOnRightJustified(int row, int endCol, String text)
        {
            PdfLine pdfLine = pdfLines.ContainsKey(row) ? pdfLines[row] : new PdfLine(capacityEachLine);
            pdfLine.AddOnRightJustified(endCol, text);
            pdfLines.Add(row, pdfLine);
        }
    }
}
