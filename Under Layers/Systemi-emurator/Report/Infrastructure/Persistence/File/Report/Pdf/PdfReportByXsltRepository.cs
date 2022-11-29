using System;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public abstract class PdfReportByXsltRepository : PdfReportRepository
    {
        public new void Output(String xsltPath, String pdfPath)
        {
            PdfTransformer.Transform(CreateDocument(), xsltPath, pdfPath);
        }
    }
}
