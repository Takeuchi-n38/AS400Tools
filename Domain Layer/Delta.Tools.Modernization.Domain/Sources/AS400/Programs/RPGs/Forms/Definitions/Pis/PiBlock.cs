using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Pis;
using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;

namespace Delta.AS400.RPGs.Forms.Definitions.Blocks.Pis
{
    public class PiBlock : NestedBlockStatement<IStatement>
    {
        public PiLine openerLine;

        private PiBlock(PiLine openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is PiLine)
            {
                result = new PiBlock((PiLine)openerLine);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TryClose(IStatement closerLine)
        {
            return (closerLine is INestedBlockStartStatement || closerLine is ICommentStatement);
        }

    }
}
//プロシージャー･インターフェイス (PI) プロシージャーの内部での受け取る引数の変数名、タイプ