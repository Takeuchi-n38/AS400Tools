using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs;
using Delta.Tools.AS400.DDSs.DiskFiles;
using Delta.Tools.AS400.DDSs.DiskFiles.PFs;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Singles.Comments;
using Delta.Utilities.Extensions.SystemString;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.DiskFiles.LFs
{
    public class LFStructureFactory : IStructureFactory
    {
        Func<ObjectID, IStructure> DiscFileCreate;

        LFStructureFactory(Func<ObjectID, PFStructure> DiscFileCreate)
        {
            this.DiscFileCreate = DiscFileCreate;
        }
        public static LFStructureFactory Of(Func<ObjectID, PFStructure> DiscFileCreate)
        {
            return new LFStructureFactory(DiscFileCreate);
        }

        IStructure IStructureFactory.Create(Source source)
        {
            var OriginalSource = source;
            var ObjectID = source.ObjectID;
            var Elements = new List<IStatement>();
            var RecordFormatHeaderLines = new List<RecordFormatHeaderLine>();
            PFStructure FileDifintion = null;

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
                    RecordFormatHeaderLines.Add(new LFRecordFormatHeaderLine(fileLine.Value, fileLine.StartLineIndex));

                    if (fileLine.Keywords.StartsWith("PFILE("))
                    {
                        FileDifintion = loadFileDifintion(ObjectID, fileLine.Keywords);
                    }

                    continue;
                }

                if (fileLine.HaveName)
                {
                    if (fileLine.TypeOfNameOrSpecification == "K")
                    {
                        //TODO:duplicated name
                        RecordFormatHeaderLines.First().AddRecordFormatKeyLine(new RecordFormatKeyLine(fileLine.Value, fileLine.StartLineIndex));
                        continue;
                    }
                }

                if (fileLine.Keywords != string.Empty)
                {
                    var keywords = fileLine.Keywords;

                    if (keywords.StartsWith("PFILE("))//PFILE(URIHFL)
                    {
                        FileDifintion = loadFileDifintion(ObjectID, keywords);
                        Elements.Add(new RecordFormatPfileKeywordLine(fileLine.Value, fileLine.StartLineIndex, FileDifintion));
                        continue;
                    }

                    if (keywords.StartsWith("SST("))
                    {
                        //SST(HINBAN 1 12)
                        var splittedSstParam = TextClipper.ClipParameter(keywords, "SST(").Split(' ');
                        var startPosition= int.Parse(splittedSstParam[1]);
                        var endPosition= splittedSstParam.Length >=3 ? int.Parse(splittedSstParam[2]): startPosition;
                        var sstLine = new RecordFormatSstKeywordLine(fileLine.Value, fileLine.StartLineIndex,
                            splittedSstParam[0],startPosition, endPosition);
                        RecordFormatHeaderLines.First().AddRecordFormatFieldLine(sstLine);
                        continue;
                    }

                    Elements.Add(new RecordFormatKeywordLine(fileLine.Value, fileLine.StartLineIndex));
                    continue;
                }

                Elements.Add(new UnKnownDdsLine(fileLine.Value, fileLine.StartLineIndex));
            }

            return new LFStructure(source, Elements, RecordFormatHeaderLines, FileDifintion);

        }

        PFStructure loadFileDifintion(ObjectID ObjectID, string keywords)
        {
            var referName = TextClipper.ClipParameter(keywords, "PFILE");
            var objectID = ObjectID.CreateWithSameLibrary(referName);
            var referDisk = DiscFileCreate(objectID);
            return (PFStructure)referDisk;
        }

    }
}
