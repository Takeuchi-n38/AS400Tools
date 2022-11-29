using System;
using System.Collections.Generic;

namespace Delta.Pdfs
{
    public class PdfPages
    {

        // Maximum length of each line, it related to processing performance.
        private int capacityEachLine;

        public PdfPages(int capacityEachLine)
        {
            if (capacityEachLine < 1)
            {
                throw new Exception("Maximum length of each line is invalid.");
            }
            this.capacityEachLine = capacityEachLine;
        }

        // List of PdfPage
        public List<PdfPage> values = new List<PdfPage>();

        //*
        //* Add new page.

        public PdfPage OpenNewPage()
        {
            var currentPage = new PdfPage(capacityEachLine);
            values.Add(currentPage);
            return currentPage;
        }


    }
}
