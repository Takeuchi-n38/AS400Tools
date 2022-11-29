using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Tools.Sources.Lines;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.ProgramDatas
{
    public class ProgramDataLine : Line, IRPGLine
    {
        FormType IRPGLine.FormType => FormType.ProgramData;

        public ProgramDataLine(string value, int originalLineStartIndex) : base(value, originalLineStartIndex)
        {

        }

    }
}
