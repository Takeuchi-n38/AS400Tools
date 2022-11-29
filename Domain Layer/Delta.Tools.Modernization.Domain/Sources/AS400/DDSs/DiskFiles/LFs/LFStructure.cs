using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Sources;
using Delta.Tools.Sources.Statements;
using System.Collections.Generic;

namespace Delta.Tools.AS400.DDSs.DiskFiles.LFs
{
    public class LFStructure : ExternalFormatFileStructure
    {
        public override PFStructure FileDifintion { get; }

        public LFStructure(Source source, List<IStatement> Elements, List<RecordFormatHeaderLine> RecordFormatHeaderLines, PFStructure FileDifintion) : base(source, Elements, RecordFormatHeaderLines)
        {
            this.FileDifintion = FileDifintion;
        }

    }
}
