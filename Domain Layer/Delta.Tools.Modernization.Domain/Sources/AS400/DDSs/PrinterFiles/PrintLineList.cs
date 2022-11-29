using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.PrinterFiles
{
    public class PrintLineList : DDSLine
    {
        public List<PrintLine> PrintLines;

        public PrintLineList(IPrinterFileLine line) : base(line.Value, line.StartLineIndex, line.EndLineIndex)
        {
            PrintLines = new List<PrintLine>();
            AddPrintLine(line);
        }

        public void AddPrintLine(IPrinterFileLine line)
        {
            PrintLines.Add(new PrintLine(line));
        }

        public void AddFieldToLastPrintLine(IPrinterFileLine line)
        {
            PrintLines.Last().Add(line);
        }


    }

}
