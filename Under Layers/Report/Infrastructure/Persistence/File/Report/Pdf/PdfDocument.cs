using System;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfDocument
    {
        // List of PdfPage
        private PdfPages pdfPages;
        private readonly float fontHeight;
        public PdfDocument(int capacityEachLine, string fontHeight)
        {
            this.pdfPages = new PdfPages(capacityEachLine);
            this.fontHeight = float.Parse(fontHeight);
        }

        //*
        //* Add new page.

        public PdfPage OpenNewPage()
        {
            return pdfPages.OpenNewPage();
        }

        public void CreatePdfFile(String pdfPath)
        {
            PdfFileWriter.Output(pdfPath, pdfPages, fontHeight);
        }

    }
}
