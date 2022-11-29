using Delta.AS400.RPGs.Forms.Definitions.Blocks.Pis;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dss;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Prs;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public class DifinitionBlockFactory
    {
        public static NestedBlockStatement<IStatement> Create(INestedBlockStartStatement element)
        {
            NestedBlockStatement<IStatement>? block;

            if (DsBlock4.TryCreate(element, out block) && block != null) return block;

            if (PiBlock.TryCreate(element, out block) && block != null) return block;

            if (PrBlock.TryCreate(element, out block) && block != null) return block;

            throw new ArgumentException();

        }
    }
}
