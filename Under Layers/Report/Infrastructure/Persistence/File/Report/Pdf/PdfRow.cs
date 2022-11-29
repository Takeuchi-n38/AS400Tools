using System.Collections.Generic;

namespace Infrastructure.Persistence.File.Report.Pdf
{
    public class PdfRow
    {

        private readonly string number;
        private readonly List<PdfCol> cols;

        public PdfRow(string rowNumber, List<PdfCol> aCols)
        {
            this.number = rowNumber;
            this.cols = aCols;
        }
    }
}