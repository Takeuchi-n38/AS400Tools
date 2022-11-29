using Delta.Tools.Sources.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.DDSs.FMTs
{
    public class FormatDataLine : IFormatDataHeaderLine, IFormatDataRecordLine, IFormatDataFieldLine
    {
        public string Value { get; }
        public int StartLineIndex { get; }
        public int EndLineIndex { get; }

        public FormatDataLine(string value, int originalLineStartIndex, int originalLineEndIndex)
        {
            Value = value.PadRight(80);
            StartLineIndex = originalLineStartIndex;
            EndLineIndex = originalLineEndIndex;
        }
        public FormatDataLine(string value, int originalLineStartIndex) : this(value, originalLineStartIndex, originalLineStartIndex)
        {
        }


    }
}
