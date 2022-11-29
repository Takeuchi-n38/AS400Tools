using Delta.Tools.AS400.Programs.RPGs.Forms.Definitions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Forms.Extensions.Dims;
using Delta.Tools.AS400.Programs.RPGs.Lines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.Tools.AS400.Programs.RPGs.Forms.Extensions
{
    public class ExtensionLineFactory3 : IExtensionLineFactory
    {
        IRPGExtensionLine IExtensionLineFactory.Create(string line, int originalLineStartIndex)
        {
            IRPGExtensionLine3 rpg = new RPGLine3(FormType.Extension, line, originalLineStartIndex);
            if(rpg.Perrcd==string.Empty)
            {
                return new DimLine3(rpg);
            }
            else
            {
                return new DimCtdataLine3(rpg);
            }


            throw new NotImplementedException();


        }
    }
}
