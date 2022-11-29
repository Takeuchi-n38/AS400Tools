using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Sources;
using Delta.Tools.Modernization.Sources.AS400.DDSs.RecordFormats;
using Delta.Tools.Sources.Statements;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.DiskFiles
{
    public abstract class ExternalFormatFileStructure : DdsStructure
    {

        public List<IStatement> Elements;

        public List<RecordFormatHeaderLine> RecordFormatHeaderLines;

        public abstract PFStructure FileDifintion { get; }

        protected ExternalFormatFileStructure(Source source, List<IStatement> Elements, List<RecordFormatHeaderLine> RecordFormatHeaderLines) : base(source)
        {
            this.Elements = Elements;
            this.RecordFormatHeaderLines = RecordFormatHeaderLines;
        }

        public RecordFormatHeaderLine RecordFormatHeaderLine => RecordFormatHeaderLines.First();

        public IEnumerable<IDDSLine> RecordFormatFields => FileDifintion.RecordFormatHeaderLine.RecordFormatFields;

        public IDDSLine RecordFormatFieldBy(string name) => RecordFormatFields.Where(e => e.Name == name).First();

        public (List<RecordFormatKeyLine> RecordFormatKeys, List<IDDSLine> RecordFormatFields) KeysAndFields => KeysAndFieldsList.FirstOrDefault();
        public List<(List<RecordFormatKeyLine> RecordFormatKeys, List<IDDSLine> RecordFormatFields)> KeysAndFieldsList
    => RecordFormatHeaderLines.Select(l=>l.KeysAndFields).ToList();

        public ObjectID? RefObjectID => FileDifintion.IsFormat ?
            FileDifintion.Elements.Where(element => element is RecordFormatFormatKeywordLine).Cast<RecordFormatFormatKeywordLine>().First().RefObjectID :
            FileDifintion.Elements.Where(e => e is RecordFormatRefKeywordLine).Cast<RecordFormatRefKeywordLine>().FirstOrDefault()?.RefObjectID;

    }
}
