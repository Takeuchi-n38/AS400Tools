using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Definitions
{
    public interface IRPGDefinitionLine : IRPGLine
    {
        FormType IRPGLine.FormType => FormType.Definition;

        //string CSharpOperandName {get; }

        string Name { get; }

        string Perrcd { get; }

        string ToPosition_Length { get; }

        string DecimalPositions { get; }
    }
}
