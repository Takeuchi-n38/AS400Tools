using Delta.AS400.DDSs.Printers;
using Delta.Pdfs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.AS400.Printers.Pdf
{
    public class PdfFileWriterForQprint
    {
        public static void Output(string pdfPath, Printer printer)
        {
            Output(pdfPath, printer, Font.DefaultFont.Height);
        }

        public static void Output(string pdfPath, Printer printer, float fontHeight)
        {
            PdfFileWriter.Output(pdfPath, Create(printer), fontHeight);
        }

        static List<Dictionary<int, PdfLine>> Create(Printer printer)
        {

            var pdfLineMapList = new List<Dictionary<int, PdfLine>>();

            foreach (var reportPage in printer.ReportPages.Pages)
            {
                var pdfLines = new Dictionary<int, PdfLine>();
                foreach (var reportLine in reportPage.Lines.Lines)
                {
                    var pdfLine = new PdfLine(printer.CapacityEachLine);
                    foreach (var item in reportLine.Value.FieldSpecifications)
                    {
                        if(item.StartIndexInLine==-1) throw new NotImplementedException();

                        pdfLine.Add(item.EndPositionInLine, item.ValuesToString);
                    }
                    pdfLines.Add(reportLine.Key, pdfLine);
                }
                pdfLineMapList.Add(pdfLines);
            }

            return pdfLineMapList;

        }

    }
}
