using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.PrinterFiles
{
    public class PrintLine : DDSLine
    {
        public int SkipBefore { get; set; } = -1;

        public int SkipAfter { get; set; } = -1;

        public int SpaceBefore { get; set; } = -1;

        public int SpaceAfter { get; set; } = -1;

        List<IPrinterFileLine> PrintItems;
        public PrintLine(IPrinterFileLine line) : base(line.Value, line.StartLineIndex, line.EndLineIndex)
        {
            PrintItems = new List<IPrinterFileLine>();
            Add(line);
        }

        public void Add(IPrinterFileLine line)
        {
            PrintItems.Add(line);
        }

        public IEnumerable<IDDSLine> IsVariables => PrintItems.Where(f => f.IsVariable);

        public IEnumerable<IDDSLine> Outputs => PrintItems.Where(f => f.IsOutput);

    }
}
