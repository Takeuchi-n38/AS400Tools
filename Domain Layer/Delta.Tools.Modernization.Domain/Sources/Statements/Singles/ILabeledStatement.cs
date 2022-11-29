using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.Sources.Statements.Singles
{
    public interface ILabeledStatement : ISingleStatement
    {

        string Name { get; }

    }
}
