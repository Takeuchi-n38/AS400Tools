using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.AS400.Programs.RPGs.Forms.Calculations;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Ls;
using Delta.Tools.AS400.Programs.RPGs.Forms.ProgramDatas;
using System;

namespace Delta.Tools.AS400.Programs.RPGs.Lines
{
    public class RPGCommentLineFactory
    {
        public static bool IsSingleLineComment(string singleLine)
            => singleLine.Length < 7 ? false : singleLine.Substring(6, 1) == "*";

        //public static IRPGLine Of(FormType formType, string originalLine, int originalLineStartIndex)
        //{
        //    switch (formType)
        //    {
        //        case FormType.Control: 
        //        case FormType.FileDescription:
        //        case FormType.L:
        //        case FormType.Definition:
        //        case FormType.Extension:
        //        case FormType.Calculation:
        //        case FormType.Output:
        //        case FormType.ProgramData:
        //        case FormType.Input:
        //            return new RPGCommentLine(formType,originalLine, originalLineStartIndex);

        //        default:
        //            throw new ArgumentException();
        //    }
        //}



    }
}
