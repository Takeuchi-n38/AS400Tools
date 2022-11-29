using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class RtvjobaStatement : CLLine
    {
        public readonly string Job;
        public readonly string User;
        public readonly string Nbr;

        public RtvjobaStatement(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex)
        {
            //0016 RTVJOBA    JOB(&JOB)
            //0024 RTVJOBA    JOB(&J) USER(&U) NBR(&B)
            var job = string.Empty;
            if (Value.Contains("JOB("))
            {
                job = TextClipper.ClipParameter(joinedLine, "JOB");
            }
            Job = job;

            var user = string.Empty;
            if (Value.Contains("USER("))
            {
                user = TextClipper.ClipParameter(joinedLine, "USER");
            }
            User = user;

            var nbr = string.Empty;
            if (Value.Contains("NBR("))
            {
                nbr = TextClipper.ClipParameter(joinedLine, "NBR");
            }
            Nbr = nbr;


        }

    }
}
