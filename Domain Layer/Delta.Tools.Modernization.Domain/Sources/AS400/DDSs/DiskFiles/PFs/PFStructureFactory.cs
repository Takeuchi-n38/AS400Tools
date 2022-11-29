using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.DiskFiles;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using Delta.Tools.Modernization.Sources.AS400.DDSs.RecordFormats;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Singles.Comments;
using Delta.Utilities.Extensions.SystemString;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.DiskFiles.PFs
{
    public class PFStructureFactory : IStructureFactory
    {

        PFStructureFactory()
        {
        }
        public static PFStructureFactory Of()
        {
            return new PFStructureFactory();
        }

        IStructure IStructureFactory.Create(Source source)
        {
            var OriginalSource = source;
            var ObjectID = source.ObjectID;
            var Elements = new List<IStatement>();
            var RecordFormatHeaderLines = new List<RecordFormatHeaderLine>();

            //var PrintLineLists = new List<PrintLineList>();

            for (var originalLinesIndex = 0; originalLinesIndex < OriginalSource.OriginalLines.Length; originalLinesIndex++)
            {
                IDDSLine ddsLine = new DDSLine(OriginalSource.OriginalLines[originalLinesIndex], originalLinesIndex);

                if (ddsLine.IsCommentLine)
                {
                    Elements.Add(CommentLine.Of(OriginalSource.OriginalLines[originalLinesIndex], originalLinesIndex));
                    continue;
                }

                if (ddsLine.CanJoin)
                {
                    originalLinesIndex++;
                    var value = ddsLine.Value + OriginalSource.OriginalLines[originalLinesIndex];
                    var startIndex = ddsLine.StartLineIndex;
                    var endIndex = originalLinesIndex;
                    ddsLine = new DDSLine(value, startIndex, endIndex);
                }

                var fileLine = (IDiskFileLine)ddsLine;

                if (fileLine.IsRecordFormatHeaderLine)
                {
                    RecordFormatHeaderLines.Add(new RecordFormatHeaderLine(fileLine.Value, fileLine.StartLineIndex));
                    continue;
                }

                if (fileLine.HaveName)
                {

                    if (fileLine.IsREFFLDLine)
                    {
                        var Reffld = TextClipper.ClipParameter(fileLine.Keywords, "REFFLD");
                        var RecordFormatRefKeywordLine = Elements.Where(e => e is RecordFormatRefKeywordLine).Cast<RecordFormatRefKeywordLine>().First();
                        var RefObjectID = RecordFormatRefKeywordLine.RefObjectID;
                        RecordFormatHeaderLines.Last().AddRecordFormatFieldLine(new RecordFormatRefFieldLine(fileLine.Value, fileLine.StartLineIndex, RefObjectID, Reffld));
                        continue;
                    }

                    if (fileLine.IsKeyLine)
                    {
                        //TODO:duplicated name
                        RecordFormatHeaderLines.First().AddRecordFormatKeyLine(new RecordFormatKeyLine(fileLine.Value, fileLine.StartLineIndex));
                        continue;
                    }

                    if (fileLine.TypeOfNameOrSpecification.Trim() == string.Empty)
                    {
                        RecordFormatHeaderLines.Last().AddRecordFormatFieldLine(new RecordFormatFieldLine(fileLine.Value, fileLine.StartLineIndex));
                        continue;
                    }

                }

                if (fileLine.Keywords != string.Empty)
                {
                    var keywords = fileLine.Keywords;

                    if (keywords.StartsWith("REF("))
                    {
                        var referName = TextClipper.ClipParameter(keywords, "REF");
                        var refObjectID = ObjectID.CreateWithSameLibrary(referName);

                        Elements.Add(new RecordFormatRefKeywordLine(fileLine.Value, fileLine.StartLineIndex, refObjectID));
                        continue;
                    }
                    if (keywords.StartsWith("UNIQUE"))
                    {
                        Elements.Add(new RecordFormatUniqueKeywordLine(fileLine.Value, fileLine.StartLineIndex));
                        continue;
                    }
                    if (keywords.StartsWith("FORMAT("))
                    {
                        //FORMAT(NOUNYU)
                        var referName = TextClipper.ClipParameter(keywords, "FORMAT");
                        var refObjectID = ObjectID.CreateWithSameLibrary(referName);
                        Elements.Add(new RecordFormatFormatKeywordLine(fileLine.Value, fileLine.StartLineIndex, refObjectID));
                        continue;
                    }
                    Elements.Add(new RecordFormatKeywordLine(fileLine.Value, fileLine.StartLineIndex));
                    continue;

                }

                Elements.Add(new UnKnownDdsLine(fileLine.Value, fileLine.StartLineIndex));
            }

            return new PFStructure(source, Elements, RecordFormatHeaderLines);
        }

    }
}