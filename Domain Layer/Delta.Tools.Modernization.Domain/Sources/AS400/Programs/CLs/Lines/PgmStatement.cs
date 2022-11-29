using Delta.Utilities.Extensions.SystemString;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class PgmStatement : CLLine
    {
        readonly string Parm;

        public readonly List<string> ParameterNames;

        public PgmStatement(string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            //             PGM        PARM(&EMPLNO &CLAPZKB &CLAIM &CLAIM1 &CLAIMFR & CLAIMTO)
            Parm = TextClipper.ClipParameter(Value, "PARM");
            var parameterNames = new List<string>();
            if (Value.Contains(" PARM("))
            {
                Parm.Split(" ").ToList().ForEach(parm =>
                {
                    parameterNames.Add(parm);
                });
            }
            ParameterNames = parameterNames;
        }

    }
}
