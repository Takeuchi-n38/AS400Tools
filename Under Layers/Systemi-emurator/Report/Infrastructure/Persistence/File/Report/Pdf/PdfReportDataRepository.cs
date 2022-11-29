using Domain.IReport;
using System;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfReportDataRepository : IReportDataRepository
    {

        private string itsReportDataXmlFilePath;

        private string itsReportTemplateXsltFilePath;

        private string itsOutputReportPdfFilePath;

        public PdfReportDataRepository(String aReportDataXmlFilePath, String aReportTemplateXsltFilePath, String aOutputReportPdfFilePath)
        {
            this.itsReportDataXmlFilePath = aReportDataXmlFilePath;
            this.itsReportTemplateXsltFilePath = aReportTemplateXsltFilePath;
            this.itsOutputReportPdfFilePath = aOutputReportPdfFilePath;
        }

        public void Output<T>(dynamic reportData)
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            System.IO.FileStream file = System.IO.File.Create(this.itsReportDataXmlFilePath);

            writer.Serialize(file, reportData);
            file.Close();

            PdfTransformer.Transform(this.itsReportDataXmlFilePath, this.itsReportTemplateXsltFilePath, this.itsOutputReportPdfFilePath);
        }
    }
}