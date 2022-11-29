using Delta.Tools.AS400.Objects;
using Delta.Tools.AS400.Programs.CLs.Lines;
using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using Delta.Tools.Sources.Statements;
using System.Collections.Generic;


namespace Delta.Tools.AS400.Programs.CLs
{
    public class CLStructureFactory : IStructureFactory
    {
        readonly CLLineFactory CLLineFactory;

        CLStructureFactory(CLLineFactory CLLineFactory)
        {
            this.CLLineFactory = CLLineFactory;
        }

        public static CLStructureFactory Of(ObjectIDFactory ObjectIDFactory)
        {
            return new CLStructureFactory(new CLLineFactory(ObjectIDFactory));
        }

        IStructure IStructureFactory.Create(Source source)
        {
            return new CLStructure(source, CreatePgmLines(source));
        }


        //開始文言：/*START GET TESTING DATA*/
        //終了文言：/*END   GET TESTING DATA*/
        //※ ENDの後にスペースを３つ挿入しています。


        List<IStatement> CreatePgmLines(Source source)
        {
            var joinedLines = new List<IStatement>();
            int originalLineStartIndex = 0;
            int originalLineEndIndex = 0;
            var exclude=false;
            for (var originalLinesIndex = 0; originalLinesIndex < source.OriginalLines.Length; originalLinesIndex++)
            {
                var joinedLine = source.OriginalLines[originalLinesIndex].TrimStart();

                if (joinedLine.StartsWith(@"/*START GET TESTING DATA*/")) exclude = true;
                if (joinedLine.StartsWith(@"/*END   GET TESTING DATA*/")) exclude = false;

                originalLineStartIndex = originalLinesIndex;

                if (CLLine.IsJoinee(joinedLine))
                {
                    joinedLine = joinedLine[0..^1];
                    while (true)
                    {
                        var nextLine = source.OriginalLines[++originalLinesIndex].TrimStart();
                        if (CLLine.IsJoinee(nextLine))
                        {
                            joinedLine += nextLine[0..^1];
                            continue;
                        }
                        else
                        {
                            joinedLine += nextLine;
                            originalLineEndIndex = originalLinesIndex;
                            break;
                        }
                    }
                }

                originalLineEndIndex = originalLinesIndex;

                if (joinedLine == string.Empty)
                {
                    joinedLines.Add(CLEmptyLine.Of(originalLineStartIndex));
                    continue;
                }

                if (CLLine.IsSingleLineComment(joinedLine))
                {
                    joinedLines.Add(CLCommentLine.Of(joinedLine, originalLineStartIndex));
                    continue;
                }

                if (joinedLine.Contains(":") && !joinedLine.Contains(" :"))
                {

                    if (joinedLine.EndsWith(":"))
                    {
                        joinedLines.Add(CLLabelStatement.Of(joinedLine, originalLineStartIndex));
                        continue;
                    }
                    var splits = joinedLine.Split(':');
                    joinedLines.Add(CLLabelStatement.Of(splits[0], originalLineStartIndex));

                    joinedLine = splits[1].TrimStart();
                }

                joinedLines.Add(CLLineFactory.Of(source.ObjectID.Library, (exclude ? "/*" : string.Empty) + joinedLine + (exclude ? "*/" : string.Empty), originalLineStartIndex, originalLineEndIndex));

            }
            return joinedLines;
        }

    }
}
