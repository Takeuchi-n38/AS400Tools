using Delta.AS400.DDSs.Printers;
using Delta.AS400.Printers.Pdf;
using System;
using System.IO;

namespace Delta.Tools.Modernization.Test.PdfTest
{
    public class PdfFileWriterForQprintTest
    {
        //ActualFolder(caseName)
        public static void WritePdfToActalFolder(Printer printer,string ActualFolderName,  string aFileName)
        {
            var pdfPath = Path.Combine(ActualFolderName, $"{aFileName}.pdf");
            PdfFileWriterForQprint.Output(pdfPath, printer);

        }

    }
}
