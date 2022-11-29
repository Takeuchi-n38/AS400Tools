using Newtonsoft.Json;
using System;
using System.Dynamic;
using System.Xml;
using System.Xml.Linq;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    /**
     * This class transforms XML, XSLT to PDF file.
     */
    public class PdfTransformer
    {

        public static void Transform(XmlDocument document, string xsltPath, string pdfPath)
        {
            // Create DOMResult from Document object
            //DOMResult domResult = createDOMResult(document, xsltPath);
            // Step 1: create PdfDocument object by DOMResult object
            PdfDocument pdfDocument = CreatePdfDocument(document);
            // Step 2: create PDF file by the PdfDocument object
            pdfDocument.CreatePdfFile(pdfPath);
        }

        /**
         * Create PDF file by XML, XSLT.
         * @param xmlPath - full path of XML file (input)
         * @param xsltPath - full path of XSLT file (input)
         * @param pdfPath - full path of PDF file (output)
         */
        public static void Transform(String xmlPath, String xsltPath, String pdfPath)
        {
            // Create Document object from XML file
            XmlDocument document = new XmlDocument();
            // Create DOMResult from Document object
            document.Load(xsltPath);
            XDocument doc = XDocument.Parse(document.ToString()); //or XDocument.Load(path)
            string jsonText = JsonConvert.SerializeXNode(doc);
            dynamic dyn = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
            // Step 1: create PdfDocument object by DOMResult object
            PdfDocument pdfDocument = CreatePdfDocument(dyn);
            pdfDocument.CreatePdfFile(pdfPath);
            // Step 2: create PDF file by the PdfDocument object
        }

        /**
         * Create new PdfDocument object from DOMResult.
         * @param domResult
         * @return
         */
        private static PdfDocument CreatePdfDocument(XmlDocument domResult)
        {
            //Node root = domResult.getNode().getFirstChild();
            var root = domResult.ChildNodes[0].FirstChild;
            //final int ROW_PER_PAGE = Convert.ToInt32(root.getNamedItem("row-per-page").getTextContent());
            int ROW_PER_PAGE = Convert.ToInt32(root.Attributes.GetNamedItem("row-per-page").Value);
            string fontHeight = root.Attributes.GetNamedItem("font-height").Value;
            int capacityEachLine = Convert.ToInt32(root.Attributes.GetNamedItem("capacity-each-line").Value);
            XmlNode row = root.FirstChild;
            PdfDocument pdfDocument = new PdfDocument(capacityEachLine, fontHeight);

            while (row != null)
            {
                // Open new page
                var newPage = pdfDocument.OpenNewPage();

                // Process row by row
                for (int i = 0; i < ROW_PER_PAGE && row != null; i++, row = row.NextSibling)
                {
                    if ((row.Attributes != null) && (row.HasChildNodes))
                    {
                        int rowNum = Convert.ToInt16(row.Attributes.GetNamedItem("row-num").Value);
                        int endColNum;

                        // Process column by column in the row
                        for (XmlNode col = row.FirstChild; col != null; col = col.NextSibling)
                        {
                            endColNum = Convert.ToInt16(col.Attributes.GetNamedItem("end-col-num").Value);
                            newPage.Add(rowNum, endColNum, col.Value);
                        }
                    }
                }
            }

            return pdfDocument;
        }
    }
}