using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dss;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Pis;
using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Prs;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using Delta.Utilities.Extensions.SystemString;
using System;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public class DefinitionLineFactory4 : IDefinitionLineFactory
    {
        IRPGDefinitionLine IDefinitionLineFactory.Create(string line, int originalLineStartIndex)
        {
            IRPGDefinitionLine4 lineD = new RPGLine4(FormType.Definition, line, originalLineStartIndex);

            if (lineD.Keywords.StartsWith("DIM("))
            {
                if (lineD.Keywords.Contains("CTDATA")) return new DimCtdataLine4(lineD);
                var size = int.Parse(TextClipper.ClipParameter(line, "DIM"));
                return new DimLine4(size,lineD);
            }

            if (lineD.DefinitionType == "S") return new VarLine(lineD);

            if (lineD.DefinitionType == "DS") return new DsLine4(lineD);

            if (lineD.DefinitionType == "PR") return new PrLine(lineD);

            if (lineD.DefinitionType == "PI") return new PiLine(lineD);

            if (lineD.FromPosition != string.Empty) return new DsItemLine4(lineD);

            if (lineD.ToPosition_Length != string.Empty && lineD.Name != string.Empty) return new DifinitionBlockItemLine(lineD);

            if (lineD.Name != string.Empty) return new DsAliasNameItemLine4(lineD);

            throw new NotImplementedException();
        }

    }
}
