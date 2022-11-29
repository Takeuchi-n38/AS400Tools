using Delta.Utilities.Extensions.SystemString;
using System;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class CLGotoStatement : CLLine
    {

        CLGotoStatement(string labelName, int originalLineStartIndex) : base(labelName, originalLineStartIndex)
        {
            //GOTO STR
        }

        public static CLGotoStatement Of(string line, int originalLineStartIndex)
        {
            var splitted = line.CompressSpacesToSingleSpace().Split(" ");
            var labelName = splitted[1];
            if (labelName.Contains("CMDLBL"))
            {
                labelName = TextClipper.ClipParameter(line, "CMDLBL");
            }
            return new CLGotoStatement(labelName, originalLineStartIndex);
        }

    }
}
