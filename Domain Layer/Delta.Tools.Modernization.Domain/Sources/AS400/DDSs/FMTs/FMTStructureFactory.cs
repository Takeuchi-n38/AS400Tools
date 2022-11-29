using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures;
using Delta.Tools.Sources.Statements;
using System.Collections.Generic;

namespace Delta.Tools.AS400.DDSs.FMTs
{
    public class FMTStructureFactory : IStructureFactory
    {
        FMTStructureFactory()
        {
        }

        public static FMTStructureFactory Of()
        {
            return new FMTStructureFactory();
        }

        IStructure IStructureFactory.Create(Source source)
        {
            var OriginalSource = source;
            var Elements = new List<FormatDataLine>();

            for (var originalLinesIndex = 0; originalLinesIndex < OriginalSource.OriginalLines.Length; originalLinesIndex++)
            {
                Elements.Add(new FormatDataLine(OriginalSource.OriginalLines[originalLinesIndex], originalLinesIndex));
            }

            return new FMTStructure(source, Elements);

        }

    }
}
