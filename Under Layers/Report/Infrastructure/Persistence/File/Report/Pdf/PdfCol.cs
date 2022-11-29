using System;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfCol
    {
        public readonly String numberOfEndCol;
        public readonly String value;

        public PdfCol(String aNumberOfEndCol, String aValue)
        {
            this.numberOfEndCol = aNumberOfEndCol;
            this.value = aValue;
        }
    }
}
