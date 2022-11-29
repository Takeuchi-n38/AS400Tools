using Delta.Tools.Sources.Statements.Blocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class Klist : IBlockStatement<KfldLine>
    {
        public readonly KlistLine klistLine;
        public Klist(KlistLine klistLine)
        {
            this.klistLine = klistLine;
        }

    }
}
