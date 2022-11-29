using Delta.AS400.Libraries;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class AutostartLine : CLLine
    {
        public readonly Library Library;
        public readonly string ProgramName;

        public AutostartLine(Library Library, string ProgramName, string joinedLine, int originalLineStartIndex, int originalLineEndIndex) : base(joinedLine, originalLineStartIndex, originalLineEndIndex)
        {
            this.Library = Library;
            this.ProgramName = ProgramName;
            //            this.ThisCallerProgram = ProgramStructureFactory.Instance.CreateStructure(Library, ProgramName); 

        }

    }
}
//AUTOSTART  CLASS(MONTH01) GROUP(JVAA65MU) DATE(&YMD) NBR(2)