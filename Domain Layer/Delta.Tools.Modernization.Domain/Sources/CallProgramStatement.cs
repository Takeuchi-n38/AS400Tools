using Delta.Tools.Sources.Lines;
using System.Collections.Generic;

namespace Delta.Tools.Sources.Statements.Singles
{
    public class CallProgramStatement
    {
        public string CallerProgramName { get; }

        public List<string> CallerProgramParameterNames { get; }

        public ILine Comment { get; }

        public CallProgramStatement(string callerProgramName, List<string> callerProgramParameterNames, ILine comment)
        {
            CallerProgramName = callerProgramName;
            CallerProgramParameterNames = callerProgramParameterNames;
            Comment = comment;
        }

    }
}
