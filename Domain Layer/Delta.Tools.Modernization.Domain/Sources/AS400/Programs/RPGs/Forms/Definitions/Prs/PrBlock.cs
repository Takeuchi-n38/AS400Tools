using Delta.Tools.Sources.Statements;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;
using Delta.Tools.Sources.Statements.Singles.Comments;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Prs
{
    public class PrBlock : NestedBlockStatement<IStatement>
    {
        public PrLine OpenerLine;

        private PrBlock(PrLine openerLine)
        {
            OpenerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is PrLine)
            {
                result = new PrBlock((PrLine)openerLine);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TryClose(IStatement closerLine)
        {
            return closerLine is INestedBlockStartStatement || closerLine is ICommentStatement;
        }

        public bool IsPrameter(string rpgName)
        {
            return OpenerLine.IsPrameter(rpgName);
        }

    }
}
//プロトタイプ(PR) 外部から見た場合のこのプロシージャーの定義