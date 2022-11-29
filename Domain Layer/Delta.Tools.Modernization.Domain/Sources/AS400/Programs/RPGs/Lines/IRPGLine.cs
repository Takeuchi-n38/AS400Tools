using Delta.Tools.AS400.Programs.RPGs.Forms;
using Delta.Tools.Sources.Lines;
using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Lines
{
    public interface IRPGLine : ILine, ISingleStatement
    {
        FormType FormType{get; }// => FormTypeExtend.Of(Value);

        bool IsCommentLine => Value.Substring(6, 1) == "*";

    }
}
