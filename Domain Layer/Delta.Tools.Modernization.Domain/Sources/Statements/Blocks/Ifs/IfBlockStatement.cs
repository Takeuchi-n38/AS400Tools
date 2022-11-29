using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.Sources.Statements.Blocks.Ifs
{
    public class IfBlockStatement : NestedBlockStatement<IStatement>
    {
        public IIfBlockStartStatement openerLine;

        private IfBlockStatement(IIfBlockStartStatement openerLine)
        {
            this.openerLine = openerLine;
        }

        public static bool TryCreate(INestedBlockStartStatement openerLine, out NestedBlockStatement<IStatement>? result)
        {
            if (openerLine is IIfBlockStartStatement)
            {
                result = new IfBlockStatement((IIfBlockStartStatement)openerLine);
                return true;
            }

            result = null;
            return false;
        }

        public IIfBlockEndStatement closerLine;
        public override bool TryClose(IStatement closerLine)
        {
            if (closerLine is IIfBlockEndStatement)
            {
                this.closerLine = (IIfBlockEndStatement)closerLine;
                return true;
            }
            return false;
        }

    }
}
