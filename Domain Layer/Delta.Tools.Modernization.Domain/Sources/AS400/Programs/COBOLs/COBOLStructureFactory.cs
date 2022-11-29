using System.Collections.Generic;
using Delta.Tools.Sources.Statements;
using Delta.Tools.AS400.Programs.COBOLs;
using Delta.Tools.AS400.Structures;
using Delta.Tools.AS400.Sources;

namespace Delta.Tools.AS400.Analyzer.Programs.COBOLs
{
    public class COBOLStructureFactory : IStructureFactory
    {
        COBOLStructureFactory()
        {
        }
        public static COBOLStructureFactory Of()
        {
            return new COBOLStructureFactory();
        }

        IStructure IStructureFactory.Create(Source source)
        {
            return new COBOLStructure(source, CreatePgmLines(source));
        }

        List<IStatement> CreatePgmLines(Source source)
        {
            var joinedLines = new List<IStatement>();
            for (var originalLinesIndex = 0; originalLinesIndex < source.OriginalLines.Length; originalLinesIndex++)
            {
                var originalLine = new COBOLLine(source.OriginalLines[originalLinesIndex]);
                joinedLines.Add(originalLine);
            }
            return joinedLines;
        }

    }
}
