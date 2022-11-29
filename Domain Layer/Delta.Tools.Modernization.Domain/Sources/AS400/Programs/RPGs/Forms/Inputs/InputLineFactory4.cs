using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs
{
    public class InputLineFactory4 : IRPGInputLineFactory
    {
        IRPGInputLine IRPGInputLineFactory.Create(string line, int originalLineStartIndex)
        {
            return new RPGLine4(FormType.Input, line, originalLineStartIndex);
        }
    }
}