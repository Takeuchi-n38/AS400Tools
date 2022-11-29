using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Dos;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Ifs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations.Selects;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.Ifs;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class NestedBlockFactory
    {
        public static NestedBlockStatement<IStatement> Create(INestedBlockStartStatement element)
        {
            NestedBlockStatement<IStatement>? block;

            if (IfBlockStatement3.TryCreate(element, out block) && block != null) return block;

            if (IfBlockStatement.TryCreate(element, out block) && block != null) return block;


            if (DoBlockStatement.TryCreate(element, out block) && block != null) return block;

            if (DoForBlockStatement.TryCreate(element, out block) && block != null) return block;

            if (DouBlockStatement.TryCreate(element, out block) && block != null) return block;

            if (DoueqBlockStatement.TryCreate(element, out block) && block != null) return block;

            if (DoweqBlockStatement.TryCreate(element, out block) && block != null) return block;

            if (DowgtBlockStatement.TryCreate(element, out block) && block != null) return block;

            if (SelectBlock.TryCreate(element, out block) && block != null) return block;

            if (DivBlock.TryCreate(element, out block) && block != null) return block;

            throw new ArgumentException();

        }
    }
}
