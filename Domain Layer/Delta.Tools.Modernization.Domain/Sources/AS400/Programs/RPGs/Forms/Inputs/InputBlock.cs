using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs
{
    public class InputBlock : IBlockStatement<IStatement>
    {
        public IEnumerable<ProgramDescribedInputFile> ProgramDescribedInputFiles=>Statements.Where(line=>line is ProgramDescribedInputFile)
            .Cast<ProgramDescribedInputFile>();

    }
}
