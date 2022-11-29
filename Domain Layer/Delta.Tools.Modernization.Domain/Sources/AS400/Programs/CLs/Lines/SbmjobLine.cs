namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class SbmjobLine : CLLine
    {
        public readonly CLLine Command;
        public SbmjobLine(CLLine clCommand, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            Command = clCommand;
        }

        //             SBMJOB     CMD(CALL PGM(SALELIB/CQEA050)) JOB(CQEA050) JOBQ(QBATCH)
    }
}
