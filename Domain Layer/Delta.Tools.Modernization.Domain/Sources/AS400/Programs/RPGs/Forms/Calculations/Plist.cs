using Delta.Tools.Sources.Statements.Blocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class Plist : IBlockStatement<ParmLine>
    {
        public readonly PlistLine plstLine;

        public Plist(PlistLine plstLine)
        {
            this.plstLine = plstLine;
        }

    }
}