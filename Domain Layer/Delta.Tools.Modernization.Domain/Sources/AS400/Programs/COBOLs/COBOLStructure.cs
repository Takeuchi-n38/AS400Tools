using Delta.Tools.AS400.Sources;
using Delta.Tools.AS400.Structures.Programs;
using Delta.Tools.Sources.Statements;
using System.Collections.Generic;

namespace Delta.Tools.AS400.Programs.COBOLs
{
    public class COBOLStructure : ProgramStructure
    {

        public COBOLStructure(Source source, List<IStatement> pgmLines) : base(source, pgmLines)
        {

        }

    }
}
