namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class SbmrmtcmdLine : CLLine
    {
        public readonly CLLine Command;
        public SbmrmtcmdLine(CLLine clCommand, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            Command = clCommand;
        }

    }
}
//SBMRMTCMD CMD('CALL PGM(SIGELIB/CLCB900)') +
//                        DDMFILE(SALELIB/SIGERMTF)