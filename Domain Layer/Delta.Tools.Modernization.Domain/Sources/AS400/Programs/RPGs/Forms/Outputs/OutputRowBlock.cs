using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Blocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Outputs
{
    public class OutputRowBlock : IBlockStatement<IRPGLine>
    {
        internal bool IsForDiskFile => NameLine.IsForDiskFile;

        internal bool IsFileNameLine => NameLine.IsFileNameLine;

        internal string FileName => NameLine.FileName;

        public string Name => NameLine.Name==string.Empty?FileName:NameLine.Name;

        public string RecordLength {get;private set; }

        IRPGOutputLine NameLine => (IRPGOutputLine)Statements[0];
        internal OutputRowBlock(IRPGOutputLine nameLine, string RecordLength)
        {
            this.RecordLength= RecordLength;
            Add(nameLine);
        }


    }
}
