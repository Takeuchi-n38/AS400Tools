using Delta.Tools.Sources.Statements.Blocks.Ifs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.CLs.Lines
{
    public class EnddoStatement : CLLine, IIfBlockEndStatement
    {

        public EnddoStatement(string joinedLine, int originalLineStartIndex) : base(joinedLine, originalLineStartIndex, originalLineStartIndex)
        {
            //0031 IF       COND(&IN09 = '1') THEN(DO)
            //0032 WRKSPLF    SELECT(SALE)
            //0033 ENDDO            
        }
        //internal override void Output(Indent indent, StringBuilder text)
        //{
        //    var content = $"if({Condition()}) {Expression()};";
        //    if (Then == "RETURN") content = "//TODO:RETURN ";
        //    text.AppendLine($"{indent}{content}{OriginalComment}");
        //}

    }
}
