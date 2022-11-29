using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.Sources.Statements.Singles
{
    //1Lineで構成されるStatement
    public interface ISingleStatement : IStatement
    {
        //IEnumerable<IStatement> IStatement.AllStatements()
        //{
        //    return new List<IStatement>();
        //}
    }
}
