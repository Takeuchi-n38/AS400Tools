using Delta.AS400.DataTypes;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles
{
    public class ProgramDescribedInputFile : NestedBlockStatement<IStatement>
    {
        public IRPGInputLine openerLine;
        public string FileName =>openerLine.FileName;

        public ProgramDescribedInputFile(IRPGInputLine openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is RecordIdentificationLine3)
            {
                result = new ProgramDescribedInputFile((RecordIdentificationLine3)openerLine);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TryClose(IStatement closerLine)
        {
            return !(closerLine is ICommentStatement) && (((IRPGInputLine)closerLine).IsNestedBlockStartStatement || ((IRPGInputLine)closerLine).IsDsLine);
        }


        public IEnumerable<IRPGInputLine> IRPGInputLines => Statements.Where(l => l is IRPGInputLine).Cast<IRPGInputLine>();

        public IEnumerable<IRPGInputLine> Fields => IRPGInputLines.Where(l => !l.IsCondition);

        public int MaxOfToLocation => Fields.Select(line=>line.ToLocationInIntType).Max();

        public IEnumerable<IDataTypeDefinition> TypeDefinitions => Fields.Select(line => line.ToTypeDefinition).Distinct();

    }
}
