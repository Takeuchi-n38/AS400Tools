using Delta.AS400.Libraries;
using Delta.Tools.AS400.Structures;
using Delta.Tools.AS400.Structures.Programs;
using Delta.Tools.Sources.Statements.Singles;
using System.Collections.Generic;
using System.Linq;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Calculations
{
    public class RPGCallLine : CalculationLine
    {

        public string ProgramName;
        public Library Library { get; set; }

        List<string> CallerProgramParameterNames { get; } = new List<string>();
        public void AddCallerProgramParameterNames(string name)
        {
            CallerProgramParameterNames.Add(name);
        }

        public CallProgramStatement ThisCallProgramStatement(IStructure ThisCallerProgram)
        {
            var dialogName = ThisCallerProgram.ObjectID.Name;

            if (ThisCallerProgram is ProgramStructure && ((ProgramStructure)ThisCallerProgram).WorkstationFileObjectID != null)
            {
                dialogName = ((ProgramStructure)ThisCallerProgram).WorkstationFileObjectID.Name;
            }

            return new CallProgramStatement(dialogName, CallerProgramParameterNames, this);

        }

        public RPGCallLine(Library library, CalculationLine RPGCLine) : base(RPGCLine)
        {
            Library = library;

            if (Factor2andMore.Contains("("))
            {
                var divide = Factor2andMore.IndexOf('(');
                ProgramName = Factor2andMore.Substring(0, divide).Trim();
                Factor2andMore.Substring(divide + 1, Factor2andMore.IndexOf(')') - divide - 1)
                    .Split(":").ToList()
                    .ForEach(name => AddCallerProgramParameterNames(name));
            }
            else
            {
                ProgramName = Factor2.Replace('\'', ' ').Trim();
                var name = ResultField.TrimEnd();
                if (name != string.Empty) AddCallerProgramParameterNames(name);
            }
        }

    }
}