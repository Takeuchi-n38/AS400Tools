using Delta.Pdfs;
using System.Collections.Generic;

namespace System.Emulater.Report
{
    public interface IReportPagesCreator
    {
        public int CapacityEachLine();

        public Font Font { get;}

        public virtual void Output(string filePath)
        {

            var reportPages = ReportPages().values;

            List<Dictionary<int, PdfLine>> pdfLineMapList = new List<Dictionary<int, PdfLine>>();

            foreach (ReportPage reportPage in reportPages)
            {
                Dictionary<int, PdfLine> pdfLines = new Dictionary<int, PdfLine>();
                foreach (var reportLine in reportPage.lines.lines)
                {
                    PdfLine pdfLine = new PdfLine(CapacityEachLine());
                    foreach (ReportItemInLine item in reportLine.Value.items)
                    {
                        pdfLine.Add(item.endPositionInLine, item.value);
                    }
                    pdfLines.Add(reportLine.Key, pdfLine);
                }
                pdfLineMapList.Add(pdfLines);
            }

            PdfFileWriter.Output(filePath, pdfLineMapList, Font.Height);

        }

        public ReportPages ReportPages();
    }
}
