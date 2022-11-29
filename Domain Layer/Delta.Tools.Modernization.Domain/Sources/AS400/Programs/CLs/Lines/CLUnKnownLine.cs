namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CLUnKnownLine : CLLine
    {
        public readonly string Command;
        public CLUnKnownLine(string command, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            Command = command;
        }

        public static CLUnKnownLine Create(string joinedLine, int originalLineStartIndex, int originalLineEndIndex)
        {
            var command = string.Empty;
            if (joinedLine.Contains(" "))
            {
                command = joinedLine.Substring(0, joinedLine.IndexOf(' '));
            }
            else
            {
                command = joinedLine;
            }
            return new CLUnKnownLine(command, joinedLine, originalLineStartIndex, originalLineEndIndex);
        }
    }
}
