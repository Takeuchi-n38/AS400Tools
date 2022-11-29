using Delta.Tools.Sources.Statements.Singles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.COBOLs
{
    public class COBOLLine : ISingleStatement
    {
        //IEnumerable<IStatement> IStatement.AllStatements()
        //{
        //    return new List<IStatement>() { this };
        //}

        protected readonly string line;

        public COBOLLine(string line)
        {
            //if (line.Length < 76) line = line.PadRight(76);
            this.line = line;
        }

    }
}
