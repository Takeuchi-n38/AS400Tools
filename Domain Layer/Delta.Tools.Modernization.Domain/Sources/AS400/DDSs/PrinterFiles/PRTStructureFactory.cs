using Delta.AS400.Objects;
using Delta.Tools.AS400.DDSs.RecordFormats;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Singles.Comments;
using Delta.Utilities.Extensions.SystemString;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Delta.Tools.AS400.DDSs.PrinterFiles
{

    public class PRTStructureFactory : IStructureFactory
    {

        PRTStructureFactory()
        {
        }
        public static PRTStructureFactory Of()
        {
            return new PRTStructureFactory();
        }

        IStructure IStructureFactory.Create(Source source)
        {

            var OriginalSource = source;
            var ObjectID = source.ObjectID;
            var Elements = new List<IStatement>();
            var PrintLineLists = new List<PrintLineList>();

            for (var originalLinesIndex = 0; originalLinesIndex < OriginalSource.OriginalLines.Length; originalLinesIndex++)
            {
                IPrinterFileLine fileLine = new DDSLine(OriginalSource.OriginalLines[originalLinesIndex], originalLinesIndex);

                if (source.ObjectID.Library.Name== "IIALIB" && fileLine.Value.Contains("INDARA"))//TODO
                {
                    Elements.Add(CommentLine.Of(OriginalSource.OriginalLines[originalLinesIndex], originalLinesIndex));
                    continue;

                }

                if (fileLine.IsCommentLine)
                {
                    Elements.Add(CommentLine.Of(OriginalSource.OriginalLines[originalLinesIndex], originalLinesIndex));
                    continue;
                }

                if (fileLine.CanJoin)
                {
                    originalLinesIndex++;
                    var value = fileLine.Value + OriginalSource.OriginalLines[originalLinesIndex];
                    var startIndex = fileLine.StartLineIndex;
                    var endIndex = originalLinesIndex;
                    fileLine = new DDSLine(value, startIndex, endIndex);
                }

                if (fileLine.IsRecordFormatHeaderLine)
                {
                    PrintLineLists.Add(AddPrintLineList(fileLine));
                    continue;
                }

                if (fileLine.IsVariable)
                {
                    var line = CreateVariableFieldLine(ObjectID, fileLine);
                    PrintLineLists.Last().AddFieldToLastPrintLine(line);
                    continue;
                }

                if (fileLine.HaveKeywords)
                {
                    PrintLineLists.Last().AddFieldToLastPrintLine(fileLine);
                    if (fileLine.Keywords.StartsWith("SPACEA"))
                    {
                        PrintLineLists.Last().PrintLines.Last().SpaceAfter = int.Parse(TextClipper.ClipParameter(fileLine.Keywords, "SPACEA"));

                        while (true)
                        {
                            originalLinesIndex++;
                            if (originalLinesIndex >= OriginalSource.OriginalLines.Length) break;
                            IPrinterFileLine curLine = new DDSLine(OriginalSource.OriginalLines[originalLinesIndex], originalLinesIndex);
                            if (curLine.IsCommentLine)
                            {
                                PrintLineLists.Last().AddFieldToLastPrintLine(curLine);
                                continue;
                            }
                            if (curLine.IsRecordFormatHeaderLine)
                            {
                                PrintLineLists.Add(AddPrintLineList(curLine));
                                break;
                            }
                            if (curLine.IsOutput)
                            {
                                if (curLine.IsVariable)
                                {
                                    var line = CreateVariableFieldLine(ObjectID, curLine);
                                    PrintLineLists.Last().AddPrintLine(line);
                                }
                                else
                                {
                                    PrintLineLists.Last().AddPrintLine(curLine);
                                }
                                break;
                            }

                            PrintLineLists.Last().AddFieldToLastPrintLine(curLine);

                        }
                    }
                    continue;
                }

                Elements.Add(new UnKnownDdsLine(fileLine.Value, fileLine.StartLineIndex));

            }

            return new PRTStructure(source, Elements.ToImmutableList(), PrintLineLists.ToImmutableList());

        }

        PrintLineList AddPrintLineList(IPrinterFileLine fileLine)
        {
            var printLineList = new PrintLineList(fileLine);
            if (fileLine.HaveKeywords)
            {
                if (fileLine.Keywords.Contains("SKIPB"))
                {
                    printLineList.PrintLines.Last().SkipBefore = int.Parse(TextClipper.ClipParameter(fileLine.Keywords, "SKIPB"));
                }
                if (fileLine.Keywords.Contains("SKIPA"))
                {
                    printLineList.PrintLines.Last().SkipAfter = int.Parse(TextClipper.ClipParameter(fileLine.Keywords, "SKIPA"));
                }
                if (fileLine.Keywords.Contains("SPACEB"))
                {
                    printLineList.PrintLines.Last().SpaceBefore = int.Parse(TextClipper.ClipParameter(fileLine.Keywords, "SPACEB"));
                }
                if (fileLine.Keywords.Contains("SPACEA"))
                {
                    printLineList.PrintLines.Last().SpaceAfter = int.Parse(TextClipper.ClipParameter(fileLine.Keywords, "SPACEA"));
                }
            }
            return printLineList;
        }

        IPrinterFileLine CreateVariableFieldLine(ObjectID ObjectID, IPrinterFileLine fileLine)
        {
            if (fileLine.IsREFFLDLine)
            {
                var Reffld = TextClipper.ClipParameter(fileLine.Keywords, "REFFLD").Trim();
                //QTY      URIBFL
                //var splitted = Reffld.Split(' ');
                var fieldName = Reffld.Substring(0, Reffld.IndexOf(' '));
                var tblName = Reffld.Substring(Reffld.LastIndexOf(' ') + 1);
                var objectId = ObjectID.Library.ObjectIDOf(tblName);
                return new RecordFormatRefFieldLine(fileLine.Value, fileLine.StartLineIndex, objectId, fieldName);
            }

            return new RecordFormatFieldLine(fileLine.Value, fileLine.StartLineIndex);

        }
    }

}
