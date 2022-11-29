using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfFileWriter
    {
        /* Common in a PDF file */
        private static readonly String VERSION_PDF = "%PDF-1.4";

        private static readonly String SIGNATURE_CONTENT = "%\u00E2\u00E3\u00CF\u00D3";

        private static readonly PdfObject PROC_SET = new PdfObject(9, "[/PDF /Text /ImageC]");

        private static readonly PdfObject CATALOG = new PdfObject(10, "<<\n/Pages 11 0 R\n/PageMode /UseNone\n/Type /Catalog\n>>");

        private static readonly String FORMAT_PARENT_PAGE_OBJ = "<<\n/Kids [{0}]\n/Count {1}\n/Type /Pages\n>>";


        private static readonly String INFO = "<<\n/Creator (QPGMR/PDFOUTQ)\n/Title (QPRINT)\n/Producer (658DFA2/5\\(DEVEROP\\)/C#P0510/iS Technoport LTD.,)\n/Author (HONSHA/QPGMR)\n>>";

        private static readonly String FORMAT_XREF_TABLE = "xref\n0 {0}\n0000000000 65535 f \n{1}";

        private static readonly String FORMAT_XREF_ENTRY = "{0} 00000 n \n";

        private static readonly String FORMAT_TRAILER = "trailer\n<<\n/Root 10 0 R\n/Info {0} 0 R\n/Size {1}\n>>\nstartxref\n{2}\n%%EOF\n";

        private static readonly String FORMAT_PAGE_DATA = "<<\n/CropBox [0 0 841 595]\n/Rotate 0\n/Parent 11 0 R\n/Resources \n<<\n/Font \n<<\n/F1 1 0 R\n/F2 4 0 R\n/F3 7 0 R\n>>\n/ProcSet 9 0 R\n>>\n/MediaBox [0 0 841 595]\n/Contents {0} 0 R\n/Type /Page\n>>";

        private static readonly String FORMAT_CONTENT_PAGE_DATA = "<<\n/Length {0}\n>>\nstream\n{1}\nendstream ";

        private static readonly String FORMAT_STREAM = "BT\n/F1 {0} Tf\n100 Tz\n0 0 0 rg\n0 0 0 RG\n0 Tr\n0 w\n{1}ET\n";
        //Format object as string value to write into PDF file
        private static readonly String FORMAT_LINE = "1.00000 .00 .00 1.00 {0} {1} Tm\n{2}";

        /* End Common */
        public static void Output(String pdfPath, PdfPages pdfPages, float fontHeight)
        {
            var pdfLineCollectionList = pdfPages.values.Select(pdfPage => pdfPage.pdfLines.pdfLines).ToList();
            Output(pdfPath, pdfLineCollectionList, fontHeight);
        }

        public static void Output(String pdfPath, List<Dictionary<Int32, PdfLine>> pdfLineCollectionList, float fontHeight)
        {
            Output(pdfPath, Contents(pdfLineCollectionList, fontHeight));
        }

        private static string Contents(List<Dictionary<Int32, PdfLine>> pdfPages, float fontHeight)
        {
            StringBuilder rawData = new StringBuilder();
            // Add header
            rawData.Append(VERSION_PDF + "\n");

            // Indicates page's content as binary
            rawData.Append(SIGNATURE_CONTENT + "\n");

            List<PdfObject> pdfObjs = GetPdfObjects(pdfPages, PdfFont.GetFont(fontHeight));
            int[] posObjs = new int[pdfObjs.Count];
            foreach (var pdfObj in pdfObjs)
            {
                // Determine start offset of object
                posObjs[pdfObj.GetObjId() - 1] = rawData.Length;
                // Add writable data of object
                rawData.Append(pdfObj.ToWritableData());
            }

            // Start offset of cross-reference table
            int startXref = rawData.Length;

            StringBuilder xrefEntries = new StringBuilder();
            // Create cross-reference entries from the start offset of all objects
            foreach (int posObj in posObjs)
            {
                xrefEntries.Append(String.Format(FORMAT_XREF_ENTRY, (new string('0', 10 - posObj.ToString().Length) + posObj.ToString())));
            }
            string xrefTable = String.Format(FORMAT_XREF_TABLE, pdfObjs.Count + 1, xrefEntries.ToString());

            // Add cross-reference table
            rawData.Append(xrefTable);

            // Add trailer
            rawData.Append(String.Format(FORMAT_TRAILER, (11 + pdfPages.Count * 2 + 2), pdfObjs.Count + 1, startXref));

            return rawData.ToString();
        }

        private static List<PdfObject> GetPdfObjects(List<Dictionary<Int32, PdfLine>> pdfPages, PdfFont pdfFont)
        {
            var pdfObjs = new List<PdfObject>();

            // Add font configuration
            pdfObjs.AddRange(pdfFont.GetFontConfig());//1-8

            // Add procedure set
            pdfObjs.Add(PROC_SET);//at 9

            // Add catalog as root object
            pdfObjs.Add(CATALOG);   //at 10 

            // Kid object reference
            var kidRefs = new List<String>();

            var idObj = 12;// Object ID of page starts at 12

            // Add all page objects and content page objects
            foreach (var pdfLines in pdfPages)
            {
                var pageId = idObj++;
                var pageContentId = idObj++;
                pdfObjs.Add(new PdfObject(pageId, String.Format(FORMAT_PAGE_DATA, pageContentId)));
                pdfObjs.Add(GetContentPageObject(pdfLines, pageContentId, pdfFont));
                kidRefs.Add(pageId + " 0 R");
            }

            // Add parent page
            //TODO
            pdfObjs.Add(new PdfObject(11, String.Format(FORMAT_PARENT_PAGE_OBJ, String.Join(" ", kidRefs), kidRefs.Count())));

            // Add info
            pdfObjs.Add(new PdfObject(idObj, INFO));

            return pdfObjs;
        }

        private static PdfObject GetContentPageObject(Dictionary<Int32, PdfLine> pdfLines, int contentObjId, PdfFont pdfFont)
        {
            StringBuilder dataStream = new StringBuilder("");

            // Create data stream from data of each line
            foreach (var item in pdfLines)
            {
                dataStream.Append(GetWritableData(item.Key, item.Value, pdfFont));
            }

            // Create stream
            string streamPackage = String.Format(FORMAT_STREAM, pdfFont.GetFontHeight(), dataStream.ToString());

            // Create data of content object
            string dataObject = String.Format(FORMAT_CONTENT_PAGE_DATA, streamPackage.Length, streamPackage);

            return new PdfObject(contentObjId, dataObject);
        }

        private static String GetWritableData(int row, PdfLine pdfLine, PdfFont pdfFont)
        {
            if (!pdfFont.ValidateRow(row))
            {
                throw new Exception("The index row is out of range. The valid range is from 1 to " + pdfFont.CountRow() + ".");
            }
            int y = pdfFont.GetY(row);
            //TODO
            string yCoordinate = String.Format(Common.FORMAT_COORDINATE, y / 1000, new string('0', 3 - (y % 1000).ToString().Length) + (y % 1000).ToString());
            StringBuilder sb = new StringBuilder("");

            // Split this PdfLine to PdfTexts
            List<PdfText> pdfTexts = SplitToPdfText(pdfLine.length, pdfLine.pdfChars);

            foreach (PdfText pdfText in pdfTexts)
            {
                int x = pdfFont.GetX(pdfText.GetBeginPosInLine());
                string xCoordinate = String.Format(Common.FORMAT_COORDINATE, x / 1000, new string('0', 3 - (x % 1000).ToString().Length) + (x % 1000).ToString());
                // Create data to write into PDF file
                sb.Append(String.Format(FORMAT_LINE, xCoordinate, yCoordinate, pdfText.GetWritableData()));
            }
            return sb.ToString();
        }


        /**
         * Split this PdfLine to PdfTexts.
         * @return List of PdfText split.
         */
        private static List<PdfText> SplitToPdfText(int length, PdfChar[] pdfChars)
        {
            List<PdfText> pdfTexts = new List<PdfText>();
            int loopTimes = length + 1;
            PdfText pdfText = new PdfText();

            for (int i = 1; i < loopTimes; i++)
            {
                // Try join a PdfChar to PdfText and check joining status
                PdfText.JoiningStatus joiningStatus = pdfText.Join(i, pdfChars[i]);
                if (joiningStatus == PdfText.JoiningStatus.OUT_OF_RANGE || joiningStatus == PdfText.JoiningStatus.INCOMPATIBLE_TYPE)
                {
                    // when joinStatus == JoinStatus.OUT_OF_RANGE, OR
                    // joinStatus == JoinStatus.INCOMPATIBLE_TYPE
                    // Add to list and create new PdfText
                    pdfTexts.Add(pdfText);
                    pdfText = new PdfText();
                    i--;
                }
            }

            if (!pdfText.IsEmpty())
            {
                // Add last PDF Text
                pdfTexts.Add(pdfText);
            }

            return pdfTexts;
        }

        private static void Output(string pdfPath, string contents)
        {
            // Write file process
            try
            {
                //var streamWriter = new StreamWriter(new FileStream(pdfPath + "\\test.pdf", FileMode.Create), Encoding.GetEncoding("Windows - 1252"));
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                var streamWriter = new StreamWriter(new FileStream(pdfPath, FileMode.Create), Encoding.GetEncoding(1252));
                streamWriter.Write(contents);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
