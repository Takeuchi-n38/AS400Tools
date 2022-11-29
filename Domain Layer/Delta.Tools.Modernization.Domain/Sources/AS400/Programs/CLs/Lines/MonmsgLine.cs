namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class MonmsgLine : CLLine
    {
        internal readonly string Msgid;
        public readonly CLLine Command;
        public MonmsgLine(string msgid, CLLine clCommand, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            Msgid = msgid;
            Command = clCommand;
        }

    }
}
//MONMSG MSGID(CPF9801) EXEC(CRTPF FILE(&VAA / VAA530IN) RCDLEN(170) IGCDTA(*YES) OPTION(*NOLIST * NOSRC)  SIZE(*NOMAX) LVLCHK(*NO))//MONMSG     MSGID(CPF0000)
