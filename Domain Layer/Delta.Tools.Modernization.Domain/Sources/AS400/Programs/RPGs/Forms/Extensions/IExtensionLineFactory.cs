using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Extensions
{
    public interface IExtensionLineFactory
    {
        IRPGExtensionLine Create(string line, int originalLineStartIndex);

    }
}
