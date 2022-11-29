using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.DSs;
using Delta.Tools.AS400.Programs.RPGs.Forms.Inputs.ProgramDescribedFiles;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Inputs
{
    public class InputLineFactory3 : IRPGInputLineFactory
    {
        IRPGInputLine IRPGInputLineFactory.Create(string line, int originalLineStartIndex)
        {
            IRPGInputLine3 lineI3 = new RPGLine3(FormType.Input, line,originalLineStartIndex);

            if((lineI3.FileName != string.Empty && !lineI3.FileName.StartsWith(" ")) && !lineI3.IsDsLine) return new RecordIdentificationLine3(lineI3);

            //if (lineD.DefinitionType == "S") return new VarLine(lineD);

            if (lineI3.IsDsLine) return new DsLine3(lineI3);

            //if (lineD.DefinitionType == "PR") return new PrLine(lineD);

            //if (lineD.DefinitionType == "PI") return new PiLine(lineD);

            if (lineI3.FromLocation != string.Empty) return new InputItemLine3(lineI3);

            //if (lineD.ToPosition_Length != string.Empty && lineD.Name != string.Empty) return new DifinitionBlockItemLine(lineD);

            //if (lineD.Name != string.Empty) return new DsAliasNameItemLine4(lineD);

            return lineI3;

        }
    }
}
