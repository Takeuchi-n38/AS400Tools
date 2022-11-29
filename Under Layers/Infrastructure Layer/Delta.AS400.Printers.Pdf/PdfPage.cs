using System;
using System.Collections.Generic;

namespace Delta.Pdfs
{
    //*
    //* Define a PDF page object.

    public class PdfPage
    {

        // Map of line in a PDF page: <row number, PdfLine object>
        public PdfLines pdfLines;

        // Maximum length of each line, it related to processing performance.
        public int capacityEachLine { get; set; }

        //List row
        public List<PdfRow> rows { get; set; }
        //Row Per Page
        public int rowPerPage { get; set; }
        //Data Per Page
        public int dataPerPage { get; set; }
        //Row Data Start
        public int rowDataStart { get; set; }

        //TODO
        //public PdfPage()
        //{
        //    super();
        //}

        //*
        //*  Create a PdfPage.
        //* @param pageNum - page number
        //* @param capacityEachLine - maximum length of each line
        //* @param objId - object id of page
        //* @param contentObjId - content object id of page
        //* @param pdfFont - font information of page

        public PdfPage(int capacityEachLine)
        {
            pdfLines = new PdfLines(capacityEachLine);
        }

        //*
        //* Add a text to page at row, end column.
        //* @param row - row number
        //* @param endCol - end column number
        //* @param text - value

        public void Add(int row, int endCol, string text)
        {
            pdfLines.Add(row, endCol, text);
        }

        //  /**
        //   * Get page number.
        //   * @return
        //   */
        //  int getPageNumber() {
        //    return pageNum;
        //  }

        //  void debug() {
        //    pdfLines.debug(pageNum);
        //  }
    }
}