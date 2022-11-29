using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Statements.Blocks.NestedBlocks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles
{
    public class RecordIdentificationLine3 : RPGLine3, INestedBlockStartStatement
    {
        public RecordIdentificationLine3(IRPGInputLine3 line) : base(line.FormType, line.Value, line.StartLineIndex)
        {

        }

    
        //readonly IRPGInputLine RPGInputLine;
        ////Positions 7-16 (File Name)
        //public string FileName => RPGInputLine.FileName;

        ////Positions 16-18 (Logical Relationship)
        ////Positions 17-18 (Sequence)
        ////Position 19 (Number)
        ////Position 20 (Option)
        ////Positions 21-22 (Record Identifying Indicator, or **)
        ////Positions 23-46 (Record Identification Codes)

        //public RecordIdentificationLine3(IRPGInputLine RPGInputLine) 
        //{
        //    this.RPGInputLine = RPGInputLine;
        //}

    }
}
