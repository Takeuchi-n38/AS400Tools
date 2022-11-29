using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.DSs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs
{
    class InputBlockFactory
    {
        public static NestedBlockStatement<IStatement> Create(INestedBlockStartStatement element)
        {
            NestedBlockStatement<IStatement>? block;

            if (DsBlock3.TryCreate(element, out block) && block != null) return block;

            if (ProgramDescribedInputFile.TryCreate(element, out block) && block != null) return block;

            //if (PiBlock.TryCreate(element, out block) && block != null) return block;

            //if (PrBlock.TryCreate(element, out block) && block != null) return block;

            throw new ArgumentException();

        }
    }
}
