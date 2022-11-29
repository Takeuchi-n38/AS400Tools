using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Sources;
using Delta.Tools.Modernization.Sources.AS400.DDSs.RecordFormats;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.DiskFiles.PFs
{
    public class PFStructure : ExternalFormatFileStructure
    {
        public string Summary => (Elements[0] is CommentLine)? ((CommentLine)Elements[0]).Value.Substring(6).Trim():string.Empty;
        public bool IsUnique=>Elements.Any(element=>element is RecordFormatUniqueKeywordLine);
        public bool IsFormat => Elements.Any(element => element is RecordFormatFormatKeywordLine);

        public PFStructure(Source source, List<IStatement> Elements, List<RecordFormatHeaderLine> RecordFormatHeaderLines) : base(source, Elements, RecordFormatHeaderLines)
        {

        }
        public override PFStructure FileDifintion => this;

    }
}
