using Delta.Utilities.Extensions.SystemString;


namespace Delta.Tools.AS400.Programs.CLs.Lines
{

    public class SndusrmsgStatement : CLLine
    {
        public readonly string Msg;

        public SndusrmsgStatement(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex)
        {
            Msg = TextClipper.ClipParameter(joinedLine, "MSG");
        }

    }
}
