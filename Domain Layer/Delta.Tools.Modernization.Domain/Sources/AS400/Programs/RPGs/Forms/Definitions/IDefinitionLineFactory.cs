using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public interface IDefinitionLineFactory
    {
        IRPGDefinitionLine Create(string line, int originalLineStartIndex);

    }

}
