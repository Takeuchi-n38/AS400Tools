using System.Collections.Generic;

namespace Delta.Pdfs
{
    public class PdfRow
    {

        private readonly string number;
        private readonly List<PdfCol> cols;

        public PdfRow(string rowNumber, List<PdfCol> aCols)
        {
            number = rowNumber;
            cols = aCols;
        }
    }
}