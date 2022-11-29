using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.FMTs
{
    public class FMTStructure : IStructure
    {
        public Source OriginalSource { get; }
        public FMTStructure(Source source, IEnumerable<IFormatDataLine> Lines)
        {
            OriginalSource = source;
            this.Lines= Lines;
        }

        public IEnumerable<IFormatDataLine> Lines;

        public IFormatDataHeaderLine Header => Lines.Where(l => l.IsHeaderLine).Cast<IFormatDataHeaderLine>().First();
        public IEnumerable<IFormatDataRecordLine> Records => Lines.Where(l => l is IFormatDataRecordLine).Cast<IFormatDataRecordLine>();
        public IEnumerable<IFormatDataFieldLine> Fields => Lines.Where(l => l.IsFieldLine).Cast<IFormatDataFieldLine>();
    }
}
