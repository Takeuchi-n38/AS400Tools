namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CLLabelStatement : CLLine
    {

        CLLabelStatement(string name, int originalLineStartIndex) : base(name, originalLineStartIndex)
        {

        }

        public static CLLabelStatement Of(string line, int originalLineStartIndex)
        {
            var name = string.Empty;
            if (line.EndsWith(":"))
            {
                name = line[0..^1];
            }
            else
            {
                name = line;
            }

            return new CLLabelStatement(name, originalLineStartIndex);
        }

    }
}
