using Delta.Tools.Sources.Statements;
using System.Collections.Generic;

namespace Delta.Tools.Sources.Statements.Blocks
{
    //複数Lineで構成されるStatement
    public class IBlockStatement<T> : IStatement
        where T : IStatement
    {
        public List<T> Statements { get; } = new List<T>();

        public virtual void Add(T element)
        {
            Statements.Add(element);
        }

    }
}
