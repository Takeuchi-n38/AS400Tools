using System;

namespace Delta.Pdfs
{
    public class PdfCol
    {
        public readonly string numberOfEndCol;
        public readonly string value;

        public PdfCol(string aNumberOfEndCol, string aValue)
        {
            numberOfEndCol = aNumberOfEndCol;
            value = aValue;
        }
    }
}
