using Delta.Utilities.Extensions.SystemString;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class RtvsysvalStatement : CLLine
    {
        public readonly string Sysval;
        public readonly string Rtnvar;

        public string SystemValue()
        {
            if (Sysval == "QDATE") return "Retriever.Instance.System.Qdate;";
            if (Sysval == "QTIME") return "Retriever.Instance.System.Qtime;";

            return Sysval;
        }

        public RtvsysvalStatement(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex)
        {
            //0102 RTVSYSVAL  SYSVAL(QDATE) RTNVAR(&YMD)
            //0103 RTVSYSVAL  SYSVAL(QTIME) RTNVAR(&HMS)
            Sysval = TextClipper.ClipParameter(joinedLine, "SYSVAL");
            Rtnvar = TextClipper.ClipParameter(joinedLine, "RTNVAR");
        }

    }
}
