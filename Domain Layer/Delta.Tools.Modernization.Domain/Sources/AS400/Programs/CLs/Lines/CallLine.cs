using Delta.AS400.Libraries;
using Delta.Tools.AS400.Structures;
using Delta.Tools.AS400.Structures.Programs;
using Delta.Tools.Sources.Statements.Singles;
using Delta.Utilities.Extensions.SystemString;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CallLine : CLLine
    {
        public readonly Library Library;
        public readonly string ProgramName;

        readonly string Parm;

        public CallProgramStatement ThisCallProgramStatement(IStructure ThisCallerProgram)
        {
            var parameters = new List<string>();

            if (Value.Contains(" PARM("))
            {
                var parm = TextClipper.ClipParameter(Value, "PARM");
                var splittedParm = parm.Split(" ");

                for (int i = 1; i <= splittedParm.Count(); i++)
                {
                    parameters.Add(splittedParm[i - 1]);
                }
            }

            var dialogName = ThisCallerProgram.ObjectID.Name;
            if (ThisCallerProgram is ProgramStructure && ((ProgramStructure)ThisCallerProgram).WorkstationFileObjectID != null)
            {
                dialogName = ((ProgramStructure)ThisCallerProgram).WorkstationFileObjectID.Name;
            }

            return new CallProgramStatement(dialogName, parameters, this);

        }

        public CallLine(Library Library, string ProgramName, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.Library = Library;
            this.ProgramName = ProgramName;

            ////0062 CALL       PGM(*CURLIB/PQEA510) PARM(&OPT)

            if (Value.Contains(" PARM("))
            {
                Parm = TextClipper.ClipParameter(Value, "PARM");
            }
            else
            {
                Parm = string.Empty;
            }
        }

    }
}
