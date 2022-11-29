using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Extensions
{
    public class ExtensionLineFactory4 : IExtensionLineFactory
    {
        IRPGExtensionLine IExtensionLineFactory.Create(string line, int originalLineStartIndex)
        {
            throw new NotImplementedException();// return new RPGLine4(line, originalLineStartIndex);
        }
    }
}
