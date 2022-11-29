using Delta.Tools.Sources.Lines;
using System.Collections.Generic;

namespace Delta.Tools.Sources.Statements.Blocks
{
    public class MethodBlockStatement : IBlockStatement<IStatement>
    {
        public bool IsInzsr=> Name.TrimEnd()=="*INZSR";

        public readonly string Name;
        public readonly ILine? NameComment;

        public List<int> ExfmtLineNumbers = new List<int>();
        public void AddExfmtLineNumber(int exfmtLineNumber)
        {
            ExfmtLineNumbers.Add(exfmtLineNumber);
        }

        public MethodBlockStatement(string name, ILine? nameComment)
        {
            Name = name;
            NameComment = nameComment;
        }

        public static MethodBlockStatement CalculateOf()
        {
            return new MethodBlockStatement("Calculate", null);
        }

        public IStatement CloseStatement { get; set; }

    }
}
/*
      C     @SBSET        BEGSR
     C                   EVAL      D1BANG=WKBANG
     C                   EVAL      D1KBKB=KBKBKB
     C                   EVAL      D1KBNM=KBKBNM
     C                   ENDSR
     */
