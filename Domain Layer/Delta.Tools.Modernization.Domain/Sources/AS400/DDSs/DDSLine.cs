using Delta.Tools.AS400.DDSs.DiskFiles;
using Delta.Tools.AS400.DDSs.DisplayFiles;
using Delta.Tools.AS400.DDSs.PrinterFiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.DDSs
{
    public class DDSLine : IDisplayFileLine, IDiskFileLine, IPrinterFileLine
    {

        public string Value { get; }
        public int StartLineIndex { get; }
        public int EndLineIndex { get; }

        public DDSLine(string value, int originalLineStartIndex, int originalLineEndIndex)
        {
            Value = value.PadRight(80);
            StartLineIndex = originalLineStartIndex;
            EndLineIndex = originalLineEndIndex;
        }
        public DDSLine(string value, int originalLineStartIndex) : this(value, originalLineStartIndex, originalLineStartIndex)
        {
        }
    }

}
