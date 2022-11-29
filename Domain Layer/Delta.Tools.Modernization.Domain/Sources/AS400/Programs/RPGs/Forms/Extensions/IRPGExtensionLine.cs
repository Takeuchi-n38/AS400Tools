using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Extensions
{
    public interface IRPGExtensionLine : IRPGLine
    {
        FormType IRPGLine.FormType => FormType.Extension;

        string Name { get;}
        string Perrcd { get; }

        string ArrayLength { get; }

        string ItemLength { get; }

        string DecimalPositions { get; }

    }
}
