using Delta.Tools.Sources.Statements.Blocks;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs
{
    public class OutputRowsContainerBlock : IBlockStatement<OutputRowBlock>
    {
        public bool IsForDiskFile => Statements.First().IsForDiskFile;

        internal OutputRowsContainerBlock(OutputRowBlock block)
        {
            Statements.Add(block);
        }

        //public string OutputRepositoryName { get; set; }
        public string FileName { get; set; }

    }
}
